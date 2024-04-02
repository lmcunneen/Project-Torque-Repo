using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private PuzzleBlock[] puzzleBlockLayers;

    private int layerOrderShift = 10;
    
    void Start()
    {
        puzzleBlockLayers = FindObjectsOfType<PuzzleBlock>();

        LayerPuzzleBlocks();
    }

    void Update()
    {
        
    }

    public void MovePuzzleBlockToFront(PuzzleBlock givenBlock)
    {
        PuzzleBlock[] blocks = new PuzzleBlock[puzzleBlockLayers.Length];

        blocks[0] = givenBlock;

        int arrayShift = 1;
        
        for (int i = 0; i < puzzleBlockLayers.Length; i++)
        {
            if (puzzleBlockLayers[i] == givenBlock)
            {
                arrayShift = 0;
                continue;
            }
            
            blocks[i + arrayShift] = puzzleBlockLayers[i];
        }

        puzzleBlockLayers = blocks;

        LayerPuzzleBlocks();
    }

    private void LayerPuzzleBlocks()
    {
        for (int i = 0; i < puzzleBlockLayers.Length; i++)
        {
            puzzleBlockLayers[i].GetComponent<SpriteRenderer>().sortingOrder = layerOrderShift - i;
        }
    }
}
