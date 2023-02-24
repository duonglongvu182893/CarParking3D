using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllAll : MonoBehaviour
{
    public bool Delete;
    public int k = 10;
    // Start is called before the first frame update
    void Start()
    {
        TestMap.instance.StartGenMap();
        Debug.Log(TestMap.instance.carIsOnMap.Count);
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (TestMap.instance.carIsOnMap.Count != TestMap.instance.numberOfCar)
        {

            while (k > 0)
            {
                //k--;
                TestMap.instance.ResetMap();
                StartCoroutine(Delay());
                Debug.Log(TestMap.instance.carIsOnMap.Count);
                if (TestMap.instance.carIsOnMap.Count == TestMap.instance.numberOfCar)
                {
                    break;
                }

                if (k == 0)
                {
                    Debug.Log("khong the gen");
                }
            }  
        

        }*/
        
    }
  
}
