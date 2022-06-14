using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player = null;
    public float MinX = 0.0f;
    public float MaxX = 0.0f;
    public float MinY = 0.0f;
    public float MaxY = 0.0f;

    private void Start()
    {
        if(GameMode.Instance != null)
            GameMode.Instance.CameraMinY = MinY;
    }

    private void FixedUpdate()
    {
        float x = Mathf.Clamp(Player.transform.position.x, MinX, MaxX);
        float y = Mathf.Clamp(Player.transform.position.y, MinY, MaxY);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
