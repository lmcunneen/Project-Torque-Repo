using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzleBlockState
{
    public PuzzleBlock puzzleBlock;
    public Vector3 position;
    public Quaternion rotation;
}

public class ReplayManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleBlockState> startingPositions = new();

    [SerializeField] private List<PuzzleBlockState> movedPositions = new();
    [SerializeField] private int movedListIndex = -1;

    public void InitialiseStartingPosition(PuzzleBlock givenBlock, Vector3 givenPosition, Quaternion givenRotation, Vector3 targetPosition, Quaternion targetRotation)
    {
        var startState = GeneratePuzzleBlockState(givenBlock, givenPosition, givenRotation);

        startingPositions.Add(startState);
    }

    private PuzzleBlockState GeneratePuzzleBlockState(PuzzleBlock block, Vector3 position, Quaternion rotation)
    {
        PuzzleBlockState newState = new();

        newState.puzzleBlock = block;
        newState.position = position;
        newState.rotation = rotation;

        return newState;
    }

    public void AddNewPuzzleBlockState(PuzzleBlock givenBlock, Vector3 givenPosition, Quaternion givenRotation, Vector3 targetPosition, Quaternion targetRotation)
    {
        var newState = GeneratePuzzleBlockState(givenBlock, givenPosition, givenRotation);
        
        if (movedListIndex < movedPositions.Count - 1)
        {
            //Debug.Log("Clearing List from front onwards!");
            movedPositions.RemoveRange(movedListIndex + 1, movedPositions.Count - movedListIndex - 1);
        }

        movedPositions.Add(newState);

        movedListIndex++;
    }

    public void MoveToPreviousState()
    {
        movedListIndex--;
        
        if(movedListIndex <= -2)
        {
            movedListIndex = -1;
            return;
        }
        
        if(movedListIndex == -1)
        {
            ResetAllToStartPosition();
            return;
        }

        PuzzleBlockState previousState = movedPositions[movedListIndex];

        previousState.puzzleBlock.SetCurrentTransformAndTargetTransform(previousState);
    }

    private void ResetAllToStartPosition()
    {
        foreach(var state in startingPositions)
        {
            state.puzzleBlock.transform.position = state.position;
            state.puzzleBlock.transform.rotation = state.rotation;
            return;
        }
    }

    public void MoveToNextState()
    {
        movedListIndex++;

        if (movedListIndex >= movedPositions.Count)
        {
            movedListIndex--;
            return;
        }

        PuzzleBlockState nextState = movedPositions[movedListIndex];

        nextState.puzzleBlock.SetCurrentTransformAndTargetTransform(nextState);
    }
}
