using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float FallDelay = 1.5f;
    [SerializeField] int CorrectAnswer = 0;
    [SerializeField] Operations Operation = Operations.Addition;
    [SerializeField] int FirstDigit = 0;
    [SerializeField] int SecondDigit = 0;

    bool willFall = true;

    void iniialize()
    {
        TextMesh text = gameObject.transform.GetChild(0).GetComponent<TextMesh>();
        string operation_text = "";

        //Sets the operation
        switch (Operation)
        {
            case Operations.Subtraction:
                operation_text = "-";
                break;

            case Operations.Addition:
                operation_text = "+";
                break;

            case Operations.Multiplication:
                operation_text = "x";
                break;

            case Operations.Division:
                operation_text = "÷";
                break;
        }

        text.text = FirstDigit + " " + operation_text + " " + SecondDigit;
    }

    private void OnValidate()
    {
        iniialize();
    }

    private void Start()
    {
        iniialize();
        int result = 0;

        switch (Operation)
        {
            case Operations.Subtraction:
                result = FirstDigit - SecondDigit;
                break;

            case Operations.Addition:
                result = FirstDigit + SecondDigit;
                break;

            case Operations.Multiplication:
                result = FirstDigit * SecondDigit;
                break;

            case Operations.Division:
                result = FirstDigit / SecondDigit;
                break;
        }

        if (result == CorrectAnswer)
            willFall = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 && willFall)
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(FallDelay);
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }
}

