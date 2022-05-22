using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right
}

public class Enemy : MonoBehaviour
{
    public EnemyStats Stats;
    public float speed = 1f;

    Rigidbody2D rb;
    Animator anim;
    Vector3 LeftRange;
    Vector3 RightRange;
    Vector3 Velocity = new Vector3();
    public Direction dir = Direction.Right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        LeftRange = transform.position; LeftRange.x -= Stats.WalkRange;
        RightRange = transform.position; RightRange.x += Stats.WalkRange;
    }

    private void Update()
    {
        if(Stats.OnGround)
        {
            Move();
        }
    }

    private void Move()
    {
        if (dir == Direction.Right)
            Velocity.x = speed;//transform.position = Vector3.Lerp(transform.position, RightRange, Time.deltaTime);
        else if (dir == Direction.Left)
            Velocity.x = -speed;//transform.position = Vector3.Lerp(transform.position, LeftRange, Time.deltaTime);
    
        CheckDirection();

        transform.position += Velocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If colliding with land
        if (collision.gameObject.CompareTag("Land"))
        {
            Stats.OnGround = true;
            anim.SetBool("Walking", true);

        }
        //If colliding with player
        else if (collision.gameObject.CompareTag("Player"))
        {
            //Add logic here
        }
    }

    private void CheckDirection()
    {
        if (transform.position.x > RightRange.x) 
            dir = Direction.Left;
        else if (transform.position.x < LeftRange.x)
            dir = Direction.Right;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //If colliding with land
        if (collision.gameObject.CompareTag("Land"))
        {
            Stats.OnGround = false;
            anim.SetBool("Walking", false);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(LeftRange, RightRange, Color.red, 1);
    }
}
