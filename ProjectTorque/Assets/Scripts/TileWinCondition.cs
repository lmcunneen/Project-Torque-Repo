using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class TileWinCondition : MonoBehaviour
{
    [SerializeField] private PuzzleBlock winBlock;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = winBlock.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
