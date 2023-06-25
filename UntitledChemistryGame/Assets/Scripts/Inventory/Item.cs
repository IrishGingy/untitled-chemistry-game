using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    // "Virtual" derives different objects from the item and that way you can do different things to different items.
    public virtual void Use(Item item)
    {
        if(item.name.Contains("Note"))
        {
            // Change preview image
            Debug.Log("Something has been done...I think");
        }
        if (item.name.Contains("List"))
        {
            // Change preview image
            Debug.Log("Using " + name);

        }
    }

    public virtual void Preview(Item item, Image preview)
    {
        preview.sprite = icon;
        preview.enabled = true;
        Debug.Log("Previewing " + name);
    }
}
