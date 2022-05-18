using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public int Health = 1;
    public bool Killable = false;
    public bool OnGround = false;
    public float WalkRange = 5f;
}
