using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float offset;
    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position.y - floor.transform.position.y;

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f * CreateMap.instance.sizeX * 0.5f, transform.position.z);

    }
}
