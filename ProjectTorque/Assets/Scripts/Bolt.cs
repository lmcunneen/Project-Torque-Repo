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

    private List<Tile> currentOverlapTiles = new();

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
        SetCurrentOverlapTiles();
        
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
        if (validHoveredTile != null)
        {
            if (gridManager.CheckIfTileIsOverlapped(validHoveredTile, currentOverlapTiles))
            {
                validHoveredTile = null;
            }
            
            puzzleBlock.SetBoltValidTiles();
        }

        puzzleBlock.RunOverlapListMethod();
        puzzleBlock.SetBoltIsHeld(false, null);

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

    private void SetCurrentOverlapTiles()
    {
        currentOverlapTiles.Clear();
        
        foreach(var tile in puzzleBlock.overlapChecker.ReturnCurrentTiles())
        {
            currentOverlapTiles.Add(tile);
        }
    }
}
