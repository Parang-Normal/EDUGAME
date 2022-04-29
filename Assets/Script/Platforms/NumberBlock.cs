using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlock : MonoBehaviour
{
    public enum GeneratorType
    {
        ProblemSet,
        OperationsGenerator
    }

    public bool CanHit = false;
    public int Value = 0;
    public int LowestValue = 0;
    public int HighestValue = 1;
    public int Increment = 1;
    public GeneratorType Type = GeneratorType.ProblemSet;
    

    public Sprite InteractableSprite = null;
    public Sprite NonInteractableSprite = null;

    public ProblemSet problemSet;
    public OperationGenerator operationGenerator;

    TextMesh text;

    void initialize()
    {
        text = gameObject.transform.GetChild(0).GetComponent<TextMesh>();

        text.text = Value.ToString();

        if (CanHit)
            gameObject.GetComponent<SpriteRenderer>().sprite = InteractableSprite;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = NonInteractableSprite;

    }

    private void OnValidate()
    {
        initialize();
    }

    private void Start()
    {
        initialize(); 
        
        if (Type == GeneratorType.ProblemSet)
            problemSet = gameObject.transform.parent.GetComponentInParent<ProblemSet>();
        else if (Type == GeneratorType.OperationsGenerator)
            operationGenerator = gameObject.transform.parent.GetComponentInParent<OperationGenerator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 && CanHit && Type == GeneratorType.ProblemSet)
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
