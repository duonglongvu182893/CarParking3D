using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowCar : MonoBehaviour
{
    public Vector3 offset;
    public GameObject objectCar;
    // Start  is called before the first frame update
    void Start()
    {
        offset = transform.position - objectCar.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = objectCar.transform.position + offset;
      
    }


}
