using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CarSetting : MonoBehaviour
{


    public GameObject vector;

    public bool isRunning = false;
    public bool stopCar = false;
   
    public bool isFree = false;

    public bool setRun = false;
    
    public Rigidbody myRb;
    public LayerMask layerMask;


    public LayerMask layerSelect;

    public Vector3 goald;
    public Vector3 distance = Vector3.zero;
    public GameObject head;
    public GameObject Car;
    public float stop;
    public bool outRoad = false;
    public bool canRun = false;

    public Transform vectorBefore;
    public Transform vectorAfter;

    public Vector3 positionInRoadup;
    public Vector3 positionInRoaddown;
    // Start is called before the first frame update

    public static CarSetting instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        distance = transform.GetComponent<Collider>().bounds.max - transform.position;
        
        ShowPoint();
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        //Check Car running
        if (isRunning && !stopCar )
        {
            CheckStop();
            CarRun();
            Vector3 direction = head.transform.position - transform.position;
            
            if (isFree)
            {
                Debug.Log("Chay thoe velo");
                myRb.velocity = direction * 2f;

            }
            else if (!isFree)
            {
                MoveCarToPosition(goald);
                Debug.Log("Chay theo gold ");
                

            }

        }
        else if (Vector3.Distance(goald, head.transform.position) < stop)
        {
            myRb.velocity = Vector3.zero;
            isRunning = false;

        }
        //Check When car go out of parking 
        if (Vector3.Distance(positionInRoadup, head.transform.position) < stop)
        {
            myRb.velocity = Vector3.zero;
            isRunning = false;
            outRoad = true;


        }

        else if (Vector3.Distance(positionInRoaddown, head.transform.position) < stop)
        {
            myRb.velocity = Vector3.zero;
            isRunning = false;
            outRoad = true;
        }

        //StopCar
        



    }
    private void Update()
    {


        if (stopCar)
        {
            isRunning = false;
            myRb.velocity = Vector3.zero;

        }
        if (outRoad)
        {
            RotateCar();
        }

        selectFromScreen();
        CheckStop();
    }

    [System.Obsolete]
    public void MoveCarToPosition(Vector3 target)
    {

        
        
        Vector3 direction = vector.transform.position - transform.position; 
        if(Vector3.Distance(target, head.transform.position) >=  stop)
        {

            myRb.velocity = direction * 2f;

           
        }
       
        //target = CheckIsBlock();


    }
   
    [System.Obsolete]
    public void CarRun()
    {
      
        //Vector3 direction = new Vector3(vector.transform.position.x - transform.position.x, 0f, vector.transform.position.z - transform.position.z);
        Vector3 direction = new Vector3(vectorAfter.transform.position.x - vectorBefore.transform.position.x, 0f, vectorAfter.transform.position.z - vectorBefore.transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(vectorBefore.transform.position, (direction), out hit, 10f, layerMask))
        {
            isFree = false;
            goald = hit.point;
            isRunning = false;
        }
        else
        {
            Debug.Log("phia truoc khong bi chan dau");
            isFree = true;
            isRunning = true;
        }
    }

    public void CheckStop()
    {
        Vector3 direction = new Vector3(vectorAfter.transform.position.x - vectorBefore.transform.position.x, 0f, vectorAfter.transform.position.z - vectorBefore.transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(vectorBefore.transform.position, (direction), out hit, 10f, layerMask))
        {
            if (Vector3.Distance(hit.point, head.transform.position) <= stop)
            {
                stopCar = true;
            }
            else
            {
                stopCar = false;
            }
        }
        else
        {
            
        }
    }

    public void selectFromScreen()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f, layerSelect))
            {
                if (hit.collider == gameObject.GetComponent<Collider>())
                {
                    isRunning = true;
                }
            }
        }
    }
    public void ShowPoint()
    {
        Vector3 direction = new Vector3(head.transform.position.x - transform.position.x, 0f, head.transform.position.z - transform.position.z);

        if (Mathf.RoundToInt(vector.transform.position.x) - Mathf.RoundToInt(transform.position.x) == 0)
        {
            positionInRoadup.x = vector.transform.position.x;
            positionInRoaddown.x = vector.transform.position.x;
            positionInRoadup.z = CreadRoad.instance.upVector.z;
            positionInRoaddown.z = CreadRoad.instance.downVector.z;

        }
        if (Mathf.RoundToInt(vector.transform.position.z) - Mathf.RoundToInt(transform.position.z) == 0)
        {
            positionInRoaddown.z = vector.transform.position.z;
            positionInRoadup.z = vector.transform.position.z;
            positionInRoadup.x = CreadRoad.instance.rightVector.x;
            positionInRoaddown.x = CreadRoad.instance.leftVector.x;

        }

    }
    public void RotateCar()
    {
        //Car.transform.position = Vector3.MoveTowards(Car.transform.position,CreadRoad.instance.vectorPath[0], Time.deltaTime * 5f);
        //vi tri so voi diem tren cung ben trai
        float num1 = Vector3.Distance(Car.transform.position, CreadRoad.instance.vectorPath[0]);
        //vi tri so voi diem tren cung ben phai
        float num2 = Vector3.Distance(Car.transform.position, CreadRoad.instance.vectorPath[1]);
        //vi tri so voi diem duoi cung ben phai
        float num3 = Vector3.Distance(Car.transform.position, CreadRoad.instance.vectorPath[2]);
        //vi tri so voi diem duoi cung ben trai
        float num4 = Vector3.Distance(Car.transform.position, CreadRoad.instance.vectorPath[3]);

        
        if ((num1 < num4 && num1 < num3)&&(num2 < num4 && num2 < num3)) 
        {
            Car.transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0, 0));
            Car.transform.position = Vector3.MoveTowards(Car.transform.position, CreadRoad.instance.vectorPath[1], Time.deltaTime * 5f);

            if (Vector3.Distance(Car.transform.position, CreadRoad.instance.vectorPath[1]) < 0.3f)
            {
                Car.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0, -1f));
                Car.transform.position = Vector3.MoveTowards(Car.transform.position, CreadRoad.instance.vectorPath[3], Time.deltaTime * 5f);

            }




        }
        
        else if ((num2 < num1 && num2 < num3)&& (num4 < num1 && num4 < num3))
        {
            Car.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0, -1f));
            Car.transform.position = Vector3.MoveTowards(Car.transform.position, CreadRoad.instance.vectorPath[3], Time.deltaTime * 5f);
            if (Vector3.Distance(Car.transform.position, CreadRoad.instance.vectorPath[3]) < 0.3f)
            {
                Car.transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0, 0f));
                Car.transform.position = Vector3.MoveTowards(Car.transform.position, CreadRoad.instance.vectorPath[2], Time.deltaTime * 5f);

            }




        }
        else if ((num3 < num1 && num3 < num2)&& (num4 < num1 && num4 < num2))
        {
                Car.transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0, 0f));
                Car.transform.position = Vector3.MoveTowards(Car.transform.position, CreadRoad.instance.vectorPath[2], Time.deltaTime * 5f);
                if (Vector3.Distance(Car.transform.position, CreadRoad.instance.vectorPath[2]) < 0.3f)
                {
                   
                    Car.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0, 1f));
                    Car.transform.position = Vector3.MoveTowards(Car.transform.position, CreadRoad.instance.vectorPath[0], Time.deltaTime * 5f);

                }
        
        }
        else if ((num1 < num4 && num1 < num2) && (num3 < num4) && (num3 < num4))
      
        {
            Car.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0, 1f));
            Car.transform.position = Vector3.MoveTowards(Car.transform.position, CreadRoad.instance.vectorPath[0], Time.deltaTime * 5f);
            if (Vector3.Distance(Car.transform.position, CreadRoad.instance.vectorPath[0]) < 0.3f)
            {

                Destroy(gameObject);

            }



        }


    }
 
}
