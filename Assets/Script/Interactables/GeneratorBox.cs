using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBox : InteractableObject
{
    private AnswerBlock Block;

    private void Start()
    {
        Type = ObjectType.GeneratorBox;
        Block = transform.parent.GetComponent<ProblemGenerator>().GetMissingBlock();
    }

    public override void Interact(int value)
    {
        Block.Add(value);
    }
}
