using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject VictoryUI = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            GameMode.Instance.CountTime = false;
            GameMode.Instance.Elements.HUD.SetActive(false);
            Instantiate(VictoryUI);
        }
    }
}
