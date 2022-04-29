using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] Operations RequiredOperation = Operations.Addition;
    [SerializeField] GameObject Text = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Operations Key = collision.gameObject.GetComponent<PlayerStats>().Key;

            Text.SetActive(true);
            if (Key == RequiredOperation)
            {
                Text.GetComponent<TextMesh>().text = "You have the key!";
            }
            else
            {
                Text.GetComponent<TextMesh>().text = "Need to obtain key!";
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Text.SetActive(false);
        }
    }
}
