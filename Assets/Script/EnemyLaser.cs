using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public GameObject DefeatUI = null;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 10)
        {
            if (GameMode.Instance.Lives > 1)
            {
                GameMode.Instance.PlayerDied();
            }
            else
            {
                GameMode.Instance.CountTime = false;
                Instantiate(DefeatUI);
            }
        }
    }
}
