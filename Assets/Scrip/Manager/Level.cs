using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;


[CreateAssetMenu]

public class Level : ScriptableObject

{
    public int sizeX;
    public int sizeZ;

    public int numberCar;

    public List<Vector3> positionCarType1;
    public List<Quaternion> quaternionCarType1;

    public List<Vector3> positionCarType2;
    public List<Quaternion> quaternionCarType2;
    public List<bool> isStraightType1;
    public List<bool> isStraightType2;

    public List<Vector3> positionBlock;
    public List<Vector3> positionWall;
    public List<Quaternion> wallRotation;



    public List<Vector3> roadPostion;
}
    
