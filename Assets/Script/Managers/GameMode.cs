using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameElements
{
    public GameObject Player = null;
    public GameObject HUD = null;
    public GameObject MainCamera = null;
}

public class GameMode : MonoBehaviour
{
    public static GameMode Instance { get; private set; }

    public OpAssets OperationAssets;
    public GameElements Elements;

    public bool RunTimer = true;

    public string NextLevel = null;

    public float Seconds { get; private set; }
    public bool CountTime { get; set; }
    public Vector3 SpawnPosition { get; set; }
    public float CameraMinY { get; set; }
    public int Lives { get; set; }
    public int Gems { get; set; }
    public Operations Operation { get; private set; }
    public List<CameraShift> CameraShiftList { get; set; }

    private Text LivesText = null;
    private Text TimerText = null;
    private Text GemsText = null;
    private Image Item = null;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Lives = 4;
        Seconds = 0;
        Gems = 0;
        CountTime = RunTimer;
        Operation = Operations.NULL;
        CameraShiftList = new List<CameraShift>();

        LivesText = Elements.HUD.transform.Find("Stats").Find("Lives").Find("LivesText").GetComponent<Text>();
        TimerText = Elements.HUD.transform.Find("Stats").Find("Timer").Find("TimerText").GetComponent<Text>();
        GemsText = Elements.HUD.transform.Find("Stats").Find("Gems").Find("GemsText").GetComponent<Text>();
        Item = Elements.HUD.transform.Find("Stats").Find("Inventory").Find("Slot 1").Find("Item").GetComponent<Image>();

        LivesText.text = Lives.ToString("0");
        TimerText.text = Seconds.ToString("000");
        GemsText.text = Gems.ToString("000");
    }

    private void Update()
    {
        if(CountTime)
        {
            Seconds += Time.deltaTime;
            TimerText.text = Seconds.ToString("000"); ;
        }
    }

    public void ResetTimer()
    {
        Seconds = 0;
        TimerText.text = Seconds.ToString("000"); ;
    }

    public void PlayerDied()
    {
        Lives--;
        LivesText.text = Lives.ToString("0");

        //Spawn
        Elements.Player.transform.position = SpawnPosition;
        Elements.MainCamera.GetComponent<CameraMovement>().MinY = CameraMinY;

        //Reset Camera Shifts
        if(CameraShiftList.Count > 0)
        {
            for (int i = 0; i < CameraShiftList.Count; i++)
            {
                CameraShiftList[i].Triggered = false;
            }

            CameraShiftList.Clear();
        }
    }

    public void UpdateGems(int amount)
    {
        Gems += amount;
        GemsText.text = Gems.ToString("000");
    }

    public void CheckpointSave(Vector3 NewSpawnPos)
    {
        SpawnPosition = NewSpawnPos;
        CameraMinY = Elements.MainCamera.GetComponent<CameraMovement>().MinY;
    }

    public void UpdateItem(Operations op)
    {
        Operation = op;
        UpdateItemSprite();
    }

    public Operations GetItem()
    {
        Operations op;

        op = Operation;
        Operation = Operations.NULL;
        UpdateItemSprite();

        return op;
    }

    private void UpdateItemSprite()
    {
        switch (Operation)
        {
            case Operations.NULL:
                Item.sprite = null;
                Item.color = Color.clear;
                break;

            case Operations.Addition:
                Item.sprite = OperationAssets.Addicus;
                Item.color = Color.white;
                break;

            case Operations.Subtraction:
                Item.sprite = OperationAssets.Minium;
                Item.color = Color.white;
                break;

            case Operations.Multiplication:
                Item.sprite = OperationAssets.Multifly;
                Item.color = Color.white;
                break;

            case Operations.Division:
                Item.sprite = OperationAssets.TheVoid;
                Item.color = Color.white;
                break;

        }
    }
}
