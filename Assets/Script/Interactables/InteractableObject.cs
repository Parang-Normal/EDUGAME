using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType
    {
        Key,
        BatteryBox,
        GeneratorBox
    }

    public bool Activated = true;
    public ObjectType Type;

    public virtual void Interact() {}

    public virtual void Interact(int value) { }

    public ObjectType GetObjectType()
    {
        return Type;
    }
}
