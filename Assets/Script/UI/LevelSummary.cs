using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour
{
    public Text Lives = null;
    public Text Time = null;
    public Text Gems = null;

    // Start is called before the first frame update
    void Start()
    {
        Lives.text = "x" + GameMode.Instance.Lives.ToString();
        Time.text = (Mathf.FloorToInt(GameMode.Instance.Seconds / 60)).ToString("0") + ":" + (GameMode.Instance.Seconds % 60).ToString("00");
        Gems.text = "x" + GameMode.Instance.Gems.ToString();

        //int g = PlayerPrefs.GetInt("Gems", 0);
        //g += GameMode.Instance.Gems;
        //PlayerPrefs.SetInt("Gems", g);
    }
}
