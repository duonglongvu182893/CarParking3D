using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCreateMap : MonoBehaviour
{
    

    public float sizeX;
    public float sizeZ;
    public float offset;

    public int[] idCar;
    public int k = 1000;

    public GameObject floor;
    public GameObject boder;
    public GameObject road;

    [HideInInspector]
    public List<GameObject> car;

    public List<GameObject> carIsOnMap;

  
    
   
    public List<GameObject> positionRoad;
    public List<GameObject> positionBoder;
    public List<GameObject> positionFloor;

    public LayerMask layerMask;

    public List<Vector3> positionIntial;

    [HideInInspector]
    public GameObject carEditor;
    [HideInInspector]
    public GameObject roadEditor;
    [HideInInspector]
    public GameObject floorEditor;
    [HideInInspector]
    public GameObject boderEditor;

    private void Start()
    {
        GenFloor(offset);
        PositionIntial(offset);
        SpawCarInMap();
        if (carIsOnMap.Count != idCar.Length)
        {

            while (k > 0)
            {
                DeleteCar();
                SpawCarInMap();
                Debug.Log(carIsOnMap.Count);
                if (carIsOnMap.Count == idCar.Length)
                {
                    break;
                }
                else if (k == 0)
                {
                    Debug.Log("khong the spaw");
                    
                }
                k--;
            }
        }
    }

    //Gen ra floor va lay vi tri cua cac mieng floor
    public void GenFloor(float offset)
    {
       for(int i = 0; i < sizeZ; i++)
        {
            List<Vector3> position = BuildMap(new Vector3(i, 0, 0), new Vector3(i, 0, sizeX), offset);
            for (int j = 0; j < position.Count; j++)
            {
                GameObject floorClone = Instantiate(floor, position[j], Quaternion.identity);
                positionFloor.Add(floorClone);
                floorClone.transform.parent = floorEditor.transform;
            }

        }
    }

    //Lay ra vi tri co the ban raycast lay xe
    public void PositionIntial(float offset)
    {    
        for(int i  =  0 ; i < 2; i++)
        {
            List<Vector3> position = BuildMap(new Vector3(-1 + i * (sizeX + 1), 0, 0), new Vector3(-1 + i * (sizeX + 1), 0, sizeZ), offset);
            for (int j = 0; j < position.Count; j++)
            {
                GameObject boderClone = Instantiate(boder, position[j], Quaternion.identity);
                positionBoder.Add(boderClone);
                positionIntial.Add(boderClone.transform.position);
                boderClone.transform.parent = boderEditor.transform;
            }
        }
        for (int i = 0; i < 2; i++)
        {
            List<Vector3> position = BuildMap(new Vector3(0 , 0, -1 + i * (sizeX + 1)), new Vector3(sizeX, 0, -1 + i * (sizeX + 1)), offset);
            for (int j = 0; j < position.Count; j++)
            {
                GameObject boderClone = Instantiate(boder, position[j], Quaternion.identity);
                positionIntial.Add(boderClone.transform.position);
                boderClone.transform.parent = boderEditor.transform;
            }
        }
    }

    //Gen ra duong ngoai va lay vi tri cua tung mieng road ngoai
    public void GenRoad(float offset)
    {

        
    }

    public void SpawCarInMap()
    {
        for(int i = 0; i < idCar.Length; i++)
        {
            GenCar(idCar[i]);
        }
    }

    [System.Obsolete]
    public void GenCar(int id)
    {
        if (positionIntial.Count > 0)
        {
            int inx = Random.RandomRange(0, positionIntial.Count);
            if (CheckGenCar(positionIntial[inx], ChoseCar(id)))
            {
                //Debug.Log("da Spaw Car");
            }
            else
            {
                positionIntial.RemoveAt(inx);
                GenCar(id);
            }
        }
    }

    //Ham kiem tra vi tri gen ra Car
    public bool CheckGenCar(Vector3 position, GameObject car)
    {
        if (position.x == sizeX)
        {
            Vector3 point = RayCast(position, transform.TransformDirection(Vector3.left),layerMask);
            if (point != Vector3.negativeInfinity)
            {
                if (Vector3.Distance(position, point) > car.transform.localScale.x)
                {
                    float offSetCar = (car.transform.localScale.x) / 2;
                    Vector3 positionSpaw = new Vector3(point.x + offSetCar, point.y, point.z);
                    Quaternion quaternion = Quaternion.Euler(0, 180, 0);
                    InstancateCar(car, positionSpaw, quaternion, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
        if (position.x == -1)
        {
            Vector3 point = RayCast(position, transform.TransformDirection(Vector3.right), layerMask);
            if (point != Vector3.negativeInfinity)
            {
                if (Vector3.Distance(position, point) > car.transform.localScale.x)
                {
                    float offSetCar = (car.transform.localScale.x) / 2;
                    Vector3 positionSpaw = new Vector3(point.x - offSetCar, point.y, point.z);
                    Quaternion quaternion = Quaternion.Euler(0, 0, 0);
                    InstancateCar(car, positionSpaw, quaternion, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        if (position.z == sizeZ)
        {
            Vector3 point = RayCast(position, transform.TransformDirection(Vector3.back), layerMask);
            if (point != Vector3.negativeInfinity)
            {
                if (Vector3.Distance(position, point) > car.transform.localScale.x)
                {
                    float offSetCar = (car.transform.localScale.x) / 2;
                    Vector3 positionSpaw = new Vector3(point.x , point.y, point.z + offSetCar);
                    Quaternion quaternion = Quaternion.Euler(0, 90, 0);
                    InstancateCar(car, positionSpaw, quaternion, false);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        if (position.z == -1)
        {
            Vector3 point = RayCast(position, transform.TransformDirection(Vector3.forward), layerMask);
            if (point != Vector3.negativeInfinity)
            {
                if (Vector3.Distance(position, point) > car.transform.localScale.x)
                {
                    float offSetCar = (car.transform.localScale.x) / 2;
                    Vector3 positionSpaw = new Vector3(point.x, point.y, point.z - offSetCar);
                    Quaternion quaternion = Quaternion.Euler(0, 90, 0);
                    InstancateCar(car, positionSpaw, quaternion, false);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;

    }
    public void InstancateCar(GameObject car,Vector3 pos, Quaternion quaternion , bool straight)
    {
        GameObject carClone = Instantiate(car, pos, quaternion);
        carClone.GetComponent<Car>().SetRotation(quaternion);
        carClone.GetComponent<Car>().isStraight = straight;
        carIsOnMap.Add(carClone);
        carClone.transform.parent = carEditor.transform;
    }
    
    public Vector3 RayCast(Vector3 postion,Vector3 direction, LayerMask layerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(postion, direction, out hit, Mathf.Infinity, layerMask))
        {

            return hit.point;
        }
        else

            return Vector3.negativeInfinity;

    }

    public GameObject ChoseCar(int id)
    {
        return car[id];

    }

    public List<Vector3> BuildMap(Vector3 point1, Vector3 point2, float sizeCell)
    {
        var distance = Vector3.Distance(point1, point2);
        int countCell = (int)(distance / sizeCell);
        var cellSizeRound = distance / countCell;
        List<Vector3> pointCells = new List<Vector3>();
        for (int i = 0; i < countCell; ++i)
        {
            var step = i * cellSizeRound;
            pointCells.Add(Vector3.Lerp(point1, point2, step / distance));
        }

        return pointCells;
    }

    public void DeleteCar()
    {
        for(int i = 0; i < carIsOnMap.Count; i++)
        {
            Destroy(carIsOnMap[i]);

        }
        carIsOnMap.Clear();
        PositionIntial(offset);
    }
}   


