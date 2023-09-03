using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    [Header("Fish Item")]
    public FishType fishType = null;
    public float weight = -1f;
    public float pointValue = 0f;

    // put a field on here that signifies that this item is getting stacked in the log (a fishType of this type already exists so we need to just update the weight range).

    public static Item CreateFish(FishType type, float weight, float pointValue)
    {
        Item newItem = CreateInstance<Item>();
        newItem.icon = type.icon;
        newItem.weight = weight;
        newItem.fishType = type;
        newItem.pointValue = pointValue;

        return newItem;
    }

    // "Virtual" derives different objects from the item and that way you can do different things to different items.
    public virtual void Use(Item item)
    {
        if(item.name.Contains("Note"))
        {
            // Change preview image
            //Debug.Log("Something has been done...I think");
        }
        if (item.name.Contains("List"))
        {
            // Change preview image
            //Debug.Log("Using " + name);

        }
    }

    public virtual void Preview(Item item, Image preview)
    {
        preview.sprite = icon;
        preview.enabled = true;
        //Debug.Log("Previewing " + name);
    }
}
