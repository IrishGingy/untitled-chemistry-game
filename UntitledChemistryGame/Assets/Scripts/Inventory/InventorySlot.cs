using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Image usedIcon;
    public GameObject weight;

    [SerializeField] Image previewImage;

    Item item;
    Upgrade upgrade;
    TextMeshProUGUI weightTextGUI;

    private void Awake()
    {
        weightTextGUI = weight.GetComponentInChildren<TextMeshProUGUI>();
        weight.SetActive(false);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        if (newItem.fishType != null)
        {
            weightTextGUI.text = $"{newItem.weight} lbs";
            weight.SetActive(true);
        }
        //removeButton.interactable = true;
    }

    public void AddUpgrade(Upgrade newUpgrade)
    {
        upgrade = newUpgrade;

        icon.sprite = upgrade.icon;
        icon.enabled = true;
        usedIcon.enabled = false;
        // can add tooltip in here
        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        //removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    //public void UseItem()
    //{
    //    if (item != null)
    //    {
    //        item.Use(item);
    //    }
    //}

    public void UseUpgrade()
    {
        if (upgrade != null)
        {
            upgrade.Use(upgrade);
            usedIcon.enabled = true;
        }
    }

    public void ShowTooltip()
    {
        if (upgrade != null)
        {
            upgrade.Tooltip(upgrade);
        }
    }

    public void PreviewItem()
    {
        if (item != null)
        {
            item.Preview(item, previewImage);
        }
    }
}
