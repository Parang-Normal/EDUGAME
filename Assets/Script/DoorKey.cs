using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public Operations operation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if(collision.gameObject.GetComponent<PlayerStats>().Key == Operations.NULL)
            {
                collision.gameObject.GetComponent<PlayerStats>().Key = operation;
                Destroy(gameObject);
            }
        }
    }
}
