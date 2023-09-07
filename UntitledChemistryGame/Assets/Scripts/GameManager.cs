using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Used by the "Trigger" abstract class
    public GameObject buttonPrompt;
    public DialogueItem nextSceneTrigger;

    public Player player;
    public GameObject pauseMenu;
    public QuestManager qm;
    public FishingManager fm;
    public BookManager bm;

    [Header("UI")]
    public GameObject inventoryUI;
    public Image previewImage;
    public GameObject questUI;
    public GameObject bookUI;
    public GameObject bookNotificationUI;

    [Header("Game States")]
    public Scene currentScene;
    public bool atMainMenu;
    public bool inMenus;
    public bool inPauseMenu;
    public bool canOpenMenus;
    // Used in special circumstances where the player shouldn't be able to open menus (except pause menu)
    public bool lockMenus;

    [Header("Dock")]
    public Transform spawnLocation;
    public Transform boatDockLocation;
    
    [Header("Settings set from the 'OptionsMenu' of the main menu")]
    public bool letterScrolling;

    private void Start()
    {
        player = GetComponent<Player>();
        buttonPrompt.SetActive(false);
        letterScrolling = true;
        currentScene = SceneManager.GetActiveScene();
        //StartGame();
    }

    private void Update()
    {
        CheckUserInput();
    }

    private void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestMenu(questUI.transform.GetChild(0).gameObject.activeSelf);
        }
        else if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventoryMenu(inventoryUI.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBookMenu(bookUI.transform.GetChild(0).gameObject.activeSelf);
        }
        // DEBUG: REMOVE THIS BEFORE RELEASE!!! (TODO)
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
        //else if (Input.GetKeyDown(KeyCode.T))
        //{
        //    qm.AddQuestToMenu(qm.questList.quests[0]);
        //}
    }

    private void StartGame()
    {
        //player.DockBoat(spawnLocation, boatDockLocation);
    }

    public void AddQuest(Quest q, Quest prevQuest)
    {
        qm.AddQuestToMenu(q, prevQuest);
    }

    public void LoadNextScene()
    {
        Scene _currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_currentScene.buildIndex + 1);
    }

    public void TogglePauseMenu()
    {
        if (!atMainMenu && !inMenus)
        {
            // TODO: In Unity, the escape button already does this. Make sure this is also the case on the final build
            //Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
            inPauseMenu = inPauseMenu ? false : true;
            player.TogglePlayerMovement();
            if (inPauseMenu)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            pauseMenu.SetActive(inPauseMenu);
        }
        else
        {
            //Debug.Log("Can't pause when at the main menu.");
        }
    }

    public void ToggleInventoryMenu(bool active)
    {
        if (canOpenMenus)
        {
            inMenus = !inMenus;
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
            player.TogglePlayerMovement();
            inventoryUI.SetActive(!active);
            //if (inventoryUI.activeSelf == false)
            //{
            //    previewImage.sprite = null;
            //    previewImage.enabled = false;
            //}
        }
    }

    public void ToggleQuestMenu(bool active)
    {
        Debug.Log($"Active: {active}");
        Debug.Log($"canOpenMenus: {canOpenMenus}");
        if (canOpenMenus)
        {
            inMenus = !inMenus;
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
            player.TogglePlayerMovement();
            //questUI.SetActive(!active);
            foreach (Transform child in questUI.transform)
            {
                child.gameObject.SetActive(!active);
            }
            Debug.Log($"QuestUI: {questUI}");
            Debug.Log($"QuestUI Active?: {questUI.activeSelf}");
        }
    }

    public void ToggleBookMenu(bool active)
    {
        if (canOpenMenus)
        {
            inMenus = !inMenus;
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
            player.TogglePlayerMovement();
            bookUI.transform.GetChild(0).gameObject.SetActive(!active);
            bookUI.transform.GetChild(1).gameObject.SetActive(!active);
            //bookUI.SetActive(!active);
        }
    }

    public void ShowBookNotification()
    {
        StartCoroutine(BookNotification());
    }

    private IEnumerator BookNotification()
    {
        bookNotificationUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        bookNotificationUI.SetActive(false);
    }
}

