using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private PuzzleBlock[] puzzleBlockLayers;

    private List<Tile> allOverlappedTiles = new();

    private int layerOrderShift = 10;
    
    void Start()
    {
        puzzleBlockLayers = FindObjectsOfType<PuzzleBlock>();

        LayerPuzzleBlocks();
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

    public void UpdateAllOverlappedTiles(List<Tile> oldTiles, List<Tile> newTiles)
    {
        foreach(var tile in newTiles)
        {
            allOverlappedTiles.Add(tile);
        }

        if (oldTiles.Count >= 1)
        {
            foreach(var tile in oldTiles)
            {
                allOverlappedTiles.Remove(tile);
            }
        }

        Debug.Log("All have been updated now :)");
    }

    public bool CheckIfTileIsOverlapped(Tile givenTile)
    {
        foreach(var tile in allOverlappedTiles)
        {
            if (tile.GetInstanceID() == givenTile.GetInstanceID())
            {
                return true;
            }
        }

        return false;
    }
}
