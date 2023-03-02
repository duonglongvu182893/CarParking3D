using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarMoverment : MonoBehaviour
{


    
    public bool Run;
    public bool isBlock;
    public bool isFree;
    public bool outRoad;

    public int dir;

    public bool touchRoad;

    public float offset;

    public float offsetXY;
    public float offsetYX;
    public float stop;
    public GameObject hitObj;

    public LayerMask layerMask;
    public LayerMask layerBoder;
    public LayerMask layerSelect;

    public Vector3 positionHit;
    public Vector3 positionHitInRoad;


    public static int Left = -1;
    public static int Right = 1;
    public bool tap = false;

    public Vector2 startPosition;
    public Vector2 currentPosition;
    public Vector2 endPosition;
    public bool isRun = false;
    public bool stopTouch = false;
    public bool touch = false;

    public float swipeRange;
    public float tapRange;


    public Vector3 informationForScreen;

    // Start is called before the first frame update
    void Start()
    {
        dir = Left;
        DetectObjectInRoad();
        
        if (TestMap.instance.sizeX > TestMap.instance.sizeZ)
        {
            offsetXY = TestMap.instance.sizeX - TestMap.instance.sizeZ;
            offsetYX = 0;
        }
        if (TestMap.instance.sizeX < TestMap.instance.sizeZ)
        {
            offsetXY = 0;
            offsetYX = TestMap.instance.sizeZ - TestMap.instance.sizeX;
        }
        if (TestMap.instance.sizeX == TestMap.instance.sizeZ)
        {
            offsetXY = 0;
            offsetYX = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        SwipScreen();
        DetectObjectInFront();

        //SelectFromScreen();
        DetectObjectInRoad();

    }
    private void FixedUpdate()
    {
        DetectObjectInFront();

        if (Run && isBlock)
        {

            offset = transform.localScale.x / 2;
            MoveCar(positionHit);
            CheckStop(positionHit);


        }
        else if (Run && !isBlock && !outRoad)
        {
            offset = 0f;
            MoveCar(positionHitInRoad);
            if (Vector3.Distance(transform.position, positionHitInRoad) <= 0.002f)
            {
                outRoad = true;
            }
        }
        else if (outRoad)
        {
            Run = false;
            isBlock = false;
            RotateCar();
            //CarFolowRoad();
            

        }
    }

    public void CheckStop(Vector3 position)
    {
        int direction = Mathf.RoundToInt(Vector3.Dot(dir * transform.right, Vector3.forward));
        if (direction == 0)
        {
            direction = Mathf.RoundToInt(Vector3.Dot(dir * transform.right, Vector3.right));
            if (direction == 1)
            {
                if (Vector3.Distance(transform.position, new Vector3(position.x - offset, position.y, position.z)) <= stop)
                {
                    Run = false;
                    touch = false;
                    stopTouch = false;
                }
            }
            else
            {

                if (Vector3.Distance(transform.position, new Vector3(position.x + offset, position.y, position.z)) <= stop)
                {
                    Run = false;
                    touch = false;
                    stopTouch = false;
                }


            }
        }
        else if (direction == 1)
        {
            if (Vector3.Distance(transform.position, new Vector3(position.x, position.y, position.z - offset)) <= stop)
            {
                Run = false;
                touch = false;
                stopTouch = false;
            }
        }
        else if (direction == -1)
        {
            if (Vector3.Distance(transform.position, new Vector3(position.x, position.y, position.z + offset)) <= stop)
            {
                Run = false;
                touch = false;
                stopTouch = false;
            }
        }


    }
    public void MoveCar(Vector3 position)
    {

        int direction = Mathf.RoundToInt(Vector3.Dot(dir * transform.right, Vector3.forward));
        if (direction == 0)
        {
            direction = Mathf.RoundToInt(Vector3.Dot(dir * transform.right, Vector3.right));
            if (direction == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.x - offset, position.y, position.z), Time.deltaTime * 3f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.x + offset, position.y, position.z), Time.deltaTime * 3f);
            }

        }
        else if (direction == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.x, position.y, position.z - offset), Time.deltaTime * 3f);
        }
        else if (direction == -1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.x, position.y, position.z + offset), Time.deltaTime * 3f);
        }


    }

    void DetectObjectInFront()
    {
        Vector3 direction = dir * transform.right;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (direction), out hit, 100f, layerMask))
        {
            isBlock = true;
            positionHit = hit.point;
            hitObj = hit.collider.gameObject;
        }
        else
        {
            isBlock = false;
            positionHit = Vector3.zero;
            hitObj = null;
        }
    }

    void DetectObjectInRoad()
    {
        if (!outRoad)
        {
            Vector3 direction = dir * transform.right;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (direction), out hit, 100f, layerBoder))
            {
                positionHitInRoad = hit.point;
            }
            else
            {
                //Debug.Log("khong co");
            }
        }
        

    }

    public void RotateCar()
    {
        //Car.transform.position = Vector3.MoveTowards(Car.transform.position,CreadRoad.instance.vectorPath[0], Time.deltaTime * 5f);
        //vi tri so voi diem tren cung ben trai
        float num0 = Vector3.Distance(transform.position, new Vector3(CreadRoad.instance.vectorPath[0].x - offsetYX, CreadRoad.instance.vectorPath[0].y, CreadRoad.instance.vectorPath[0].z - offsetXY));
        //vi tri so voi diem tren cung ben phai
        float num1 = Vector3.Distance(transform.position, new Vector3(CreadRoad.instance.vectorPath[1].x - offsetYX, CreadRoad.instance.vectorPath[1].y, CreadRoad.instance.vectorPath[1].z + offsetXY));
        //vi tri so voi diem duoi cung ben phai
        float num2 = Vector3.Distance(transform.position, new Vector3(CreadRoad.instance.vectorPath[2].x + offsetYX, CreadRoad.instance.vectorPath[2].y, CreadRoad.instance.vectorPath[2].z - offsetXY));
        //vi tri so voi diem duoi cung ben trai
        float num3 = Vector3.Distance(transform.position, new Vector3(CreadRoad.instance.vectorPath[3].x + offsetYX, CreadRoad.instance.vectorPath[3].y, CreadRoad.instance.vectorPath[3].z + offsetXY));

        if ((num0 < num3 && num0 < num2) && (num1 < num3 && num1 < num2))
        {

            transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0, 0));

            transform.position = Vector3.MoveTowards(transform.position, CreadRoad.instance.vectorPath[1], Time.deltaTime * 5f);

            if (Vector3.Distance(transform.position, CreadRoad.instance.vectorPath[1]) < 0.04f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0, -1f));
                transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, CreadRoad.instance.vectorPath[3], Time.deltaTime * 5f);

            }

        }

        else if ((num1 < num0 && num1 < num2) && (num3 < num0 && num3 < num2))
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0, -1f));

            transform.position = Vector3.MoveTowards(transform.position, CreadRoad.instance.vectorPath[3], Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, CreadRoad.instance.vectorPath[3]) < 0.03f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0, 0f));
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
                transform.position = Vector3.MoveTowards(transform.position, CreadRoad.instance.vectorPath[2], Time.deltaTime * 5f);

            }


        }
        else if ((num2 < num0 && num2 < num1) && (num3 < num0 && num3 < num1))
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0, 0f));

            transform.position = Vector3.MoveTowards(transform.position, CreadRoad.instance.vectorPath[2], Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, CreadRoad.instance.vectorPath[2]) < 0.03f)
            {

                transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0, 1f));
                transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, CreadRoad.instance.vectorPath[0], Time.deltaTime * 5f);

            }

        }
        else if ((num0 < num3 && num0 < num1) && (num2 < num3) && (num2 < num1))

        {
            transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0, 1f));

            transform.position = Vector3.MoveTowards(transform.position, CreadRoad.instance.vectorPath[0], Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, CreadRoad.instance.vectorPath[0]) < 0.8f)
            {

                Destroy(gameObject);

            }
        }


    }
    public void SelectFromScreen()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, layerSelect))
            {
                if (hit.collider == gameObject.GetComponent<Collider>())
                {

                    Run = true;

                }
            }
        }
    }
    public void SwipScreen()
    {
        if (!outRoad)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f, layerSelect))
                {
                    if (hit.collider == gameObject.GetComponent<Collider>())
                    {

                        touch = true;

                    }
                }
                else
                {
                    touch = false;

                }

            }
            else if (Input.GetMouseButtonUp(0))
            {
                touch = false;
            }
            if (touch)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startPosition = Input.GetTouch(0).position;
                }
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    currentPosition = Input.GetTouch(0).position;
                    Vector2 Distance = currentPosition - startPosition;


                    if (!stopTouch)
                    {
                        if (transform.GetComponent<Car>().isStraight)
                        {
                            if (Distance.x < -swipeRange)
                            {
                                informationForScreen = Vector3.left;
                                stopTouch = true;

                            }
                            if (Distance.x > swipeRange)
                            {

                                informationForScreen = Vector3.right;
                                stopTouch = true;


                            }
                        }

                        if (!transform.GetComponent<Car>().isStraight)
                        {
                            if (Distance.y > swipeRange)
                            {

                                informationForScreen = Vector3.forward;
                                stopTouch = true;

                            }
                            if (Distance.y < -swipeRange)
                            {


                                informationForScreen = Vector3.back;
                                stopTouch = true;

                            }
                        }
                    }


                }

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    stopTouch = false;

                    endPosition = Input.GetTouch(0).position;
                    Vector2 Distance = endPosition - startPosition;

                    if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
                    {
                       
                        tap = true;
                        touch = true;

                    }
                }
                if (stopTouch && touch && !Run)
                {
                    dir = Mathf.RoundToInt(Vector3.Dot(transform.right, informationForScreen));
                    Run = true;
                }
            }

        }

    }
    

}