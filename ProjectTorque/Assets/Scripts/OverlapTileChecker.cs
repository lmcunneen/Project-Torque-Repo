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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Tile>(out var tile))
        {
            currentOverlappedTiles.Add(tile);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Tile>(out var tile)) { return; }

        if (currentOverlappedTiles.Find(x => tile))
        {
            currentOverlappedTiles.Remove(tile);
        }
    }

    private IEnumerator DelayAssignMethod()
    {
        yield return new WaitForEndOfFrame();

        ReturnOverlapTilesToGridManager(false);
    }

    private void AssignPreviousOverlapTiles()
    {
        previousOverlapTiles.Clear();

        foreach (var tile in currentOverlappedTiles)
        {
            previousOverlapTiles.Add(tile);
        }
    }

    public void ReturnOverlapTilesToGridManager(bool resetPrevious)
    {
        gridManager.UpdateAllOverlappedTiles(previousOverlapTiles, currentOverlappedTiles);

        if (resetPrevious) 
            AssignPreviousOverlapTiles();
    }
}
