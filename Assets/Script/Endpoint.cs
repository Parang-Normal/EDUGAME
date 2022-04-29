using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conditions
{
    public bool Addition;
    public bool Subtraction;
    public bool Multiplication;
    public bool Division;
}

public class Endpoint : MonoBehaviour
{
    [SerializeField] GameObject VictoryUI = null;
    public Conditions WinConditions;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            GameMode.Instance.CountTime = false;
            Instantiate(VictoryUI);
        }
    }
}
