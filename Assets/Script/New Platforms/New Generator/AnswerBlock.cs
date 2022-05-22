using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerBlock : DigitBlock
{
    public DigitAssets DigitBlockAssets;

    public void SetInteractable(bool isInteractable)
    {
        Properties.Interactable = isInteractable;

        if (isInteractable)
            sprite.sprite = DigitBlockAssets.Interactable;
        else
            sprite.sprite = DigitBlockAssets.NonInteractable;
    }
}
