using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float JumpForce = 300f;
    [SerializeField] GameObject PauseMenu;

    Rigidbody2D rb;
    Animator anim;
    float axis = 0;
    bool jump = false;
    bool onGround = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        if (axis == 0)
            anim.SetBool("Running", false);
        else
            anim.SetBool("Running", true);

        if (axis < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if(axis > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

    }

    void ProcessInput()
    {
        axis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && onGround)
        {
            jump = true;
            onGround = false;
        }

        if (Input.GetKey(KeyCode.P))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    
    private void FixedUpdate()
    {
        Vector2 pos = rb.position;
        pos.x += axis * Time.deltaTime * Speed;
        rb.position = pos;

        if (jump)
        {
            rb.AddRelativeForce(new Vector2(0, JumpForce));
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }
}
