using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour
{
    public static CustomizationManager Instance { get; private set; }

    public Text FragmentAmount;
    public InputField Name;

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

        UpdateFragments();
        Name.text = PlayerPrefs.GetString("PlayerName", "Player");
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("PlayerName", Name.text);
    }

    public void UpdateFragments()
    {
        FragmentAmount.text = PlayerPrefs.GetInt("Fragments", 0).ToString("000");
    }

}
