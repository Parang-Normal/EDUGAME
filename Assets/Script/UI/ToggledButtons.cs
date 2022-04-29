using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggledButtons : MonoBehaviour
{
    public enum Type
    {
        Music,
        SFX
    }

    public Type ToggleType = Type.Music;
    public Sprite Enabled;
    public Sprite Disabled;

    private Image UIImage;

    private void Start()
    {
        UIImage = gameObject.GetComponent<Image>();
    }

    public void Toggle()
    {
        //Get type
        if(ToggleType == Type.Music)
        {
            //Get data from SystemManager
            if (SystemManager.Instance.Music) //If enabled
                UIImage.sprite = Enabled;
            else //If disabled
                UIImage.sprite = Disabled;

        }
        else if(ToggleType == Type.SFX)
        {
            //Get data from SystemManager
            if (SystemManager.Instance.SFX) //If enabled
                UIImage.sprite = Enabled;
            else //If disabled
                UIImage.sprite = Disabled;
        }
    }
}
