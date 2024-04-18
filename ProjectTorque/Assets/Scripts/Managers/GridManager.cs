using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    }

    public bool CheckIfTileIsOverlapped(Tile givenTile, List<Tile> overlapsToIgnore)
    {
        List<Tile> tilesToCheck = allOverlappedTiles.Except(overlapsToIgnore).ToList();

        PrintList("Overlaps", overlapsToIgnore);
        PrintList("Tiles To Check", tilesToCheck);
        
        foreach(var tile in tilesToCheck)
        {
            if (tile.GetInstanceID() == givenTile.GetInstanceID())
            {
                return true;
            }
        }

        return false;
    }

    private void PrintList(string name, List<Tile> list)
    {
        string debugMessage = name + ":\n";
        
        foreach (var thing in list)
        {
            debugMessage += thing.ToString() + '\n';
        }

        Debug.Log(debugMessage);
    }
}
