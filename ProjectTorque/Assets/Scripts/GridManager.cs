using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private PuzzleBlock[] puzzleBlockLayers;
    
    void Start()
    {
        puzzleBlockLayers = FindObjectsOfType<PuzzleBlock>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePuzzleBlockToFront(PuzzleBlock givenBlock)
    {

    }
}
