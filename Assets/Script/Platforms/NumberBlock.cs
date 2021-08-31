using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlock : MonoBehaviour
{
    public bool CanHit = false;
    public int Value = 0;
    public int LowestValue = 0;
    public int HighestValue = 1;
    public int Increment = 1;

    [SerializeField] Sprite InteractableSprite = null;
    [SerializeField] Sprite NonInteractableSprite = null;

    ProblemSet problemSet;

    TextMesh text;

    private void OnValidate()
    {
        problemSet = gameObject.transform.parent.GetComponentInParent<ProblemSet>();
        text = gameObject.transform.GetChild(0).GetComponent<TextMesh>();

        text.text = Value.ToString();

        if (CanHit)
            gameObject.GetComponent<SpriteRenderer>().sprite = InteractableSprite;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = NonInteractableSprite;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 && CanHit)
        {
            Value += Increment;

            if (Value > HighestValue)
                Value = LowestValue;

            else if (Value < LowestValue)
                Value = HighestValue;

            text.text = Value.ToString();

            problemSet.CheckResult();
        }
    }
}
