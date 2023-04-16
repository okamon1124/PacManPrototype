using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{

    Texture2D map;
    GameObject CubePrefab;
    GameObject IntersectionColliderPrefab;

    [MenuItem("Window/LevelEditor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>();
    }
    
    private void OnGUI()
    {
        map = (Texture2D)EditorGUILayout.ObjectField("Add 2D map here", map, typeof(Texture2D), false);
        CubePrefab = (GameObject)EditorGUILayout.ObjectField("Drag CubePrefab", CubePrefab, typeof(GameObject), false);
        IntersectionColliderPrefab = (GameObject)EditorGUILayout.ObjectField("Drag IntersectionPrefab", IntersectionColliderPrefab, typeof(GameObject), false);

        if (GUILayout.Button("Creat Level"))
        {
            ClearPrefabs();
            GenerateLevel();
        }
        else if (GUILayout.Button("Clear level"))
        {
            ClearPrefabs();
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
        //Debug.Log(pixelColor.ToString());
        if(pixelColor.a != 0)
        {
            //Debug.Log($"PixelColorInfo R: {pixelColor.r}, G: {pixelColor.g} ,B: {pixelColor.b}");

            if (pixelColor.r == 1)
            {
                Vector3 posotion = new Vector3(x, -0.25f, y);
                Instantiate(IntersectionColliderPrefab, posotion, Quaternion.identity, Selection.transforms[0]);
            }
            else
            {
                Vector3 posotion = new Vector3(x, -0.25f, y);
                Instantiate(CubePrefab, posotion, Quaternion.identity, Selection.transforms[0]);
            }
        }
    }

    void ClearPrefabs()
    {
        int childcount = Selection.transforms[0].childCount;

        
        for (int i = childcount-1; i >= 0; i--)
        {
            //ObjectsToDestroy.Add()
            DestroyImmediate(Selection.transforms[0].GetChild(i).gameObject);
        }
    }
}
