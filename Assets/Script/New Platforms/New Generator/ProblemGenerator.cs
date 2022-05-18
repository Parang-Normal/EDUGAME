using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemGenerator : MonoBehaviour
{
    public OpAssets OperationAssets;
    public ProblemStats Properties;

    private DigitBlock FirstDigit;
    private DigitBlock SecondDigit;
    private DigitBlock Result;
    private SpriteRenderer Operation;
    private GameObject Platforms;
    private bool PlatformActivated = false;

    private void Awake()
    {
        FirstDigit = transform.Find("FirstDigit").GetComponent<DigitBlock>();
        SecondDigit = transform.Find("SecondDigit").GetComponent<DigitBlock>();
        Result = transform.Find("Result").GetComponent<DigitBlock>();
        Operation = transform.Find("Operation").GetComponent<SpriteRenderer>();
        Platforms = transform.Find("Platforms").gameObject;
    }

    private void Start()
    {
        Randomize();
        SetOperation();
    }

    //Randomizes the values of the first digit, second digit, and result
    //Depends on what digit must be solved by the player
    private void Randomize()
    {
        switch (Properties.Problem)
        {
            //First digit must be solved
            case MissingNumber.FirstDigit:
                Properties.SecondDigit_Value = Random.Range(Properties.SecondDigit_MinValue, Properties.SecondDigit_MaxValue);
                Properties.Result_Value = Random.Range(Properties.Result_MinValue, Properties.Result_MaxValue);

                FirstDigit.SetInteractable(true);
                SecondDigit.SetValue(Properties.SecondDigit_Value);
                Result.SetValue(Properties.Result_Value);
                break;

            //Second digit must be solved
            case MissingNumber.SecondDigit:
                Properties.FirstDigit_Value = Random.Range(Properties.FirstDigit_MinValue, Properties.FirstDigit_MaxValue);
                Properties.Result_Value = Random.Range(Properties.Result_MinValue, Properties.Result_MaxValue);

                SecondDigit.SetInteractable(true);
                FirstDigit.SetValue(Properties.FirstDigit_Value);
                Result.SetValue(Properties.Result_Value);
                break;

            //Result must be solved
            case MissingNumber.Result:
                Properties.FirstDigit_Value = Random.Range(Properties.FirstDigit_MinValue, Properties.FirstDigit_MaxValue);
                Properties.SecondDigit_Value = Random.Range(Properties.SecondDigit_MinValue, Properties.SecondDigit_MaxValue);
                Properties.Result_Value = Random.Range(Properties.Result_MinValue, Properties.Result_MaxValue);

                FirstDigit.SetInteractable(true);
                SecondDigit.SetInteractable(true);
                FirstDigit.SetValue(Properties.FirstDigit_Value);
                SecondDigit.SetValue(Properties.SecondDigit_Value);
                Result.SetValue(Properties.Result_Value);
                break;
        }
    }

    //Sets the correct sprite depending on the operation used
    private void SetOperation()
    {
        switch (Properties.Operation)
        {
            case Operations.Addition:
                Operation.sprite = OperationAssets.Addicus;
                break;

            case Operations.Subtraction:
                Operation.sprite = OperationAssets.Minium;
                break;

            case Operations.Multiplication:
                Operation.sprite = OperationAssets.Multifly;
                break;

            case Operations.Division:
                Operation.sprite = OperationAssets.TheVoid;
                break;
        }
    }

    private int Solve(int firstDigit, int secondDigit)
    {
        switch (Properties.Operation)
        {
            case Operations.Addition:
                return firstDigit + secondDigit;

            case Operations.Subtraction:
                return firstDigit - secondDigit;

            case Operations.Multiplication:
                return firstDigit * secondDigit;

            case Operations.Division:
                return firstDigit / secondDigit;

            default:
                return 0;
        }
    }

    private void TogglePlatform(bool activate)
    {
        //Check to avoid disabling disabled platforms or activating activated platforms
        if (PlatformActivated != activate)
        {
            PlatformActivated = activate;

            for(int i = 0; i < Platforms.transform.childCount; i++)
            {
                Platforms.transform.GetChild(i).GetComponent<Platform>().ToggleActivate(activate, Properties.Operation);
            }
        }
    }

    public void Check()
    {
        //Debug.Log("Answer: " + Solve(FirstDigit.GetValue(), SecondDigit.GetValue()).ToString() + " Result: " + Result.GetValue().ToString());
        if(Solve(FirstDigit.GetValue(), SecondDigit.GetValue()) == Result.GetValue())
        {
            Debug.Log("Correct!");
            TogglePlatform(true);
        }
        else
        {
            TogglePlatform(false);
        }
    }
}
