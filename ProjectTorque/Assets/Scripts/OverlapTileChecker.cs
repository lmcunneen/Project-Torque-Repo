using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapTileChecker : MonoBehaviour
{
    private List<Tile> currentOverlappedTiles = new();

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
}
