using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ControllAll : MonoBehaviour
{


    //public Level levelLoad = new Level();
    // Start is called before the first frame update
    void Start()
    {
        Save();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void Save()
    {
        Level asset = ScriptableObject.CreateInstance<Level>();
        
    }
  
}
