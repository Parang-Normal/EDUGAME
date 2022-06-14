using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationBlock : MonoBehaviour
{
    public OpAssets SpriteAssets;
    public Sprite EmptySprite;
    public GameObject KeyPrefab;
    public OperationGenerator ParentGenerator = null;
    public Operations Operation = Operations.NULL;
    public Operations CorrectOperation = Operations.NULL;

    private SpriteRenderer Renderer;
    private bool HasKey = false;

    private void Start()
    {
        Renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void ChangeSprite(Operations NewOperation)
    {
        switch (NewOperation)
        {
            case Operations.Subtraction:
                Renderer.sprite = SpriteAssets.Minium;
                break;

            case Operations.Addition:
                Renderer.sprite = SpriteAssets.Addicus;
                break;

            case Operations.Multiplication:
                Renderer.sprite = SpriteAssets.Multifly;
                break;

            case Operations.Division:
                Renderer.sprite = SpriteAssets.TheVoid;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If player hit
        if (collision.gameObject.CompareTag("Player"))
        {
            Operations Key = GameMode.Instance.Operation;

            //If player has key
            if(Key != Operations.NULL)
            {
                //If this block has no key
                if (!HasKey)
                {
                    Operation = Key;
                    ChangeSprite(Key);
                    HasKey = true;

                    GameMode.Instance.GetItem();
                }

                //If this block has key
                else
                {
                    Operations localCopy = Operation;
                    Operations inventoryCopy = GameMode.Instance.GetItem();

                    Operation = inventoryCopy;
                    GameMode.Instance.UpdateItem(localCopy);
                    ChangeSprite(Operation);
                }

                ParentGenerator.CheckOperations();
            }

            //If player has no key
            else
            {
                //If this block has key
                if (HasKey)
                {
                    GameMode.Instance.UpdateItem(Operation);

                    HasKey = false;
                    Renderer.sprite = EmptySprite;
                    Operation = Operations.NULL;

                    ParentGenerator.CheckOperations();
                }
            }
        }
    }

    public bool IsOperationCorrect()
    {
        return Operation == CorrectOperation;
    }
}
/* Old code for switching keys
  //If player hit
        if (collision.gameObject.CompareTag("Player"))
        {
            MainCharacterMovement Player = collision.gameObject.GetComponent<MainCharacterMovement>();
            ConditionKey Key = Player.TransferKey();

            //If player has key
            if(Key != null)
            {
                //If this block has no key
                if (!HasKey)
                {
                    Operation = Key.Operation;
                    ChangeSprite(Key.Operation);
                    HasKey = true;

                    Player.DestroyKey();
                }

                //If this block has key
                else
                {
                    GameObject Clone = Instantiate(KeyPrefab, transform, true);
                    Clone.transform.parent = null;
                    Clone.GetComponent<ConditionKey>().Operation = Operation;
                    Player.GetKey(Clone, transform);

                    Operation = Key.Operation;
                    ChangeSprite(Key.Operation);
                }

                ParentGenerator.CheckOperations();
            }

            //If player has no key
            else
            {
                //If this block has key
                if (HasKey)
                {
                    GameObject Clone = Instantiate(KeyPrefab, transform, true);
                    Clone.transform.parent = null;
                    Clone.GetComponent<ConditionKey>().Operation = Operation;
                    Player.GetKey(Clone, transform);

                    HasKey = false;
                    Renderer.sprite = EmptySprite;
                    Operation = Operations.NULL;
                }
            }
        }
*/