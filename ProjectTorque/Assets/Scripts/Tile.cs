using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color baseColor;

    private bool isActive = false;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var triggeredBolt = collision.GetComponent<Bolt>();

        if (triggeredBolt != null)
        {
            spriteRenderer.color = Color.cyan;

            isActive = true;

            triggeredBolt.SetValidHoveredTile(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActive)
        {
            spriteRenderer.color = baseColor;

            isActive = false;

            collision.TryGetComponent<Bolt>(out var exitedBolt);

            if (exitedBolt.GetValidHoveredTile() == this)
            {
                collision.GetComponent<Bolt>().SetValidHoveredTile(null);
            }
        }
    }
}
