using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemGenerator : MonoBehaviour
{
    public OpAssets OperationAssets;
    public ProblemStats Properties;

    private AnswerBlock FirstDigit;
    private AnswerBlock SecondDigit;
    private ResultBlock Result;
    private SpriteRenderer Operation;
    private GameObject Platforms;
    private InteractableObject Interactable;
    private bool PlatformActivated = false;

    private void Awake()
    {
        FirstDigit = transform.Find("FirstDigit").GetComponent<AnswerBlock>();
        SecondDigit = transform.Find("SecondDigit").GetComponent<AnswerBlock>();
        Result = transform.Find("Result").GetComponent<ResultBlock>();
        Operation = transform.Find("Operation").GetComponent<SpriteRenderer>();
        Platforms = transform.Find("Platforms").gameObject;
        Interactable = transform.Find("Interactable").GetComponent<InteractableObject>();
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
        int answerRand = 0, resultRand = 0;

        switch (Properties.Problem)
        {
            //First digit must be solved
            case MissingNumber.FirstDigit:Random.Range(Properties.SecondDigit_MinValue, Properties.SecondDigit_MaxValue);

                answerRand = Random.Range(Properties.SecondDigit_MinValue, Properties.SecondDigit_MaxValue);
                resultRand = Random.Range(Properties.Result_MinValue, Properties.Result_MaxValue);
                if(Properties.Operation == Operations.Addition || Properties.Operation == Operations.Subtraction)
                {
                    while (answerRand == resultRand || answerRand == 0 || resultRand == 0)
                    {
                        answerRand = Random.Range(Properties.SecondDigit_MinValue, Properties.SecondDigit_MaxValue);
                        resultRand = Random.Range(Properties.Result_MinValue, Properties.Result_MaxValue);
                    }
                }

                //Using divisibility
                if (Properties.UseDivisibility)
                {
                    if (Properties.Operation == Operations.Multiplication)
                        Properties.SecondDigit_Value = Properties.Divisor;
                    else
                        Properties.SecondDigit_Value = answerRand * Properties.Divisor;

                    Properties.Result_Value = resultRand * Properties.Divisor;
                }
                //Not using divisibility
                else
                {
                    Properties.SecondDigit_Value = answerRand;
                    Properties.Result_Value = resultRand;
                }

                FirstDigit.SetInteractable(true);
                FirstDigit.Properties.Increment = Properties.Increment;
                SecondDigit.SetValue(Properties.SecondDigit_Value);
                Result.SetValue(Properties.Result_Value);
                break;

            //Second digit must be solved
            case MissingNumber.SecondDigit:

                answerRand = Random.Range(Properties.FirstDigit_MinValue, Properties.FirstDigit_MaxValue);
                resultRand = Random.Range(Properties.Result_MinValue, Properties.Result_MaxValue);
                if (Properties.Operation == Operations.Addition || Properties.Operation == Operations.Subtraction)
                {
                    while (answerRand == resultRand || answerRand == 0 || resultRand == 0)
                    {
                        answerRand = Random.Range(Properties.FirstDigit_MinValue, Properties.FirstDigit_MaxValue);
                        resultRand = Random.Range(Properties.Result_MinValue, Properties.Result_MaxValue);
                    }
                }

                //Using divisibility
                if (Properties.UseDivisibility)
                {
                    if (Properties.Operation == Operations.Multiplication)
                        Properties.FirstDigit_Value = Properties.Divisor;
                    else
                        Properties.FirstDigit_Value = answerRand * Properties.Divisor;

                    Properties.Result_Value = resultRand * Properties.Divisor;
                }
                //Not using divisibility
                else
                {
                    Properties.FirstDigit_Value = answerRand;
                    Properties.Result_Value = resultRand;
                }

                SecondDigit.SetInteractable(true);
                SecondDigit.Properties.Increment = Properties.Increment;
                FirstDigit.SetValue(Properties.FirstDigit_Value);
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
        //If correct answer
        if(Solve(FirstDigit.GetValue(), SecondDigit.GetValue()) == Result.GetValue())
        {
            Result.SetResult(true);
            TogglePlatform(true);
        }
        //If wrong answer
        else
        {
            Result.SetResult(false);
            TogglePlatform(false);
        }
    }

    public AnswerBlock GetMissingBlock()
    {
        switch (Properties.Problem)
        {
            case MissingNumber.FirstDigit:
                return FirstDigit;

            case MissingNumber.SecondDigit:
                return SecondDigit;

            default:
                return new AnswerBlock();
        }
    }
}
