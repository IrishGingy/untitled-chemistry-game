using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    //public GameObject player;

    [SerializeField] Image previewImage;

    Inventory inventory;
    bool camLook;
    PlayerController playerController;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        // Disables inventory UI on start.
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        camLook = true;
        //playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf == false)
            {
                previewImage.sprite = null;
                previewImage.enabled = false;
            }

            if (camLook)
            {
                // TODO: this should be done on a first person controller (the player shouldn't be able to look around or move with the inventory open)
                //playerController.lookingAtUI = false;
                Cursor.lockState = CursorLockMode.None;
                camLook = false;
            }
            else
            {
                // TODO: this should be done on a first person controller (the player shouldn't be able to look around or move with the inventory open)
                //playerController.lookingAtUI = true;
                //Cursor.lockState = CursorLockMode.Locked;
                camLook = true;
            }
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("UPDATING UI");
    }

}
