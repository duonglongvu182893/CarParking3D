using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Car;
    public Level levelGame;

    [MenuItem("Assets / NewScriptableObject.asset")]
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void LoadMap(Level level)
    {
        TestMap.instance.sizeX = level.sizeX;
        TestMap.instance.sizeZ = level.sizeZ;

        TestMap.instance.GenBoder();
        TestMap.instance.GenFloor();

        CreadRoad.instance.Test();

        for(int i = 0; i < level.positionCarType1.Count; i++)
        {
            GameObject car = Instantiate(Car[1], level.positionCarType1[i], level.quaternionCarType1[i]);

        }
        for (int i = 0; i < level.positionCarType2.Count; i++)
        {
            GameObject car = Instantiate(Car[0], level.positionCarType2[i], level.quaternionCarType2[i]);

        }
    }
    public void CreateScriptableObject(string name)
    {
        Level levelSave = ScriptableObject.CreateInstance<Level>();
        AssetDatabase.CreateAsset(levelSave, "Assets/Level/"+name+".asset");
        AssetDatabase.SaveAssets();

    }
}
