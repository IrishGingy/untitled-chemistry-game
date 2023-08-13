using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Used by the "Trigger" abstract class
    public GameObject buttonPrompt;
    public DialogueItem nextSceneTrigger;

    public Player player;

    [Header("Game States")]
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
        //StartGame();
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
        if (!atMainMenu)
        {
            inPauseMenu = inPauseMenu ? false : true;
        }
        else
        {
            Debug.Log("Can't pause when at the main menu.");
        }
    }

    public void ToggleInventoryMenu(GameObject ui, Image previewImage)
    {
        if (canOpenMenus)
        {
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

