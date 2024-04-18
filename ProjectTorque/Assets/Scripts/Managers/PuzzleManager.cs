using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PuzzleBlockConditions
{
    public PuzzleBlock puzzleBlock;
    public List<Tile> winTiles = new();
    public bool hasMetCondition = false;
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

    public void CheckForWinCondition(PuzzleBlock givenBlock)
    {
        foreach(var condition in allBlockWinConditions)
        {
            if (condition.puzzleBlock.GetInstanceID() != givenBlock.GetInstanceID()) { continue; }

            bool tileAlignment = condition.winTiles.All(givenBlock.GetComponent<OverlapTileChecker>().ReturnCurrentTiles().Contains);

            if (tileAlignment)
            {
                condition.hasMetCondition = true;

                CheckForLevelComplete();
            }

            else
            {
                condition.hasMetCondition = false;
            }
        }
    }

    private void CheckForLevelComplete()
    {
        foreach(var condition in allBlockWinConditions)
        {
            if(!condition.hasMetCondition) { return; }
        }

        Debug.Log("Level Complete!");
    }
}
