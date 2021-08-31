using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovingPlatform : MonoBehaviour
{
    public bool Activated = false;
    public Vector3 MaxDistance = new Vector3(0, 0, 0);
    public Vector3 Velocity = new Vector3(0, 0, 0);

    private Vector3 originalPosition;
    private Vector3 distance;
    private int direction = 1;
    private bool hasPlayer = false;

    private void OnValidate()
    {
        originalPosition = gameObject.transform.position;
        distance = MaxDistance + originalPosition;
    }

    private void Update()
    {
        if (Activated && hasPlayer)
        {
            transform.position += (Velocity * Time.deltaTime * direction);
            if (transform.position.x >= distance.x)
                direction = -1;

            else if (transform.position.x <= originalPosition.x)
                direction = 1;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            hasPlayer = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            hasPlayer = false;
            collision.collider.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(originalPosition, distance, Color.red, Mathf.Infinity);
    }
}
