using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    [SerializeField] Image previewImage;

    Item item;
    Upgrade upgrade;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void AddUpgrade(Upgrade newUpgrade)
    {
        upgrade = newUpgrade;

        icon.sprite = upgrade.icon;
        icon.enabled = true;
        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    /*public void UseItem()
    {
        if (item != null)
        {
            item.Use(item);
        }
    }*/

    public void PreviewItem()
    {
        if (item != null)
        {
            item.Preview(item, previewImage);
        }
    }
}
