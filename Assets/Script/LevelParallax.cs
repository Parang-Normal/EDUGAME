using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParallax : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enable parallax effect of children if player enters
        if (collision.CompareTag("Player"))
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Parallax>() != null)
                    transform.GetChild(i).GetComponent<Parallax>().ActivateParallax(collision.transform.position.x);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Disable parallax effect of children if player exits
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Parallax>() != null)
                transform.GetChild(i).GetComponent<Parallax>().enabled = false;
        }
    }
}
