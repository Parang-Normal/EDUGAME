using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BatterySprites
{
    public Sprite Activated;
    public Sprite Deactivated;
}

public class BatteryBox : InteractableObject
{
    public BatterySprites BatteryBoxSprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Type = ObjectType.BatteryBox;
    }

    public override void Interact()
    {
        Activated = false;
        spriteRenderer.sprite = BatteryBoxSprites.Deactivated;
        GameMode.Instance.UpdateLife();
    }
}
