using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzleBlockConditions
{
    public PuzzleBlock puzzleBlock;
    public List<Tile> winTiles = new();
}

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleBlockConditions> allBlockWinConditions = new();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public List<PuzzleBlockConditions> ReturnAllBlockWinConditions()
    {
        return allBlockWinConditions;
    }
}
