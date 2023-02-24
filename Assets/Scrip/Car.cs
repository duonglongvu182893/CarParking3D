using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class Car : MonoBehaviour 

{
    public float offSet;

    public int id;

    public Quaternion rotation;


    public void SetRotation(Quaternion rotationQ)
    {
        
        rotation = rotationQ;
    }
    public int GetID()
    {
        return id;
    }
    public float GetOffset()
    {
        return offSet;
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }



}
