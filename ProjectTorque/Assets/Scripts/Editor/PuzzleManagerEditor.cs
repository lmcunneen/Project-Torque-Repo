using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PuzzleManager))]
public class PuzzleManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUIContent recolourPuzzleBlocks = new()
        {
            text = "Recolour Puzzle Blocks",
            tooltip = "Recolours every tile to fit its PuzzleBlock colour"
        };

        if (GUILayout.Button(recolourPuzzleBlocks))
        {
            PuzzleManager managerComponent = (PuzzleManager)target;

            var conditionList = managerComponent.ReturnAllBlockWinConditions();

            var listOfAllTiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);

            foreach(var tile in listOfAllTiles)
            {
                tile.GetComponent<SpriteRenderer>().color = Color.white;
            }

            foreach(var condition in conditionList)
            {
                foreach(var tile in condition.winTiles)
                {
                    tile.GetComponent<SpriteRenderer>().color = condition.puzzleBlock.GetComponent<SpriteRenderer>().color;
                }
            }
        }
    }
}
