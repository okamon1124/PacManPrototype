using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{

    Texture2D map;
    GameObject CubePrefab;
    //Transform ParentTransform;

    [MenuItem("Window/LevelEditor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>();
    }
    
    private void OnGUI()
    {
        map = (Texture2D)EditorGUILayout.ObjectField("Add 2D map here", map, typeof(Texture2D), false);
        CubePrefab = (GameObject)EditorGUILayout.ObjectField("Drag Prefab here", CubePrefab, typeof(GameObject), false);
        //ParentTransform = (Transform)EditorGUILayout.ObjectField("Drag Parent here",ParentTransform, typeof(Transform), false);
        //ParentTransform = Selection.gameObjects[0].transform;

        if (GUILayout.Button("Creat Level"))
        {
            //Debug.Log("buttom pressed");
            GenerateLevel();
        }
    }

    public void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GeneratePrefab(x,y);
            }
        }
    }

    void GeneratePrefab(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        Debug.Log(pixelColor);
        if(pixelColor.a != 0)
        {
            //Debug.Log("black Dot");
            Vector3 posotion = new Vector3(x, -0.25f, y);
            Instantiate(CubePrefab, posotion, Quaternion.identity, Selection.transforms[0]);
        }
    }


}
