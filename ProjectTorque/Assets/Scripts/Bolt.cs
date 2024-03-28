using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private PuzzleBlock puzzleBlock;

    void Start()
    {
        puzzleBlock = transform.parent.GetComponent<PuzzleBlock>();
    }

    public void OnMouseDown()
    {
        puzzleBlock.SetBoltIsHeld(true, this);

        Debug.Log(gameObject.name + " is held!");
    }

    public void OnMouseUp()
    {
        puzzleBlock.SetBoltIsHeld(false, null);

        Debug.Log(gameObject.name + " is not held!");
    }

    public void SetParent(Transform givenTransform)
    {
        transform.parent = givenTransform;
    }
}
