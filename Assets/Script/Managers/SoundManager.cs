using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXClip
{
    Jump,
    Fragment,
    Win,
    Lose,
    PlayerWalk,
    BotWalk
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public List<AudioClip> MusicList;
    public List<AudioClip> SFXList;

    private AudioSource MusicPlayer;
    private AudioSource SFXPlayer;

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

        MusicPlayer = gameObject.AddComponent<AudioSource>();
        SFXPlayer = gameObject.AddComponent<AudioSource>();

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        MusicPlayer.clip = MusicList[0];
        SFXPlayer.clip = SFXList[0];

        MusicPlayer.loop = true;
        MusicPlayer.Play();

        /*
        SFXPlayer.loop = true;
        SFXPlayer.Play();
        */
    }

    public void PlaySFX(SFXClip label)
    {
        switch (label)
        {
            case SFXClip.Jump:
                SFXPlayer.clip = SFXList[2];
                break;

            case SFXClip.Fragment:
                SFXPlayer.clip = SFXList[1];
                break;

            case SFXClip.Win:
                SFXPlayer.clip = SFXList[6];
                break;

            case SFXClip.Lose:
                SFXPlayer.clip = SFXList[3];
                break;

            case SFXClip.PlayerWalk:
                SFXPlayer.clip = SFXList[4];
                break;

            case SFXClip.BotWalk:
                SFXPlayer.clip = SFXList[5];
                break;
        }

        SFXPlayer.Play();
    }

    public void MuteMusic(bool mute)
    {
        MusicPlayer.mute = mute;
    }
    public void MuteSFX(bool mute)
    {
        SFXPlayer.mute = mute;
    }
}
