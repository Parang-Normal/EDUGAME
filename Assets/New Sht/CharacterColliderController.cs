using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColliderController : MonoBehaviour
{

    CharacterController Controller;

    // Start is called before the first frame update
    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject, true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessTrigger(collision.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ProcessTrigger(collision.gameObject, false);
    }

    private void ProcessCollision(GameObject collider, bool enter)
    {
        //If it is a generator or land
        if(collider.CompareTag("Generator") || collider.CompareTag("Land"))
        {
            Controller.UpdateGroundStatus(enter);
        }
        
        else
        {
            Controller.UpdateGroundStatus(!enter);
        }
    }

    private void ProcessTrigger(GameObject collider, bool enter)
    {
        //If it is a key
        if (collider.CompareTag("Key"))
        {
            Controller.UpdateButton(enter, collider.GetComponent<ConditionKey>());
        }
        //if it entered a kill zone
        else if (collider.CompareTag("KillZ") && enter)
        {
            if (GameMode.Instance.Lives > 1)
            {
                GameMode.Instance.PlayerDied();
            }
            else
            {
                GameMode.Instance.CountTime = false;
                Controller.Defeat();
            }
        }
    }
}
