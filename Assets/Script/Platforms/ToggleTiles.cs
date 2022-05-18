using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTiles : Platform
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
        // boxCollider.enabled = !IsInvisible;
        ToggleSprite(!IsInvisible);
    }

    public override void ToggleActivate(bool activate, Operations Operation)
    {
        //Make blocks visible when addition/multiplication and make blocks invisible if subtraction/division
        if(Operation == Operations.Addition || Operation == Operations.Multiplication)
        {
            ToggleSprite(activate);
        }
        else if(Operation == Operations.Subtraction || Operation == Operations.Division)
        {
            ToggleSprite(!activate);
        }
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
