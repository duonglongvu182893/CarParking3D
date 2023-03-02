using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class Car : MonoBehaviour 

{
    public float offSet;

    /// <summary>
    /// car size 
    /// </summary>
    public int id;

    public Quaternion rotation;

    public bool isChoseCar = false;

    public bool isStraight = false;

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
