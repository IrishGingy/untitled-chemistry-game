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

    public Item item;
    Upgrade upgrade;
    TextMeshProUGUI weightTextGUI;

    private void Start()
    {
        Debug.Log("HiTTING AWAKE!");
        weightTextGUI = weight.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(weight);
        Debug.Log(weightTextGUI);
        Debug.Log(weightTextGUI.text);
        weight.SetActive(false);
    }

    public void AddItem(Item newItem, bool inventory)
    {
        if (inventory)
        {
            Debug.Log("Inventory!");
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
            if (newItem.fishType != null)
            {
                weightTextGUI = weight.GetComponentInChildren<TextMeshProUGUI>();
                weightTextGUI.text = $"{newItem.weight} lbs";
                weight.SetActive(true);
            }
            //removeButton.interactable = true;
        }
        else
        {
            Debug.Log("Log this time!");
            // does this already exist in log?
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
            if (newItem.fishType != null)
            {
                weightTextGUI = weight.GetComponentInChildren<TextMeshProUGUI>();
                weightTextGUI.text = $"{newItem.weight} lbs";
                weight.SetActive(true);
            }

            //if (weightTextGUI.text != "lbs")
            //{
            //    if (newItem.fishType != null)
            //    {
            //        float min = 0;
            //        float max = 0;
            //        string text = weightTextGUI.text.Replace(" lbs", "");

            //        if (text.Contains("-"))
            //        {
            //            string[] parts = text.Split("-");
            //            min = float.Parse(parts[0]);
            //            max = float.Parse(parts[1]);
            //            if (!(newItem.weight == min || newItem.weight == max))
            //            {
            //                if (newItem.weight > max)
            //                {
            //                    max = newItem.weight;
            //                }
            //                else if (newItem.weight < min)
            //                {
            //                    min = newItem.weight;
            //                }

            //                weightTextGUI.text = $"{min}-{max} lbs";
            //            }
            //        }
            //        else
            //        {
            //            float oldWeight = float.Parse(text);
            //            Debug.Log("Old Weight: " + oldWeight);
            //            Debug.Log("New Item weight: " + newItem.weight);
            //            Debug.Log("New Item Weight Type: " + newItem.weight.GetType().ToString());
            //            if (newItem.weight > oldWeight)
            //            {
            //                Debug.Log("It's greater!");
            //                min = oldWeight;
            //                max = newItem.weight;
            //            }
            //            else if (newItem.weight < oldWeight)
            //            {
            //                Debug.Log("It's lower!");
            //                min = newItem.weight;
            //                max = oldWeight;
            //            }

            //            weightTextGUI.text = $"{min}-{max} lbs";
            //        }
            //        //weightTextGUI.text = $"{newItem.weight} lbs";
            //        weight.SetActive(true);
            //    }
            //}
            //else
            //{
            //    if (newItem.fishType != null)
            //    {
            //        weightTextGUI.text = $"{newItem.weight} lbs";
            //        weight.SetActive(true);
            //    }
            //}
        }
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
