using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    Jump,
    Interact,
    Charge
}

public class CharacterController : MonoBehaviour
{
    

    public FloatingJoystick joystick = null;
    public JumpButton JumpInteractButton = null;
    public GameObject ChargeButton = null;
    public GameObject DefeatUI = null;
    
    public bool jump { set; get; }
    public bool onGround;
    public float axis { private set; get; }

    public ButtonType buttonType = ButtonType.Jump;
    public InteractableObject interactableObj { private set; get; }
    Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        if (PlayerPrefs.GetInt("DuckyHat_Equipped", 0) == 1)
            anim.SetBool("DuckyHat", true);
        else
            anim.SetBool("DuckyHat", false);
    }

    // Start is called before the first frame update
    void Start()
    {

        jump = false;
        onGround = true;

        if(GameMode.Instance != null)
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

    public void UpdateButton(bool interact, InteractableObject newObj)
    {
        if (interact && newObj.Activated)
        {
            if(newObj.GetObjectType() == InteractableObject.ObjectType.Key || newObj.GetObjectType() == InteractableObject.ObjectType.BatteryBox)
            {
                ChargeButton.SetActive(false);
                JumpInteractButton.gameObject.SetActive(true);
                JumpInteractButton.SetMode(JumpButton.ActionMode.Interact);
            }
            else if(newObj.GetObjectType() == InteractableObject.ObjectType.GeneratorBox)
            {
                ChargeButton.SetActive(true);
                JumpInteractButton.gameObject.SetActive(false);
            }

            interactableObj = newObj;
        }
        else
        {
            ChargeButton.SetActive(false);
            JumpInteractButton.gameObject.SetActive(true);
            JumpInteractButton.SetMode(JumpButton.ActionMode.Jump);

            interactableObj = null;
        }
    }
    public void JumpInteract()
    {
        //If button is set to jump
        if (JumpInteractButton.Mode == JumpButton.ActionMode.Jump)
        {
            if (onGround)
            {
                jump = true;
                onGround = false;
                DoAnimation("Jumping", true);
            }
        }

        //If button is set to interact
        else if (JumpInteractButton.Mode == JumpButton.ActionMode.Interact)
        {
            //If there is an interactable object
            if (interactableObj != null)
            {
                interactableObj.Interact();
                interactableObj = null;
            }
        }
    }

    public void Charge(bool up)
    {
        if (up)
            interactableObj.Interact(1);
        else
            interactableObj.Interact(-1);
    }

    public void DoAnimation(string AnimName, bool activate)
    {
        anim.SetBool(AnimName, activate);
    }

    public void Defeat()
    {
        Instantiate(DefeatUI);
    }

    /*
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
    */
}
