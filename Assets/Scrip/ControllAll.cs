using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;



public class ControllAll : MonoBehaviour
{


    public TMP_InputField nameSave;
    public TMP_InputField nameLoad;


    public List<GameObject> Car;
    public GameObject saveDialog;
    public GameObject loadDialog;

    public GameObject carObj;



    //public Level levelLoad = new Level();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SaveDialog()
    {
        saveDialog.SetActive(!saveDialog.active);
    }
    public void LoadDialog()
    {
        loadDialog.SetActive(!loadDialog.active);
    }

    public void SaveDataToSO()

    {
        Level scriptableObject = ScriptableObject.CreateInstance<Level>();

        AssetDatabase.CreateAsset(scriptableObject, "Assets/Level/" + nameSave.text + ".asset");

        scriptableObject.sizeX = TestMap.instance.sizeX;
        scriptableObject.sizeZ = TestMap.instance.sizeZ;
        scriptableObject.numberCar = TestMap.instance.numberOfCar;
        scriptableObject.positionCarType1 = new List<Vector3>();
        scriptableObject.positionCarType2 = new List<Vector3>();
        scriptableObject.quaternionCarType1 = new List<Quaternion>();
        scriptableObject.quaternionCarType2 = new List<Quaternion>();
        for (int i = 0; i < TestMap.instance.carIsOnMap.Count; i++)
        {
            if (TestMap.instance.carIsOnMap[i].GetComponent<Car>().GetID() == 1)
            {
                scriptableObject.positionCarType1.Add(TestMap.instance.carIsOnMap[i].transform.position);
                scriptableObject.quaternionCarType1.Add(TestMap.instance.carIsOnMap[i].GetComponent<Car>().GetRotation());

            }
            if (TestMap.instance.carIsOnMap[i].GetComponent<Car>().GetID() == 0)
            {
                scriptableObject.positionCarType2.Add(TestMap.instance.carIsOnMap[i].transform.position);
                scriptableObject.quaternionCarType2.Add(TestMap.instance.carIsOnMap[i].GetComponent<Car>().GetRotation());
            }
        }
        EditorUtility.SetDirty(scriptableObject);
        JsonUtility.ToJson(scriptableObject);
        AssetDatabase.SaveAssets();

        saveDialog.SetActive(!saveDialog.active);
    }


    public void ReadDataFromSO()
    {

        string path = "Assets/Level/" + nameLoad.text + ".asset";
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

        TestMap.instance.GenBoder();
        TestMap.instance.GenFloor();

        CreadRoad.instance.Test();

        for (int i = 0; i < scriptableObject.positionCarType1.Count; i++)
        {
            GameObject car = Instantiate(Car[1], scriptableObject.positionCarType1[i], scriptableObject.quaternionCarType1[i]);
            TestMap.instance.carIsOnMap.Add(car);
            car.transform.parent = carObj.transform;

        }
        for (int i = 0; i < scriptableObject.positionCarType2.Count; i++)
        {
            GameObject car = Instantiate(Car[0], scriptableObject.positionCarType2[i], scriptableObject.quaternionCarType2[i]);
            TestMap.instance.carIsOnMap.Add(car);
            car.transform.parent = carObj.transform;

        }
        loadDialog.SetActive(!loadDialog.active);
    }
}