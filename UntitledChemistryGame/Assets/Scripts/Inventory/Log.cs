using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Log : MonoBehaviour
{
    #region Singleton

    public static Log instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Log found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public GameObject logItemsParent;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        /*
        if (!item.isDefaultItem) 
        { 
            items.Add(item);
        }
        */
        if (items.Count >= space)
        {
            //Debug.Log("Not enough room.");
            return false;
        }
        items.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Update(Item item)
    {
        InventorySlot[] logSlots = logItemsParent.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in logSlots)
        {
            if (slot.item)
            {
                if (item.fishType == slot.item.fishType)
                {
                    float min = 0f;
                    float max = 0f;

                    TextMeshProUGUI weightGUI = slot.weight.GetComponentInChildren<TextMeshProUGUI>();
                    string text = weightGUI.text.Replace(" lbs", "");
                    if (text.Contains("-"))
                    {
                        string[] parts = text.Split("-");
                        min = float.Parse(parts[0]);
                        max = float.Parse(parts[1]);
                        if (!(item.weight == min || item.weight == max))
                        {
                            if (item.weight > max)
                            {
                                max = item.weight;
                            }
                            else if (item.weight < min)
                            {
                                min = item.weight;
                            }

                            weightGUI.text = $"{min}-{max} lbs";
                        }
                    }
                    else
                    {
                        float oldWeight = float.Parse(text);
                        Debug.Log("Old Weight: " + oldWeight);
                        Debug.Log("New Item weight: " + item.weight);
                        Debug.Log("New Item Weight Type: " + item.weight.GetType().ToString());
                        if (item.weight > oldWeight)
                        {
                            Debug.Log("It's greater!");
                            min = oldWeight;
                            max = item.weight;
                        }
                        else if (item.weight < oldWeight)
                        {
                            Debug.Log("It's lower!");
                            min = item.weight;
                            max = oldWeight;
                        }

                        weightGUI.text = $"{min}-{max} lbs";
                    }
                }
            }
        }
    }
}
