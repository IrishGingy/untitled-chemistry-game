using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform inventoryItemsParent;
    public Transform logItemsParent;
    public GameObject inventoryUI;
    public QuestManager qm;
    //public GameObject player;

    [SerializeField] Image previewImage;

    Inventory inventory;
    Log log;
    //bool camLook;
    PlayerController playerController;

    InventorySlot[] inventorySlots;
    InventorySlot[] logSlots;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        // Disables inventory UI on start.
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }

        inventory = Inventory.instance;
        log = Log.instance;
        Debug.Log("Inventory: " + inventory);
        Debug.Log("Log:" + log);
        inventory.onItemChangedCallback += UpdateInventoryUI;
        log.onItemChangedCallback += UpdateLogUI;

        inventorySlots = inventoryItemsParent.GetComponentsInChildren<InventorySlot>();
        logSlots = logItemsParent.GetComponentsInChildren<InventorySlot>();
        //camLook = true;
        //playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Inventory"))
        //{
        //    gm.ToggleInventoryMenu(inventoryUI, previewImage);
        //}
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].AddItem(inventory.items[i], true);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }

        // Check if a quest has been completed
        //qm.QuestCheck();
        //Debug.Log("UPDATING UI");
    }

    public void UpdateLogUI()
    {
        for (int i = 0; i < logSlots.Length; i++)
        {
            if (i < log.items.Count)
            {
                logSlots[i].AddItem(log.items[i], false);
            }
            else
            {
                logSlots[i].ClearSlot();
            }
        }

        // Check if a quest has been completed
        //qm.QuestCheck();
        //Debug.Log("UPDATING UI");
    }
}
