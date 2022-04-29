using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public void MuteMusic(bool mute)
    {
        MusicPlayer.mute = mute;
    }
    public void MuteSFX(bool mute)
    {
        SFXPlayer.mute = mute;
    }
}
