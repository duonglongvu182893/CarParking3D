using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadOnMap 
{
    int id;
    float length;
    float sizeOnBrick;
    int numberBrick;
    List<GameObject> objectInRoad;
    bool canUse;

     public RoadOnMap()
    {
        canUse = true;
        
    }
    public void SetId(int rowCol)
    {
        id = rowCol;

    }
    public int GetID()
    {
        return id;
    }
    public void SetInfor(int idRC, float offsetSize, int number, bool use)
    {
        int id = idRC;
        sizeOnBrick = offsetSize;
        numberBrick = number;

        canUse = use;
        GetLength();

        Debug.Log("id:" + id + ", length:" + GetLength());
    }
    
    public void SetObject(GameObject g)
    {
        objectInRoad.Add(g);
    }
    public float GetLength()
    {
        length = (numberBrick * sizeOnBrick);
        return length;
    }
    public void AddObject(GameObject g)
    {
        if(GetLength() > GetUsebleLength())
        {
            SetObject(g);
            canUse = true;
            Debug.Log("da them");
        }
        else
        {
            canUse = false;
            Debug.Log("khogn the them");
        }
    }

    public bool GetUseable()
    {
        return canUse;
    }

    public float GetUsebleLength()
    {
        float sizeObj = 0f;
        for(int i = 0; i < objectInRoad.Count; i++)
        {
            sizeObj = sizeObj + objectInRoad[i].transform.localScale.x;
        }
       
        return length - sizeObj;
    }

    
    
    
}
