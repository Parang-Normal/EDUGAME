using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operations
{
    Subtraction,
    Addition,
    Multiplication,
    Division
}

public enum Activator
{
    Blocks,
    MovingPlatform
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
public class AddAssets
{
   public Sprite InvisibleSprite;
   public Sprite SolidSprite;
}

public class ProblemSet : MonoBehaviour
{
    [Tooltip("The object which this generator would activte")]
    [SerializeField] Activator Activate;
    [Tooltip("Dictates which operation will be used")]
    [SerializeField] Operations operation = Operations.Subtraction;
    [SerializeField] int Result = 0;
    [SerializeField] NumberBlock FirstDigit = null;
    [SerializeField] NumberBlock SecondDigit = null;
    
    [SerializeField] GameObject OperationSprite = null;
    [SerializeField] GameObject ResultSprite = null;

    public OpAssets OperationAssets;
    public AddAssets AdditionAssets;


    //Question variables
    TextMesh operation_text;
    TextMesh result_text;

    //Bridge variables
    Transform BridgeSpawnPoint_Location;
    float x = 0;

    private void OnValidate()
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
                break;

            case Operations.Addition:
                //operation_text.text = "+";
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.Addicus;
                break;

            case Operations.Multiplication:
                //operation_text.text = "x";
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.Multifly;
                break;

            case Operations.Division:
                //operation_text.text = "÷";
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.TheVoid;
                break;
        }

        result_text.text = Result.ToString();
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
        if (Activate == Activator.MovingPlatform)
        {
            int answer = FirstDigit.Value - SecondDigit.Value;

            if (answer == Result)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().Activated = true;
                }
            }
            else
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().Activated = false;
                }
            }
        }

        else if (Activate == Activator.Blocks)
        {
            if (FirstDigit.CanHit)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    if (i < FirstDigit.Value)
                        BridgeSpawnPoint_Location.GetChild(i).gameObject.SetActive(false);
                    else
                        BridgeSpawnPoint_Location.GetChild(i).gameObject.SetActive(true);
                }
            }
            else if (SecondDigit.CanHit)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    if (i < SecondDigit.Value)
                        BridgeSpawnPoint_Location.GetChild(i).gameObject.SetActive(false);
                    else
                        BridgeSpawnPoint_Location.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    void Add()
    {
        if (Activate == Activator.MovingPlatform)
        {
            int answer = FirstDigit.Value + SecondDigit.Value;

            if (answer == Result)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().Activated = true;
                }
            }
            else
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().Activated = false;
                }
            }

        }
        else if (Activate == Activator.Blocks)
        {
            if (FirstDigit.CanHit)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    if (i < FirstDigit.Value)
                    {
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<SpriteRenderer>().sprite = AdditionAssets.SolidSprite;
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                    }
                    else
                    {
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<SpriteRenderer>().sprite = AdditionAssets.InvisibleSprite;
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                    }

                }
            }
            else if (SecondDigit.CanHit)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    if (i < SecondDigit.Value)
                    {
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<SpriteRenderer>().sprite = AdditionAssets.SolidSprite;
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                    }
                    else
                    {
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<SpriteRenderer>().sprite = AdditionAssets.InvisibleSprite;
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
        }
    }

    void Multiply()
    {
        if (Activate == Activator.MovingPlatform)
        {
            int answer = FirstDigit.Value * SecondDigit.Value;

            if (answer == Result)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().Activated = true;
                }
            }
            else
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().Activated = false;
                }
            }
        }
        else if (Activate == Activator.Blocks)
        {
            if (FirstDigit.CanHit)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    if (i < FirstDigit.Value)
                    {
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<SpriteRenderer>().sprite = AdditionAssets.SolidSprite;
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                    }
                    else
                    {
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<SpriteRenderer>().sprite = AdditionAssets.InvisibleSprite;
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                    }

                }
            }
            else if (SecondDigit.CanHit)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    if (i < SecondDigit.Value)
                    {
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<SpriteRenderer>().sprite = AdditionAssets.SolidSprite;
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                    }
                    else
                    {
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<SpriteRenderer>().sprite = AdditionAssets.InvisibleSprite;
                        BridgeSpawnPoint_Location.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
        }
    }

    void Divide()
    {
        int answer = FirstDigit.Value / SecondDigit.Value;

        if (Activate == Activator.MovingPlatform)
        {
            if(answer == Result)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().Activated = true;
                }
            }
            else
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    BridgeSpawnPoint_Location.GetChild(i).GetComponent<MovingPlatform>().Activated = false;
                }
            }
        }

        else if(Activate == Activator.Blocks)
        {
            if (FirstDigit.CanHit)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    if (i < FirstDigit.Value)
                        BridgeSpawnPoint_Location.GetChild(i).gameObject.SetActive(false);
                    else
                        BridgeSpawnPoint_Location.GetChild(i).gameObject.SetActive(true);
                }
            }
            else if (SecondDigit.CanHit)
            {
                for (int i = 0; i < BridgeSpawnPoint_Location.childCount; i++)
                {
                    if (i < SecondDigit.Value)
                        BridgeSpawnPoint_Location.GetChild(i).gameObject.SetActive(false);
                    else
                        BridgeSpawnPoint_Location.GetChild(i).gameObject.SetActive(true);
                }
            }
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
