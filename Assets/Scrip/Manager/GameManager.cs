using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Car;
    public GameObject block;
    public GameObject wall;
    public GameObject floor;
    public GameObject road;
    public GameObject turnRoad;

    public GameObject carObj;
    public GameObject fenceObj;
    public GameObject floorObj;
    public GameObject roadObj;
    public int level;

    [System.Obsolete]
    void Start()
    {
        ReadDataFromSO("1");
    }

   
    void Update()
    {



    }

    //doc information tu SO va tao map
    [System.Obsolete]
    public void ReadDataFromSO(string level)
    {

        string path = "Assets/Level/level" + level + ".asset";
        // Load ScriptableObject
        Level scriptableObject = AssetDatabase.LoadAssetAtPath<Level>(path);

        // Kiểm tra xem đối tượng đã được load thành công hay chưa
        if (scriptableObject == null)
        {
            Debug.LogError("Could not load ScriptableObject at path: " + path);
            return;
        }

        // Sử dụng đối tượng ScriptableObject đã được load
        TestMap.instance.sizeX = scriptableObject.sizeX;
        TestMap.instance.sizeZ = scriptableObject.sizeZ;

        GenFloor(scriptableObject.sizeX, scriptableObject.sizeZ);

        CreadRoad.instance.GetInformation();

        
        for(int i = 0; i < scriptableObject.roadPostion.Count; i++)
        {
            if (scriptableObject.roadPostion[i].x == -2 && scriptableObject.roadPostion[i].z == -2)
            {

                GameObject roadClone = Instantiate(turnRoad, scriptableObject.roadPostion[i], Quaternion.identity);
                roadClone.transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0, 0));
                roadClone.transform.parent = roadObj.transform;
            }
            else if (scriptableObject.roadPostion[i].x == -2 && scriptableObject.roadPostion[i].z == TestMap.instance.sizeZ + 1)
            {

                GameObject roadClone = Instantiate(turnRoad, scriptableObject.roadPostion[i], Quaternion.identity);
                roadClone.transform.parent = roadObj.transform;
            }
            else if (scriptableObject.roadPostion[i].z == -2 && scriptableObject.roadPostion[i].x == TestMap.instance.sizeX + 1)
            {

                GameObject roadClone = Instantiate(turnRoad, scriptableObject.roadPostion[i], Quaternion.identity);
                roadClone.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
                roadClone.transform.parent = roadObj.transform;
            }
            else if (scriptableObject.roadPostion[i].z == TestMap.instance.sizeZ + 1 && scriptableObject.roadPostion[i].x == TestMap.instance.sizeX + 1)
            {

                GameObject roadClone = Instantiate(turnRoad, scriptableObject.roadPostion[i], Quaternion.identity);
                roadClone.transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                roadClone.transform.parent = roadObj.transform;
            }
            else if ((scriptableObject.roadPostion[i].z == -2 )|| scriptableObject.roadPostion[i].z == TestMap.instance.sizeZ + 1)
            {
                GameObject roadClone = Instantiate(road, scriptableObject.roadPostion[i], Quaternion.identity);
                roadClone.transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0, 0));
                roadClone.transform.parent = roadObj.transform;
            }
           
            else
            {
                GameObject roadClone = Instantiate(road, scriptableObject.roadPostion[i], Quaternion.identity);
                roadClone.transform.parent = roadObj.transform;
            }
            

        }

        for (int i = 0; i < scriptableObject.positionCarType1.Count; i++)
        {
            GameObject car = Instantiate(Car[1], scriptableObject.positionCarType1[i], scriptableObject.quaternionCarType1[i]);
            car.transform.GetComponent<Car>().isStraight = scriptableObject.isStraightType1[i];
            //car.transform.GetComponent<Car>().rotation = scriptableObject.quaternionCarType1[i];
            TestMap.instance.carIsOnMap.Add(car);
            car.transform.parent = carObj.transform;


        }
        for (int i = 0; i < scriptableObject.positionCarType2.Count; i++)
        {
            GameObject car = Instantiate(Car[0], scriptableObject.positionCarType2[i], scriptableObject.quaternionCarType2[i]);
            TestMap.instance.carIsOnMap.Add(car);
            car.transform.parent = carObj.transform;
            car.transform.GetComponent<Car>().isStraight = scriptableObject.isStraightType2[i];

        }
        for (int i = 0; i < scriptableObject.positionBlock.Count; i++)
        {
            GameObject blockClone = Instantiate(block, scriptableObject.positionBlock[i], Quaternion.identity);
            TestMap.instance.transform.parent = TestMap.instance.blockEditor.transform;
        }
        for (int i = 0; i < scriptableObject.positionWall.Count; i++)
        {
            GameObject wallClone = Instantiate(wall, scriptableObject.positionWall[i], scriptableObject.wallRotation[i]);
            wallClone.transform.parent = fenceObj.transform;

        }

    }

    //Gen floor
    public void GenFloor(int sizeX, int sizeZ)
    {
        for (int i = 0; i <= sizeX - 1; i++)
        {
            for (int j = 0; j <= sizeZ - 1; j++)
            {
                Vector3 pos = new Vector3(i, 0, j);
                GameObject firtBlock = Instantiate(floor, pos, Quaternion.identity);
                firtBlock.transform.parent = floorObj.transform;
            }
        }
       
    }

   
   
  
}
