using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacterMovement : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float JumpForce = 300f;
    [SerializeField] FloatingJoystick joystick = null;
    [SerializeField] JumpButton JumpButton = null;
    [SerializeField] Transform KeySocket = null;

    Rigidbody2D rb;
    Animator anim;
    float axis = 1;
    bool jump = false;
    bool onGround = true;
    ConditionKey InteractableObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        GameMode.Instance.SpawnPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //For PC
        ProcessInput();
    }

    public void StopMovement()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetBool("Running", false);
    }

    public void JumpInteract()
    {
        //If button is set to jump
        if(JumpButton.Mode == JumpButton.ActionMode.Jump)
        {
            if (onGround)
            {
                jump = true;
                anim.SetBool("Jumping", true);
                onGround = false;
            }
        }

        //If button is set to interact
        else if(JumpButton.Mode == JumpButton.ActionMode.Interact)
        {
            //If there is an interactable object
            if (InteractableObject != null)
            {
                GetKey(InteractableObject.gameObject, KeySocket);
                InteractableObject = null;
            }
        }
    }

    void ProcessInput()
    {
        axis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && onGround)
        {
            JumpInteract();
        }
    }
    
    private void FixedUpdate()
    {
        Vector2 direction = joystick.Direction;

        if (jump)
        {
            rb.AddRelativeForce(new Vector2(0, JumpForce));
            jump = false;
        }

        
        if (direction.x != 0 || axis != 0)
        {
            if (direction.x < 0 || axis < 0)
            {
                axis = -1;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction.x > 0 || axis > 0)
            {
                axis = 1;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            // Vector2 pos = rb.position;
            // pos.x += axis * Time.deltaTime * Speed;
            // rb.position = pos;
            //rb.velocity = new Vector2(axis * Time.deltaTime * Speed, rb.velocity.y);
            float v = axis * Speed; // * Time.deltaTime * Speed;
            rb.velocity = (new Vector2(v, rb.velocity.y));

            anim.SetBool("Running", true);
        }
        else
        {
            StopMovement();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If land or generator tiles
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            onGround = true;
            anim.SetBool("Jumping", false);
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
            anim.SetBool("Jumping", false);
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
            onGround = false;
            anim.SetBool("Jumping", false);
        }
        else
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If key
        if (collision.gameObject.layer == 11)
        {
            JumpButton.SetMode(JumpButton.ActionMode.Interact);
            InteractableObject = collision.gameObject.GetComponent<ConditionKey>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If key
        if (collision.gameObject.layer == 11)
        {
            JumpButton.SetMode(JumpButton.ActionMode.Jump);
        }
    }

    public void GetKey(GameObject TargetObject, Transform Origin)
    {
        //If there is no key yet
        if (KeySocket.childCount == 0)
        {
            StartCoroutine(MakeKeyFloat(TargetObject, Origin.position));
        }

        //If there is a key already
        else
        {
            StartCoroutine(SwitchKeys(TargetObject));
            InteractableObject = null;
        }
        
    }

    public ConditionKey TransferKey()
    {
        if (KeySocket.childCount > 0)
            return KeySocket.GetChild(0).GetComponent<ConditionKey>();
        else
            return null;
    }

    public void DestroyKey()
    {
        if (KeySocket.childCount > 0)
            Destroy(KeySocket.GetChild(0).gameObject);
    }

    IEnumerator MakeKeyFloat(GameObject TargetObject, Vector3 Position)
    {
        float elapsedTime = 0.0f;
        float waitTime = 1.0f;

        Vector3 TargetPos = Position;
        Vector3 OriginalPos = TargetObject.transform.position;

        while (elapsedTime < waitTime)
        {
            TargetObject.transform.position = Vector3.Lerp(OriginalPos, TargetPos, elapsedTime / waitTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }


        TargetObject.transform.parent = KeySocket;

        yield return null;
    }

    IEnumerator SwitchKeys(GameObject TargetObject)
    {
        float elapsedTime = 0.0f;
        float waitTime = 1.0f;

        ConditionKey ExistingKey = KeySocket.GetChild(0).GetComponent<ConditionKey>();
        Vector3 ExistingKeyPos = ExistingKey.transform.position;
        Vector3 OriginalPos = TargetObject.transform.position;

        while (elapsedTime < waitTime)
        {
            TargetObject.transform.position = Vector3.Lerp(OriginalPos, ExistingKeyPos, elapsedTime / waitTime);
            ExistingKey.transform.position = Vector3.Lerp(ExistingKeyPos, OriginalPos, elapsedTime / waitTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        ExistingKey.transform.parent = null;
        TargetObject.transform.parent = KeySocket;

        yield return null;
    }
}
