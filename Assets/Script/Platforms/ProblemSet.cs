using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operations
{
    Subtraction,
    Addition,
    Multiplication,
    Division,
    NULL
}

public enum Activator
{
    Blocks,
    MovingPlatform,
}

[System.Serializable]
public class OpAssets
{
    public Sprite Addicus;
    public Sprite Minium;
    public Sprite Multifly;
    public Sprite TheVoid;
}

[System.Serializable]
public class DigitAssets
{
    public Sprite Interactable;
    public Sprite NonInteractable;
}

[System.Serializable]
public class AddAssets
{
   public Sprite InvisibleSprite;
   public Sprite SolidSprite;
}

public class ProblemSet : MonoBehaviour
{
    [Tooltip("The object which this generator would activte")]
    public Activator Activate = Activator.Blocks;
    [Tooltip("Dictates which operation will be used")]
    public Operations operation = Operations.Subtraction;
    public int Result = 0;
    public NumberBlock FirstDigit = null;
    public NumberBlock SecondDigit = null;
    
    public GameObject OperationSprite = null;
    public GameObject ResultSprite = null;

    public OpAssets OperationAssets;
    public AddAssets AdditionAssets;


    //Question variables
    TextMesh operation_text;
    TextMesh result_text;

    //Bridge variables
    Transform BridgeSpawnPoint_Location;
    public bool bridgeActive = false;

    void intialize()
    {
        //Get question text meshes
        operation_text = OperationSprite.transform.GetChild(0).GetComponent<TextMesh>();
        result_text = ResultSprite.transform.GetChild(0).GetComponent<TextMesh>();

        //Get bridge spawn point
        BridgeSpawnPoint_Location = gameObject.transform.GetChild(2).transform;

        //Sets the operation
        switch (operation)
        {
            case Operations.Subtraction:
                //operation_text.text = "-";
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.Minium;
                bridgeActive = true;
                break;

            case Operations.Addition:
                //operation_text.text = "+";
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.Addicus;
                bridgeActive = false;
                break;

            case Operations.Multiplication:
                //operation_text.text = "x";
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.Multifly;
                bridgeActive = false;
                break;

            case Operations.Division:
                //operation_text.text = "÷";
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.TheVoid;
                bridgeActive = true;
                break;
        }

        result_text.text = Result.ToString();
    }

    private void OnValidate()
    {
        intialize();
    }

    private void Start()
    {
        intialize();
    }

    public void CheckResult()
    {
        switch (operation)
        {
            case Operations.Subtraction:
                Subtract();
                break;

            case Operations.Addition:
                Add();
                break;

            case Operations.Multiplication:
                Multiply();
                break;

            case Operations.Division:
                Divide();
                break;
        }
    }

    void Subtract()
    {
        int answer = FirstDigit.Value - SecondDigit.Value;

        if (Activate == Activator.MovingPlatform)
        {
            ActivateMovingPlatform(answer);
        }

        else if (Activate == Activator.Blocks)
        {
            ActivateBlocks(answer, false);
        }
    }

    void Add()
    {
        int answer = FirstDigit.Value + SecondDigit.Value;

        if (Activate == Activator.MovingPlatform)
        {
            ActivateMovingPlatform(answer);
        }

        else if (Activate == Activator.Blocks)
        {
            ActivateBlocks(answer, true);
        }
    }

    void Multiply()
    {
        int answer = FirstDigit.Value * SecondDigit.Value;

        if (Activate == Activator.MovingPlatform)
        {
            ActivateMovingPlatform(answer);
        }
        else if (Activate == Activator.Blocks)
        {
            ActivateBlocks(answer, true);
        }
    }

    void Divide()
    {
        float correctAnswer = Result;
        float answer = (FirstDigit.Value * 1.0f) / (SecondDigit.Value * 1.0f);


        if (Activate == Activator.MovingPlatform)
        {
            ActivateMovingPlatform(answer);
        }

        else if (Activate == Activator.Blocks)
        {
            ActivateBlocks(answer, false);
        }

    }

    void ActivateMovingPlatform(float answer) 
    {
        if (answer == Result)
        {
            for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
            {
                BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().ActivatePlatform(true);
            }
        }
        else
        {
            for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
            {
                BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().ActivatePlatform(false);
            }
        }
    }

    void ActivateBlocks(float answer, bool activate)
    {
        //If answer is correct and the bridge is not active, then activate bridge
        if (answer == Result && bridgeActive == !activate)
        {
            for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
            {
                if (BridgeSpawnPoint_Location.GetChild(i).GetComponent<ToggleTiles>() != null)
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<ToggleTiles>().ToggleSprite(activate);

                if(BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>() != null)
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().ActivatePlatform(!activate);
            }

            bridgeActive = activate;
        }

        //If bridge is active, disable bridge
        else if (bridgeActive == activate)
        {
            for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
            {
                if (BridgeSpawnPoint_Location.GetChild(i).GetComponent<ToggleTiles>() != null)
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<ToggleTiles>().ToggleSprite(!activate);

                if (BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>() != null)
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().ActivatePlatform(activate);
            }

            bridgeActive = !activate;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

}
