using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{

    public int sizeX = 1;
    public int sizeZ = 1;
    public int numberOfCar = 4;
    public int realNumberCar;

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

    //public 


    public Vector3 posIsUse;
    public Vector3 posIsUse2;

    public static CreateMap instance;
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
       
       // CreateCar(numberOfCar);


    }
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        NumberOfCar();
    }

    [System.Obsolete]
    public void CreateCar(int number)
    {
        GenFloor();

        /*
        for (int i = 0; i < number; i++)
        {
            int inx = Random.RandomRange(0, positionIntialCar.Count);
            GenCar(positionIntialCar[inx]);
            if(carIsOnMap.Count == realNumberCar)
            {
                break;
            }
        }*/
      

        for (int i = 0; i < number; i++)
        {
            /*
            int inx = Random.RandomRange(0, positionIntialCar.Count);
            GameObject CloneCar = PickCar();
            
            if (CheckCanChoose(CloneCar,positionIntialCar[inx]))
            {
                GenCar(positionIntialCar[inx], CloneCar);
            }*/
            RunAgaint();

        }


    }

    //Gen san choi
    [System.Obsolete]
    public void GenFloor()
    {
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
        }
        RandomIntialPosition();
    }


    //Vi tri xung quanh co the day xe vao
    [System.Obsolete]
    public void RandomIntialPosition()
    {
        for (int i = -sizeX - 1 ; i <= sizeX + 1; i++)
        {
            for (int j = -sizeZ - 1 ; j <= sizeZ +1 ; j++)
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
        }
        
        GenBoder();
       

    }
    public void GenBoder()
    {
        for (int i = 0; i < positionIntialCar.Count; i++)
        {
            GameObject newBoder = Instantiate(boder, positionIntialCar[i], Quaternion.identity);
            newBoder.transform.parent = Boder.transform;
        }
    }

    /*
    [System.Obsolete]
    public void GenCar(Vector3 pos)
    {


        RaycastHit hit;
        if(pos.x == sizeX + 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, layerMask))
            { 
                GameObject CloneCar = PickCar();
                float offSet = (CloneCar.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(CloneCar, new Vector3(hit.point.x + offSet, hit.point.y, hit.point.z ), Quaternion.Euler(0, 180, 0));
                carSpaw.transform.parent = Car.transform;
                Vector3 bound = carSpaw.GetComponent<Collider>().bounds.min;
                if(carSpaw.GetComponent<Collider>().bounds.min.x > sizeX + 0.5f || carSpaw.GetComponent<Collider>().bounds.max.x > sizeX+0.5f)
                {

                    Destroy(carSpaw);

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
        
        if(pos.x == -sizeX - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layerMask))
            {

                GameObject CloneCar = PickCar();
                float offSet = (CloneCar.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(CloneCar, new Vector3(hit.point.x - offSet, hit.point.y, hit.point.z), Quaternion.Euler(0, 0, 0));
                //GameObject carSpaw = Instantiate(CloneCar, hit.point, Quaternion.Euler(0, 0, 0));
                carSpaw.transform.parent = Car.transform;
                Vector3 bound = carSpaw.GetComponent<Collider>().bounds.min;
                if (carSpaw.GetComponent<Collider>().bounds.min.x < - sizeX -  0.5f || carSpaw.GetComponent<Collider>().bounds.max.x < - sizeX - 0.5f)
                {
                    Destroy(carSpaw);
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

                GameObject CloneCar = PickCar();
                float offSet = (CloneCar.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(CloneCar, new Vector3(hit.point.x, hit.point.y, hit.point.z + offSet), Quaternion.Euler(0, 90, 0));
                carSpaw.transform.parent = Car.transform;
                if (carSpaw.GetComponent<Collider>().bounds.min.z > sizeZ + 0.5f || carSpaw.GetComponent<Collider>().bounds.max.z > sizeX + 0.5f)
                {
                    Destroy(carSpaw);
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

                GameObject CloneCar = PickCar();
                float offSet = (CloneCar.transform.localScale.x) / 2;
                GameObject carSpaw = Instantiate(CloneCar, new Vector3(hit.point.x, hit.point.y, hit.point.z - offSet), Quaternion.Euler(0, -90, 0));
                carSpaw.transform.parent = Car.transform;
                
                if (carSpaw.GetComponent<Collider>().bounds.min.z < -sizeZ - 0.5f || carSpaw.GetComponent<Collider>().bounds.max.z < sizeX - 0.5f)
                {
                    Destroy(carSpaw);
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



        //
       
    }*/

    public void GenCar(Vector3 pos, GameObject g)
    {

        RaycastHit hit;
        if (pos.x == sizeX + 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, layerMask))
            {


                float offSet = (g.transform.localScale.x) / 2;

                GameObject carSpaw = Instantiate(g, new Vector3(hit.point.x + offSet, hit.point.y, hit.point.z), Quaternion.Euler(0, 180, 0));
                Debug.Log("da spaw");

                carSpaw.transform.parent = Car.transform;


                Vector3 bound = carSpaw.GetComponent<Collider>().bounds.min;
                if (carSpaw.GetComponent<Collider>().bounds.min.x > sizeX + 0.7f || carSpaw.GetComponent<Collider>().bounds.max.x > sizeX + 0.7f)
                {

                    //Destroy(carSpaw);
                    Debug.Log("xe pos" + (pos.z - 1));
                    Debug.Log(" va so voi xe min z: " + carSpaw.GetComponent<Collider>().bounds.min.z);
                    Debug.Log(" va so voi xe max z: " + carSpaw.GetComponent<Collider>().bounds.max.z);
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
                Debug.Log("da spaw");
                //GameObject carSpaw = Instantiate(CloneCar, hit.point, Quaternion.Euler(0, 0, 0));
                carSpaw.transform.parent = Car.transform;
                Vector3 bound = carSpaw.GetComponent<Collider>().bounds.min;
                if (carSpaw.GetComponent<Collider>().bounds.min.x < -sizeX - 0.7f || carSpaw.GetComponent<Collider>().bounds.max.x < -sizeX - 0.7f)
                {
                    //Destroy(carSpaw);
                    Debug.Log("xe pos" + (pos.z - 1));
                    Debug.Log(" va so voi xe min z: " + carSpaw.GetComponent<Collider>().bounds.min.z);
                    Debug.Log(" va so voi xe max z: " + carSpaw.GetComponent<Collider>().bounds.max.z);
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
                Debug.Log("da spaw");
                carSpaw.transform.parent = Car.transform;

                
                if (carSpaw.GetComponent<Collider>().bounds.min.z > sizeZ + 0.7f || carSpaw.GetComponent<Collider>().bounds.max.z > sizeZ + 0.7f)
                {
                    // Destroy(carSpaw);
                    Debug.Log("xe pos" + (pos.z - 1));
                    Debug.Log(" va so voi xe min z: " + carSpaw.GetComponent<Collider>().bounds.min.z);
                    Debug.Log(" va so voi xe max z: " + carSpaw.GetComponent<Collider>().bounds.max.z);
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
                Debug.Log("da spaw");
                carSpaw.transform.parent = Car.transform;

                if (carSpaw.GetComponent<Collider>().bounds.min.z < -sizeZ - 0.7f || carSpaw.GetComponent<Collider>().bounds.max.z < -sizeZ - 0.7f)
                {
                    // Destroy(carSpaw);
                    Debug.Log("xe pos " + pos.z + 1);
                    Debug.Log(" va so voi xe min z: " + carSpaw.GetComponent<Collider>().bounds.min.z);
                    Debug.Log(" va so voi xe max z: " + carSpaw.GetComponent<Collider>().bounds.max.z);
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
    public void RunAgaint()
    {
        int inx = Random.RandomRange(0, positionIntialCar.Count);
        GameObject CloneCar = PickCar();

        if (CheckCanChoose(CloneCar, positionIntialCar[inx]))
        {
            GenCar(positionIntialCar[inx], CloneCar);

        }
        else
        {
            RunAgaint();
        }
    }


    [System.Obsolete]
    public GameObject PickCar()
    {
        int indx = Random.RandomRange(0, car.Count);
        return car[indx];
    }
    public void NumberOfCar()
    {
        for(int i = 0; i < carIsOnMap.Count; i++)
        {
            if(carIsOnMap[i] == null)
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
        for (int i = 0; i < carIsOnMap.Count; i++)
        {
            if (carIsOnMap[i] == null)
            {
                carIsOnMap.RemoveAt(i);
            }
        }
        for (int i = 0; i < FloorGen.Count; i++)
        {
            Destroy(FloorGen[i]);
        }
        for (int i = 0; i < FloorGen.Count; i++)
        {
            if (FloorGen[i] == null)
            {
                FloorGen.RemoveAt(i);
            }
        }
        for (int i = 0; i < positionInMap.Count; i++)
        {

            positionInMap.RemoveAt(i);
        }
        for (int i = 0; i < positionIntialCar.Count; i++)
        {

            positionIntialCar.RemoveAt(i);
        }

    }
    public void ResetCar()
    {
        for (int i = 0; i < carIsOnMap.Count; i++)
        {
            Destroy(carIsOnMap[i]);
        }
        for (int i = 0; i < carIsOnMap.Count; i++)
        {
            if (carIsOnMap[i] == null)
            {
                carIsOnMap.RemoveAt(i);
            }
        }
    }

    public bool CheckCanChoose(GameObject g, Vector3 pos)
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
                    Debug.Log(false);
                    return false;

                }
                else
                {
                    return true;
                }
            }

        }

        if (pos.x == -sizeX - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layerMask))
            {

                distanceUseble = Vector3.Distance(pos, hit.point);
                if (distanceUseble <= g.transform.localScale.x)
                {
                    Debug.Log(false);
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
                    Debug.Log(false);
                    return false;

                }
                else
                {
                    return true;
                }
            }

        }
        if (pos.z == -sizeZ - 1)
        {
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

                distanceUseble = Vector3.Distance(pos, hit.point);
                if (distanceUseble <= g.transform.localScale.x)
                {
                    Debug.Log(false);
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

}
