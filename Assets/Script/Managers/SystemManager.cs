using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles all player settings when launching the game
public class SystemManager : MonoBehaviour
{
    public static SystemManager Instance { get; private set; }

    public bool Music { get; private set; }
    public bool SFX { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Music = true;
        SFX = true;

        DontDestroyOnLoad(this);
    }

    public void SetMusic(bool set)
    {
        Music = set;
        SoundManager.Instance.MuteMusic(!set);
    }

    public void SetSFX(bool set)
    {
        SFX = set;
        SoundManager.Instance.MuteSFX(!set);
    }
}
