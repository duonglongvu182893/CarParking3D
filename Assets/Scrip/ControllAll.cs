using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ControllAll : MonoBehaviour
{


    //public Level levelLoad = new Level();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

     
    }
    
    public void Save(Level levelSave)

    {

        levelSave.sizeX = TestMap.instance.sizeX;
        levelSave.sizeZ = TestMap.instance.sizeZ;

        for(int i = 0; i < TestMap.instance.carIsOnMap.Count;i++)
        {
            if (TestMap.instance.carIsOnMap[i].GetComponent<Car>().GetID() == 1)
            {
                levelSave.positionCarType1.Add(TestMap.instance.carIsOnMap[i].transform.position);
                levelSave.quaternionCarType1.Add(TestMap.instance.carIsOnMap[i].GetComponent<Car>().GetRotation());

            }
            if (TestMap.instance.carIsOnMap[i].GetComponent<Car>().GetID() == 0)
            {
                levelSave.positionCarType2.Add(TestMap.instance.carIsOnMap[i].transform.position);
                levelSave.quaternionCarType2.Add(TestMap.instance.carIsOnMap[i].GetComponent<Car>().GetRotation());
            }
        }
        levelSave.numberCar = TestMap.instance.numberOfCar;
    }
    
}
