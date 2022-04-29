using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTiles : MonoBehaviour
{
    public Sprite InvisibleSprite;
    public Sprite SolidSprite;
    public bool IsInvisible = true;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = !IsInvisible;
    }

    public void ToggleSprite(bool makeSolid)
    {

        //Make tile solid
        if (makeSolid)
            spriteRenderer.sprite = SolidSprite;


        //Make tile invisible
        else
            spriteRenderer.sprite = InvisibleSprite;

        //Enable or disable box collider
        boxCollider.enabled = makeSolid;
    }
    
}
