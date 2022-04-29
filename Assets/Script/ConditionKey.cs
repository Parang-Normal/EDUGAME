using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionKey : MonoBehaviour
{
    public OpAssets OperationAssets;
    public Operations Operation;
    public float amplitude = 0.1f;
    public float frequency = 1f;

    private SpriteRenderer OperationSprite;
    private Vector3 startPos = new Vector3();
    private Vector3 tempPos = new Vector3();


    private void initialize()
    {
        OperationSprite = gameObject.GetComponent<SpriteRenderer>();

        UpdateSprite();
    }

    private void OnValidate()
    {
        initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();
        startPos = transform.position;
    }

    private void Update()
    {
        /*
        tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
        */
    }

    public void UpdateSprite()
    {
        //Sets the operation
        switch (Operation)
        {
            case Operations.Subtraction:
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.Minium;
                break;

            case Operations.Addition:
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.Addicus;
                break;

            case Operations.Multiplication:
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.Multifly;
                break;

            case Operations.Division:
                OperationSprite.GetComponent<SpriteRenderer>().sprite = OperationAssets.TheVoid;
                break;
        }
    }
}
