using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovingPlatform : MonoBehaviour
{
    public enum Axis
    {
        Horizontal,
        Vertical
    }

    public Axis Direction;
    public bool Activated = false;
    public bool Automatic = false;
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

    private void Start()
    {
        originalPosition = gameObject.transform.position;
        distance = MaxDistance + originalPosition;
    }

    private void Update()
    {
        if (Activated && (hasPlayer || Automatic))
        {
            transform.position += (Velocity * Time.deltaTime * direction);

            if(Direction == Axis.Horizontal)
            {
                if (transform.position.x >= distance.x)
                    direction = -1;

                else if (transform.position.x <= originalPosition.x)
                    direction = 1;
            }
            else if(Direction == Axis.Vertical)
            {
                if (transform.position.y >= distance.y)
                    direction = -1;

                else if (transform.position.y <= originalPosition.y)
                    direction = 1;
            }

        }
    }

    public void ActivatePlatform(bool active)
    {
        Activated = active;

        if (active)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            hasPlayer = true;
            collision.gameObject.transform.parent.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            hasPlayer = false;
            collision.gameObject.transform.parent.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(originalPosition, distance, Color.red, 1);
    }
}
