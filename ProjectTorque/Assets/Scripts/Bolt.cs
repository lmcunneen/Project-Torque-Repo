using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private PuzzleBlock puzzleBlock;

    private SpriteRenderer blockSprite;
    private SpriteRenderer boltSprite;

    private GridManager gridManager;

    private Tile validHoveredTile;

    void Start()
    {
        puzzleBlock = transform.parent.GetComponent<PuzzleBlock>();
        blockSprite = puzzleBlock.GetComponent<SpriteRenderer>();

        boltSprite = GetComponent<SpriteRenderer>();

        gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        boltSprite.sortingOrder = blockSprite.sortingOrder + 1;
    }

    public void OnMouseDown()
    {
        if (puzzleBlock.ReturnOtherBoltValidTile(this) == null)
        {
            Debug.Log("Can't move this bolt!");
            return;
        }
        
        puzzleBlock.SetBoltIsHeld(true, this);
        gridManager.MovePuzzleBlockToFront(puzzleBlock);

        Debug.Log(gameObject.name + " is held!");
    }

    public void OnMouseUp()
    {
        puzzleBlock.SetBoltIsHeld(false, null);

        if (validHoveredTile != null)
        {
            puzzleBlock.SetBoltValidTiles();
        }

        Debug.Log(gameObject.name + " is not held!");
    }

    public void SetParent(Transform givenTransform)
    {
        transform.parent = givenTransform;
    }

    public Tile GetValidHoveredTile()
    {
        return validHoveredTile;
    }   
    
    public void SetValidHoveredTile(Tile givenTile)
    {
        validHoveredTile = givenTile;
    }
}
