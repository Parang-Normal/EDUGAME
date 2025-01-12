﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX(SFXClip.Fragment);
            GameMode.Instance.UpdateGems(1);
            Destroy(gameObject);
        }
    }
}
