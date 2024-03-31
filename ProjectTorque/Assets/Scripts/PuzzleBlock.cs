using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleBlock : MonoBehaviour
{
    [Header("Bolts")]
    [SerializeField] private Bolt northBolt;
    [SerializeField] private Bolt southBolt;

    private Tile northValidTile;
    private Tile southValidTile;

    private bool boltIsHeld = false;
    private Camera mainCam;

    public GameObject debug;
    private Bolt rotatingBolt;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (boltIsHeld)
        {
            RotateBlock();
        }
    }

    public void SetBoltIsHeld(bool value, Bolt activeBolt)
    {
        if (activeBolt != null)
        {
            transform.SetParent(GetParentBolt(activeBolt).transform);
        }

        else
        {
            transform.parent = null;
            rotatingBolt = null;

            northBolt.SetParent(transform);
            southBolt.SetParent(transform);
        }

        boltIsHeld = value;
    }

    private Bolt GetParentBolt(Bolt activeBolt)
    {
        if (northBolt == activeBolt)
        {
            southBolt.SetParent(null);
            rotatingBolt = southBolt;
            return southBolt;
        }

        else
        {
            northBolt.SetParent(null);
            rotatingBolt = northBolt;
            return northBolt;
        }
    }
    
    public void RotateBlock()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Transform parent = transform.parent;

        parent.rotation = Quaternion.LookRotation(Vector3.forward, ReturnLookVector(mousePos, parent));

        debug.transform.position = mousePos;
    }

    private Vector2 ReturnLookVector(Vector3 mousePos, Transform parent)
    {
        Vector2 lookVector = mousePos - parent.position;

        if (rotatingBolt == northBolt)
        {
            lookVector = new Vector2(-lookVector.x, -lookVector.y);
        }

        return lookVector;
    }

    public Tile ReturnOtherBoltValidTile(Bolt givenBolt)
    {
        if (givenBolt == northBolt)
            return southBolt.GetValidHoveredTile();

        else if (givenBolt == southBolt)
            return northBolt.GetValidHoveredTile();

        Debug.Log("Invalid Input for ReturnOtherBoltValidTile!");
        return null;
    }
    
    public void SetBoltValidTiles()
    {
        northValidTile = northBolt.GetValidHoveredTile();
        southValidTile = southBolt.GetValidHoveredTile();

        if (northValidTile == null ||  southValidTile == null) { return; }

        ShiftPuzzleBlock();
    }

    private void ShiftPuzzleBlock()
    {
        Vector2 shiftPosition = Vector2.Lerp(northValidTile.transform.position, southValidTile.transform.position, 0.5f);
        transform.position = shiftPosition;

        float rotation = Mathf.Round(transform.rotation.eulerAngles.z / 22.5f) * 22.5f;

        Debug.Log(transform.rotation.eulerAngles.z);
        Debug.Log(rotation);
       
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }
}
