using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject Player = null;
    [SerializeField] float MinX = 0.0f;
    [SerializeField] float MaxX = 0.0f;
    [SerializeField] float MinY = 0.0f;
    [SerializeField] float MaxY = 0.0f;

    private void Update()
    {
        float x = Mathf.Clamp(Player.transform.position.x, MinX, MaxX);
        float y = Mathf.Clamp(Player.transform.position.y, MinY, MaxY);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
