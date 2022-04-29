using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class JumpButtonAssets
{
    public Sprite JumpInitial;
    public Sprite JumpPressed;
    public Sprite InteractInitial;
    public Sprite InteractPressed;
}

public class JumpButton : MonoBehaviour
{
    public enum ActionMode
    {
        Jump,
        Interact
    }

    public JumpButtonAssets SpriteAssets;

    public ActionMode Mode { get; private set; }

    private void Start()
    {
        Mode = ActionMode.Jump;
    }

    public void SetMode(ActionMode mode)
    {
        Mode = mode;
        SpriteState State = new SpriteState();

        switch (Mode)
        {
            case ActionMode.Jump:
                gameObject.GetComponent<Image>().sprite = SpriteAssets.JumpInitial;
                State.pressedSprite = SpriteAssets.JumpPressed;
                break;

            case ActionMode.Interact:
                gameObject.GetComponent<Image>().sprite = SpriteAssets.InteractInitial;
                State.pressedSprite = SpriteAssets.InteractPressed;
                break;
        }

        gameObject.GetComponent<Button>().spriteState = State;
    }
}
