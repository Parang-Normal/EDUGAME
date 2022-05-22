using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StatusSprite
{
    public Sprite Purchase;
    public Sprite Equipped;
    public Sprite Unequipped;
}

[System.Serializable]
public class ConfirmationPanels
{
    public GameObject ConfirmPanel;
    public GameObject RejectPanel;
}

public class Accessory : MonoBehaviour
{
    public enum ItemStatus
    {
        Purchase,
        Equipped,
        Unequipped
    }

    public StatusSprite Sprites;
    public ConfirmationPanels Panels;
    public string ID = "";
    public int Price = 0;
    public ItemStatus Status = ItemStatus.Purchase;

    private bool Purchased = false;
    private bool Equipped = false;
    private string PurchaseID;
    private string EquipID;
    private Image ButtonSprite;

    private void Awake()
    {
        PurchaseID = ID + "_Purchased";
        EquipID = ID + "_Equipped";

        int p = PlayerPrefs.GetInt(PurchaseID, 0);
        int e = PlayerPrefs.GetInt(EquipID, 0);

        if (p == 0)
            Purchased = false;
        else if (p == 1)
            Purchased = true;
        else
            Debug.Log("Wrong value stored for purchase: " + p);

        if (e == 0)
            Equipped = false;
        else if (e == 1)
            Equipped = true;
        else
            Debug.Log("Wrong value stored for equip: " + e);

        ButtonSprite = transform.Find("Buy").GetComponent<Image>();
    }

    private void Start()
    {
        ChangeStatus();
    }

    private void ChangeStatus()
    {
        //Not yet bought
        if (Purchased == false)
        {
            ButtonSprite.sprite = Sprites.Purchase;
            Status = ItemStatus.Purchase;
        }
        //Bought
        else if (Purchased == true)
        {
            //Not yet equipped
            if (Equipped == false)
            {
                ButtonSprite.sprite = Sprites.Unequipped;
                Status = ItemStatus.Unequipped;
            }
            //Is equipped
            else if (Equipped == true)
            {
                ButtonSprite.sprite = Sprites.Equipped;
                Status = ItemStatus.Equipped;
            }
        }
    }

    private void Equip(bool equip)
    {
        Equipped = equip;

        if (equip)
            PlayerPrefs.SetInt(EquipID, 1);
        else
            PlayerPrefs.SetInt(EquipID, 0);

        ChangeStatus();
    }

    private void Purchase()
    {
        int playerCoins = PlayerPrefs.GetInt("Fragments", 0);

        //If player's fragments is not enough to buy item
        if (playerCoins - Price < 0)
            Panels.RejectPanel.SetActive(true);

        //If player cana afford item
        else
            Panels.ConfirmPanel.SetActive(true);
    }

    public void ConfirmPurchase()
    {
        Purchased = true;
        Equipped = true;

        PlayerPrefs.SetInt(PurchaseID, 1);
        PlayerPrefs.SetInt(EquipID, 1);

        int coins = PlayerPrefs.GetInt("Fragments", 0);
        PlayerPrefs.SetInt("Fragments", coins - Price);

        CustomizationManager.Instance.UpdateFragments();

        ChangeStatus();
    }

    public void Interact()
    {
        switch (Status)
        {
            //Purchase Item
            case ItemStatus.Purchase:
                Purchase();
                break;

            //Item is currently equipped so unequip item
            case ItemStatus.Equipped:
                Equip(false);
                break;

            //Item is currently unequipped so equip item
            case ItemStatus.Unequipped:
                Equip(true);
                break;

            default:
                Debug.Log("Unknown status for item: " + ID);
                break;
        }
    }
    
}
