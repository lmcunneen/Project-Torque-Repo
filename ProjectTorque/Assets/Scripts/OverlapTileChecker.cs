using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapTileChecker : MonoBehaviour
{
    private List<Tile> previousOverlapTiles = new();
    private List<Tile> currentOverlappedTiles = new();

    private GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        
        StartCoroutine(DelayAssignMethod());
    }

    private void OnTriggerEnter2D(Collider2D collision) //Add tile to list if entered
    {
        if (collision.TryGetComponent<Tile>(out var tile))
        {
            currentOverlappedTiles.Add(tile);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Remove tile from list if exited AND in the list
    {
        if (!collision.TryGetComponent<Tile>(out var tile)) { return; }

        if (currentOverlappedTiles.Find(x => tile))
        {
            currentOverlappedTiles.Remove(tile);
        }
    }

    private IEnumerator DelayAssignMethod() //Wait for the trigger methods to finish before returning the lists
    {
        yield return new WaitForEndOfFrame();

        ReturnOverlapTilesToGridManager();
    }

    private void AssignPreviousOverlapTiles()
    {
        previousOverlapTiles.Clear();

        foreach (var tile in currentOverlappedTiles)
        {
            previousOverlapTiles.Add(tile);
        }
    }

    public void ReturnOverlapTilesToGridManager()
    {
        gridManager.UpdateAllOverlappedTiles(previousOverlapTiles, currentOverlappedTiles);

        AssignPreviousOverlapTiles();
    }
}
