using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public FloatingJoystick joystick = null;
    public JumpButton button = null;
    public GameObject DefeatUI = null;
    
    public bool jump { set; get; }
    public bool onGround; //{ private set; get; }
    public float axis { private set; get; }

    public ConditionKey key { private set; get; }
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        jump = false;
        onGround = true;

        GameMode.Instance.SpawnPosition = gameObject.transform.position;
    }
    void ProcessInput()
    {
        axis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && onGround)
        {
            JumpInteract();
        }
    }

    private void Update()
    {
        ProcessInput();
    }

    public void UpdateGroundStatus(bool isInGround)
    {
        onGround = isInGround;
        DoAnimation("Jumping", false);
    }

    public void UpdateButton(bool interact, ConditionKey newKey)
    {
        if (interact)
        {
            button.SetMode(JumpButton.ActionMode.Interact);
            key = newKey;
        }
        else
        {
            button.SetMode(JumpButton.ActionMode.Jump);
        }
    }
    public void JumpInteract()
    {
        //If button is set to jump
        if (button.Mode == JumpButton.ActionMode.Jump)
        {
            if (onGround)
            {
                jump = true;
                onGround = false;
                DoAnimation("Jumping", true);
            }
        }

        //If button is set to interact
        else if (button.Mode == JumpButton.ActionMode.Interact)
        {
            //If there is an interactable object
            if (key != null)
            {
                GetItem();
                key = null;
            }
        }
    }

    public void DoAnimation(string AnimName, bool activate)
    {
        anim.SetBool(AnimName, activate);
    }

    public void Defeat()
    {
        Instantiate(DefeatUI);
    }

    private void GetItem()
    {
        //If inventory is empty
        if(GameMode.Instance.Operation == Operations.NULL)
        {
            GameMode.Instance.UpdateItem(key.Operation);
            Destroy(key.gameObject);
        }

        //If inventory is not empty
        else 
        {
            Operations inventoryItem = GameMode.Instance.GetItem();
            Operations worldItem = key.Operation;

            GameMode.Instance.UpdateItem(worldItem);
            key.Operation = inventoryItem;
            key.UpdateSprite();
        }
    }
}
