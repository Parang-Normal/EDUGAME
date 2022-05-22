using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitBlock : MonoBehaviour
{
    //public DigitAssets DigitBlockAssets;
    public DigitStats Properties;

    protected ProblemGenerator Generator;
    protected SpriteRenderer sprite;
    protected TextMesh text;

    private void Awake()
    {
        Generator = transform.GetComponentInParent<ProblemGenerator>();
        sprite = GetComponent<SpriteRenderer>();
        text = transform.Find("Text").GetComponent<TextMesh>();
    }

    public void SetValue(int value)
    {
        Properties.Value = value;
        text.text = Properties.Value.ToString();
        Generator.Check();
    }

    public int GetValue()
    {
        return Properties.Value;
    }

    public void Add(int value)
    {
        Properties.Value += value; 
        text.text = Properties.Value.ToString();
        Generator.Check();
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If colliding with player and is interactable
        if(collision.gameObject.CompareTag("Player") && Properties.Interactable)
        {
            SetValue(Properties.Value + 1);
            Generator.Check();
        }
    }
    */
}
