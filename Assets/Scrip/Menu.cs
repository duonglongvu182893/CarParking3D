using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{

    public TextMeshProUGUI numberOfCar;

    public TMP_InputField  size;
    public TMP_InputField numberCarWant;

    public GameObject setMenu;
    public GameObject Screen;

    public GameObject Camera;

    public int a;
    public int car;
    public Vector3 positionOldCam;


    // Start is called before the first frame update

    [System.Obsolete]
    void Update()
    {
        ShowInformation();
        CheckWin();
        
        
    }
    public void Start()
    {

        positionOldCam = Camera.transform.position;
    }

    [System.Obsolete]
    public void CheckWin()
    {
        if(CreateMap.instance.carIsOnMap.Count == 0)
        {
            ResetMap();
        }
    }

    //[System.Obsolete]
    [System.Obsolete]
    public void BeginGame()
    {
        
        CreateMap.instance.sizeX = int.Parse(size.text);
        CreateMap.instance.sizeZ = int.Parse(size.text);
        CreateMap.instance.numberOfCar = int.Parse(numberCarWant.text);
        CreateMap.instance.StartGenMap();
        //CreateMap.instance.CreateCar(CreateMap.instance.numberOfCar);

        Camera.transform.position = new Vector3(positionOldCam.x, positionOldCam.y + 3f * CreateMap.instance.sizeX, positionOldCam.z);
        setMenu.SetActive(false);

    }

    [System.Obsolete]
    public void ShowInformation()
    {
        string number = CreateMap.instance.carIsOnMap.Count.ToString();
        numberOfCar.SetText(number);
        Screen.SetActive(!setMenu.active);
    }

    [System.Obsolete]
    public void ResetMap()
    {
        CreateMap.instance.ResetMap();
        Screen.SetActive(false);
        setMenu.SetActive(!Screen.active);
    }

   
}
