using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUIContent generateGrid = new()
        {
            text = "Generate 8x8 Grid",
            tooltip = "Automatically creates a gameplay grid at world origin"
        };

        if (GUILayout.Button(generateGrid))
        {
            CreateGridObjects();
        }
    }

    private void CreateGridObjects()
    {
        var spawnedParent = new GameObject("GameplayGrid");

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject spawnedTile = (GameObject)Instantiate(Resources.Load("TilePrefab"), spawnedParent.transform);
                spawnedTile.name = "Tile (" + ((x * 7) + y) + ")";
                spawnedTile.transform.position = new Vector2(x, y);
            }
        }

        spawnedParent.transform.position = new Vector2(0.5f, 0.5f);
    }
}
