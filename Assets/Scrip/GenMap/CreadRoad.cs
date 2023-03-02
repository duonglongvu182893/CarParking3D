using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreadRoad : MonoBehaviour
{
    public int rows;
    public int coloums;
    public int scale = 1;
    public GameObject gridPrefab;


    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);
    public List<Vector3> positionIntial = new List<Vector3>();
    List<Vector3> positionMap = new List<Vector3>();
    public List<Vector3> vectorPath = new List<Vector3>();
    public List<GameObject> Road = new List<GameObject>();

    public Vector3 upVector= Vector3.zero;
    public Vector3 leftVector = Vector3.zero;
    public Vector3 rightVector = Vector3.zero;
    public Vector3 downVector = Vector3.zero;


    public static CreadRoad instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update

    void Start()
    {
       
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    void GeneateGrid()
    {
        
        for (int i = -1; i <= rows -1 ; i++)
        {
            for (int j = -1; j <= coloums -1; j++)
            {
                positionMap.Add(new Vector3(i, 0, j));

            }
        }

        for (int i = - 2; i <= rows ; i++)
        {
            for (int j = - 2; j <= coloums ; j++)
            {
                if ((i != - 2 || j != - 2) && (i != - 2 || j != coloums ) && (i != rows  || j != coloums ) && (i != rows  || j != - 2))
                {

                    positionIntial.Add(new Vector3(i, 0, j));

                }
                else
                {
                    vectorPath.Add(new Vector3(i, 0, j));
                    positionIntial.Add(new Vector3(i, 0, j));
                }


            }
        }

        for (int i = 0; i < positionIntial.Count; i++)
        {
            for (int j = 0; j < positionMap.Count; j++)
            {
                if (positionMap[j] == positionIntial[i])
                {
                    positionIntial.RemoveAt(i);
                }


            }
        }
        GenRoad();

    }
    void GenRoad()
    {
        for(int i = 0; i < positionIntial.Count; i++)
        {
            GameObject roadSpaw = Instantiate(gridPrefab, positionIntial[i], Quaternion.identity);
           
            roadSpaw.transform.parent = gameObject.transform;

            Road.Add(roadSpaw);
        }
    }
    public void CreateRoad()
    {
        rows = CreateMap.instance.sizeX + 1;
        coloums = CreateMap.instance.sizeZ + 1;
        GeneateGrid();
        //CreateVector();     

    }



    public void ResetRoad()
    {

        positionIntial.RemoveRange(0, positionIntial.Count);
        
        positionMap.RemoveRange(0, positionMap.Count);

        vectorPath.RemoveRange(0, vectorPath.Count);

        for(int i = 0; i < Road.Count; i++)
        {
            Destroy(Road[i]);
        }
        Road.RemoveRange(0, Road.Count);
    }


    public void Test()
    {
        /*
        rows = TestMap.instance.sizeX + 1;
        coloums = TestMap.instance.sizeZ + 1;*/

        rows = TestMap.instance.sizeX +1 ;
        coloums = TestMap.instance.sizeZ  +1;
        GeneateGrid();
       // CreateVector();
    }
}
