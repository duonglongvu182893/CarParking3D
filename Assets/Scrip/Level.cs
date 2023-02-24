using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Map Save", menuName ="Level" )]
public class Level : ScriptableObject
{
    public int sizeX;
    public int sizeZ;

    public int numberCar;

    public List<GameObject> carType1;
    public List<GameObject> carType2;



}
