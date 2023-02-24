using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    internal static object activeObject;
    public float rayLeangth;
    public LayerMask layerMask;


    public bool left = false;
    public bool right = false;
    public bool up = false;
    public bool down = false;
    public bool tap = false;


    public Vector2 startPosition;
    public Vector2 currentPosition;
    public Vector2 endPosition;
    public bool isRun = false;
    public bool stopTouch = false;
    public bool touch = false;

    public float swipeRange;
    public float tapRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {




    }

}