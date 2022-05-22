using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpForce = 320f;

    CharacterController Controller;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Controller.joystick.Direction;
        float axis = Controller.axis;

        bool jump = Controller.jump;
        float initialV = 0f;

        //Jump
        if (jump)
        {
            rb.AddForce(new Vector2(0, JumpForce));
            Controller.jump = false;
        }

        //Go Left
        if (direction.x < 0 || axis < 0)
        {
            initialV = -1;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            Controller.DoAnimation("Running", true);
        }
        //Go Right
        else if (direction.x > 0 || axis > 0)
        {
            initialV = 1;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            Controller.DoAnimation("Running", true);
        }
        //Stop
        else
        {
            initialV = 0;
            Controller.DoAnimation("Running", false);
        }

        Vector2 temp = new Vector2(initialV, rb.velocity.y);
        float v = initialV * Speed;
        rb.velocity = (new Vector2(v, rb.velocity.y));
        //rb.MovePosition(rb.position + (temp * Speed * Time.fixedDeltaTime));
    }

}
