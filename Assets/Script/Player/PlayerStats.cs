using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Operations Key { get; set; }

    private void Awake()
    {
        Key = Operations.NULL;
    }
}
