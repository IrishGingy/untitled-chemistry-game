using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Used by the "Trigger" abstract class
    public GameObject buttonPrompt;
    public DialogueItem nextSceneTrigger;

    public Player player;
    public GameObject pauseMenu;
    public QuestManager qm;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void StartGame()
    {
        //player.DockBoat(spawnLocation, boatDockLocation);
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
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
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

    public void ToggleInventoryMenu(GameObject ui, Image previewImage)
    {
        if (canOpenMenus)
        {
            inMenus = !inMenus;
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
            player.TogglePlayerMovement();
            ui.SetActive(!ui.activeSelf);
            if (ui.activeSelf == false)
            {
                previewImage.sprite = null;
                previewImage.enabled = false;
            }
        }
    }

    public void ToggleQuestMenu()
    {
        if (canOpenMenus)
        {

        }
    }
}

