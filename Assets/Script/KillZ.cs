using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZ : MonoBehaviour
{
    [SerializeField] GameObject DefeatUI = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.layer == 10)
        {
            if (GameMode.Instance.Lives > 1)
            {
                GameMode.Instance.PlayerDied();
            }
            else
            {
                GameMode.Instance.CountTime = false;
                SoundManager.Instance.PlaySFX(SFXClip.Lose);
                Instantiate(DefeatUI);
            }
        }
    }
}
