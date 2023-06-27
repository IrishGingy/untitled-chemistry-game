using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    #region Singleton

    public static UpgradeMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of UpgradeMenu found!");
            return;
        }

        instance = this;
    }

    #endregion

    [SerializeField] private Canvas upgradeMenu;

    public List<Upgrade> upgradeList = new List<Upgrade>(); 
    public Transform itemsParent;
    public SimpleBoatController boatController;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        upgradeMenu.enabled = false;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            upgradeMenu.enabled = !upgradeMenu.enabled;
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < upgradeList.Count)
            {
                slots[i].AddUpgrade(upgradeList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("UPDATING UI");
    }

    public void IncreaseBoatSpeed()
    {
        float oldSpeed = boatController.maxMoveSpeed;
        boatController.maxMoveSpeed *= 2;
        Debug.Log($"Boat speed increased from [{oldSpeed}] to [{boatController.maxMoveSpeed}]");
    }
}
