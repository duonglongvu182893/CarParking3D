using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class TestMap : MonoBehaviour
{
    public int sizeX = 2;

    public int sizeZ = 2;



    public int numberOfCar = 4;
    public GameObject boder;
    public GameObject Car;
    public GameObject Floor;
    public GameObject Boder;
    public GameObject floor;

    public LayerMask layerMask;

    public List<Vector3> positionIntialCar = new List<Vector3>();
    public List<Vector3> positionInMap = new List<Vector3>();
  

    public List<GameObject> car = new List<GameObject>();
    public List<GameObject> carIsOnMap = new List<GameObject>();
    public List<GameObject> FloorGen = new List<GameObject>();
    public List<GameObject> boderGen = new List<GameObject>();


    public List<RoadOnMap> roadX = new List<RoadOnMap>();
    public List<RoadOnMap> roadZ = new List<RoadOnMap>();

    public int[] idCar;

    public int numberCartype1;
    public int numberCartype2;

    public Vector3 posIsUse;
    public Vector3 posIsUse2;

    public bool isRunReset = false;

    public int k = 5;

    public static TestMap instance;

  
    

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        
        StartGenMap();

    }
    public void StartGenMap()
    {
        GetInformationFromInspector();
        GenFloor();
        GenBoder();
        CreadRoad.instance.Test();
        CreateCar();
        NumberOfCar();
        
        if (carIsOnMap.Count != numberOfCar)
        {

            while (k > 0)
            {
                GenCarOnMap();
                if (carIsOnMap.Count == numberOfCar)
                {
                    break;
                }
                if (k < 0)
                {
                    Debug.Log("khong the gen");
                }
                Debug.Log(carIsOnMap.Count);


            }

        } 

    }
    private void Update()
    {
        if (isRunReset)
        {
            GenCarOnMap();
        }
    }
    private void Awake()
    {
        instance = this;
    }
    [System.Obsolete]
    public void CreateCar()
    {
        for (int i = 0; i < idCar.Length; i++)
        {
            if (positionIntialCar.Count > 0)
            { 
            
                RunAgaint(idCar[i]);
            }
           
        }
    } 
    public void RunAgaint(int id)
    {
        int inx = Random.RandomRange(0, positionIntialCar.Count);

        GameObject CloneCar = PickCar(id);
        if (positionIntialCar.Count > 0 )
        {
            
            if (CheckCanChoose(CloneCar, positionIntialCar[inx]))
            {
                GenCar(positionIntialCar[inx], CloneCar);

            
            }
            else if (!CheckCanChoose(CloneCar, positionIntialCar[inx]))
            {
               
               positionIntialCar.RemoveAt(inx);
                RunAgaint(id);
            }
        }
    }
    //Gen san choi
    [System.Obsolete]
    public void GenFloor()
    {
        /*
        for (int i = -sizeX; i <= sizeX; i++)
        {
            for (int j = -sizeZ; j <= sizeZ; j++)
            {
                Vector3 pos = new Vector3(i, 0, j);
                GameObject firtBlock = Instantiate(floor, pos, Quaternion.identity);
                firtBlock.transform.parent = Floor.transform;
                positionInMap.Add(new Vector3(i, 0f, j));
                FloorGen.Add(firtBlock);
            }
        }*/
        for (int i = 0; i <= sizeX - 1; i++) 
        {
            for (int j = 0; j <= sizeZ - 1; j++) 
            {
                Vector3 pos = new Vector3(i, 0, j);
                GameObject firtBlock = Instantiate(floor, pos, Quaternion.identity);
                firtBlock.transform.parent = Floor.transform;
                positionInMap.Add(new Vector3(i, 0f, j));
                FloorGen.Add(firtBlock);
            }
        }
        RandomIntialPosition();
    }
    //Vi tri xung quanh co the day xe vao
    [System.Obsolete]
    public void RandomIntialPosition()
    {
        /*
        for (int i = -sizeX - 1; i <= sizeX + 1; i++)
        {
            for (int j = -sizeZ - 1; j <= sizeZ + 1; j++)
            {

                if ((i != -sizeX - 1 || j != -sizeZ - 1) && (i != -sizeX - 1 || j != sizeZ + 1) && (i != sizeX + 1 || j != sizeZ + 1) && (i != sizeX + 1 || j != -sizeZ - 1))
                {
                    positionIntialCar.Add(new Vector3(i, 0, j));

                }


            }
        }
        for (int i = 0; i < positionIntialCar.Count; i++)
        {
            for (int j = 0; j < positionInMap.Count; j++)
            {
                if (positionInMap[j] == positionIntialCar[i])
                {
                    positionIntialCar.RemoveAt(i);
                }


            }
        }*/

        for (int i = - 1; i <= sizeX ; i++)
        {
            for (int j =- 1; j <= sizeZ ; j++)
            {

                if ((i !=  - 1 || j !=- 1) && (i != - 1 || j != sizeZ ) && (i != sizeX  || j != sizeZ) && (i != sizeX || j != - 1))
                {
                    positionIntialCar.Add(new Vector3(i, 0, j));

                }


            }
        }
        for (int i = 0; i < positionIntialCar.Count; i++)
        {
            for (int j = 0; j < positionInMap.Count; j++)
            {
                if (positionInMap[j] == positionIntialCar[i])
                {
                    positionIntialCar.RemoveAt(i);
                }


            }
        }


        //ControllRoad();


    }
    public void GenBoder()
    {
        for (int i = 0; i < positionIntialCar.Count; i++)
        {
            GameObject newBoder = Instantiate(boder, positionIntialCar[i], Quaternion.identity);
            newBoder.transform.parent = Boder.transform;

            boderGen.Add(newBoder);
        }
    }


    /*
    [System.Obsolete]



    public void GenCar(Vector3 pos, GameObject g)
    {
        
        RaycastHit hit;
        if (pos.x == sizeX + 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, layerMask))
            {
                
            
                float offSet = (g.transform.localScale.x) / 2;

                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x + offSet, hit.point.y, hit.point.z), Quaternion.Euler(0, 180, 0));

                carSpaw.GetComponent<Car>().SetRotation(Quaternion.Euler(0, 180, 0));
                carSpaw.transform.parent = Car.transform; 

                
                Vector3 bound = carSpaw.GetComponent<Collider>().bounds.min;
                if (carSpaw.GetComponent<Collider>().bounds.min.x > sizeX + 0.7f || carSpaw.GetComponent<Collider>().bounds.max.x > sizeX + 0.7f)
                {

                    //Destroy(carSpaw);
                   
                    carSpaw.active = false;
                    

                }
                else
                {

                    carIsOnMap.Add(carSpaw);
                }
            }
            else
            {
                Debug.Log("khong trung");
            }
        }
        
        if (pos.x == -sizeX - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layerMask))
            {

               
                float offSet = (g.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x - offSet, hit.point.y, hit.point.z), Quaternion.Euler(0, 0, 0));

                carSpaw.GetComponent<Car>().SetRotation(Quaternion.Euler(0, 0, 0));
                carSpaw.transform.parent = Car.transform;
                Vector3 bound = carSpaw.GetComponent<Collider>().bounds.min;
                if (carSpaw.GetComponent<Collider>().bounds.min.x < -sizeX - 0.7f || carSpaw.GetComponent<Collider>().bounds.max.x < -sizeX - 0.7f)
                {
                    //Destroy(carSpaw);
                    
                    carSpaw.active = false;
                    
                }
                else
                {
                    carIsOnMap.Add(carSpaw);

                }


            }
            else
            {
                Debug.Log("khong trung");
            }
        }

        if (pos.z == sizeZ + 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
            {

               
                float offSet = (g.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x, hit.point.y, hit.point.z + offSet), Quaternion.Euler(0, 90, 0));

                carSpaw.GetComponent<Car>().SetRotation(Quaternion.Euler(0, 90, 0));
                carSpaw.transform.parent = Car.transform;
                if (carSpaw.GetComponent<Collider>().bounds.min.z > sizeZ + 0.7f || carSpaw.GetComponent<Collider>().bounds.max.z > sizeZ + 0.7f)
                {
                    // Destroy(carSpaw);
                    
                    carSpaw.active = false;
                    
                }
                else
                {
                    carIsOnMap.Add(carSpaw);

                }
            }
            else
            {
                Debug.Log("khong trung");
            }
        }
        if (pos.z == -sizeZ - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

                
                float offSet = (g.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x, hit.point.y, hit.point.z - offSet), Quaternion.Euler(0, -90, 0));

                carSpaw.GetComponent<Car>().SetRotation(Quaternion.Euler(0, -90, 0));
                carSpaw.transform.parent = Car.transform;

                if (carSpaw.GetComponent<Collider>().bounds.min.z < -sizeZ - 0.7f || carSpaw.GetComponent<Collider>().bounds.max.z < -sizeZ - 0.7f)
                {
                    // Destroy(carSpaw);
                    
                    carSpaw.active = false;
                    
                }
                else
                {
                    carIsOnMap.Add(carSpaw);

                }
            }
            else
            {
                Debug.Log("khong trung");
            }
        }
    }*/



    public void GenCar(Vector3 pos, GameObject g)
    {

        RaycastHit hit;
        if (pos.x == sizeX )
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, layerMask))
            {


                float offSet = (g.transform.localScale.x) / 2;

                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x + offSet, hit.point.y, hit.point.z), Quaternion.Euler(0, 180, 0));

                carSpaw.GetComponent<Car>().SetRotation(Quaternion.Euler(0, 180, 0));
                carSpaw.transform.parent = Car.transform;


                Vector3 bound = carSpaw.GetComponent<Collider>().bounds.min;
                if (carSpaw.GetComponent<Collider>().bounds.min.x > sizeX + 0.7f || carSpaw.GetComponent<Collider>().bounds.max.x > sizeX + 0.7f)
                {

                    //Destroy(carSpaw);

                    carSpaw.active = false;


                }
                else
                {

                    carIsOnMap.Add(carSpaw);
                }
            }
            else
            {
                Debug.Log("khong trung");
            }
        }

        if (pos.x == - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layerMask))
            {


                float offSet = (g.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x - offSet, hit.point.y, hit.point.z), Quaternion.Euler(0, 0, 0));

                carSpaw.GetComponent<Car>().SetRotation(Quaternion.Euler(0, 0, 0));
                carSpaw.transform.parent = Car.transform;
                Vector3 bound = carSpaw.GetComponent<Collider>().bounds.min;
                if (carSpaw.GetComponent<Collider>().bounds.min.x < -sizeX - 0.7f || carSpaw.GetComponent<Collider>().bounds.max.x < -sizeX - 0.7f)
                {
                    //Destroy(carSpaw);

                    carSpaw.active = false;

                }
                else
                {
                    carIsOnMap.Add(carSpaw);

                }


            }
            else
            {
                Debug.Log("khong trung");
            }
        }

        if (pos.z == sizeZ)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
            {


                float offSet = (g.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x, hit.point.y, hit.point.z + offSet), Quaternion.Euler(0, 90, 0));

                carSpaw.GetComponent<Car>().SetRotation(Quaternion.Euler(0, 90, 0));
                carSpaw.transform.parent = Car.transform;
                if (carSpaw.GetComponent<Collider>().bounds.min.z > sizeZ + 0.7f || carSpaw.GetComponent<Collider>().bounds.max.z > sizeZ + 0.7f)
                {
                    // Destroy(carSpaw);

                    carSpaw.active = false;

                }
                else
                {
                    carIsOnMap.Add(carSpaw);

                }
            }
            else
            {
                Debug.Log("khong trung");
            }
        }
        if (pos.z == - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {


                float offSet = (g.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x, hit.point.y, hit.point.z - offSet), Quaternion.Euler(0, -90, 0));

                carSpaw.GetComponent<Car>().SetRotation(Quaternion.Euler(0, -90, 0));
                carSpaw.transform.parent = Car.transform;

                if (carSpaw.GetComponent<Collider>().bounds.min.z < -sizeZ - 0.7f || carSpaw.GetComponent<Collider>().bounds.max.z < -sizeZ - 0.7f)
                {
                    // Destroy(carSpaw);

                    carSpaw.active = false;

                }
                else
                {
                    carIsOnMap.Add(carSpaw);

                }
            }
            else
            {
                Debug.Log("khong trung");
            }
        }
    }
    [System.Obsolete]
    public GameObject PickCar(int id)
    {
       // int indx = Random.RandomRange(0, car.Count);
        //return car[indx];
        return car[id];

    }
    public void NumberOfCar()
    {
        for (int i = 0; i < carIsOnMap.Count; i++)
        {
            if (carIsOnMap[i] == null)
            {
                carIsOnMap.RemoveAt(i);
            }
        }

    }
    public void ResetMap()
    {
        CreadRoad.instance.ResetRoad();
        for (int i = 0; i < carIsOnMap.Count; i++)
        {
            Destroy(carIsOnMap[i]);
        }
        carIsOnMap.Clear();
        for (int i = 0; i < FloorGen.Count; i++)
        {
            Destroy(FloorGen[i]);
        }
        FloorGen.Clear();
        for (int i = 0; i < boderGen.Count; i++)
        {
            Destroy(boderGen[i]);
        }
        boderGen.Clear();

        positionInMap.Clear();
        positionIntialCar.Clear();

    }
    //Check vi tri cua xe co duoc phep dat vao khong
    public bool CheckCanChoose(GameObject g , Vector3 pos)
   { 
        float distanceUseble;
        RaycastHit hit;
        if (pos.x == sizeX + 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, layerMask))
            {
                distanceUseble = Vector3.Distance(pos, hit.point);
                if (distanceUseble <= g.transform.localScale.x)
                {
                   
                    return false;
                    
                }
                else
                {
                    return true;
                }
            }
           
        }

        if (pos.x == - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layerMask))
            {

                distanceUseble = Vector3.Distance(pos, hit.point);
                if (distanceUseble <= g.transform.localScale.x)
                {
                    
                    return false;

                }
                else
                {
                    return true;
                }
            }
         
        }

        if (pos.z == sizeZ + 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
            {

                distanceUseble = Vector3.Distance(pos, hit.point);
                if (distanceUseble <= g.transform.localScale.x)
                {
                    
                    return false;

                }
                else
                {
                    return true;
                }
            }
           
        }
        if (pos.z == - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

                distanceUseble = Vector3.Distance(pos, hit.point);
                if (distanceUseble <= g.transform.localScale.x)
                {
                    
                    return false;

                }
                else
                {
                    return true;
                }
            }
         
        }

        return false;

    }
    //Lay thong tin tu inspector
    public void GetInformationFromInspector()
    {
        
        numberOfCar = idCar.Length;

        for(int i = 0; i < idCar.Length; i++)
        {
            if(idCar[i] == 0)
            {
                numberCartype1++;
            }
        }
        for (int i = 0; i < idCar.Length; i++)
        {
            if (idCar[i] == 1)
            {
                numberCartype2++;
            }
        }

    }
    //Reset toan bo map
    public void ResetRandomGame()
    {
        if(FloorGen.Count == 0)
        {
            StartGenMap();
        }
        else
        {
            StartCoroutine(delay());
        }

    }
    IEnumerator delay()
    {
        
        for (int i = 0; i < carIsOnMap.Count; i++)
        {
            Destroy(carIsOnMap[i]);
        }
        carIsOnMap.Clear();

        positionIntialCar.Clear();

        yield return new WaitForSeconds(1f);
        RandomIntialPosition();

        CreateCar();

    }
    public void GenCarOnMap()
    {
        k--;
        for (int i = 0; i < carIsOnMap.Count; i++)
        {
            Destroy(carIsOnMap[i]);
        }
        carIsOnMap.Clear();
  
        positionIntialCar.Clear();

        GenBoder();
        RandomIntialPosition();

        CreateCar();
       
    }



}
