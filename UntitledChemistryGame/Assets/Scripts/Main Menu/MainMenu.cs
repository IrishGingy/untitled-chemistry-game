using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject mainMenuCam;
    public Quest firstQuest;

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gm.atMainMenu = true;
        gm.canOpenMenus = false;
        playerCam.SetActive(false);
        gm.player.boat.SetActive(false);
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gm.atMainMenu = false;
        gm.canOpenMenus = true;
        gm.qm.AddQuestToMenu(firstQuest, null);

        // Later smoothly transition to the playerCam
        gm.player.boat.SetActive(true);
        playerCam.SetActive(true);
        mainMenuCam.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        //Debug.Log("Game quit.");
    }

    public void LetterScrolling(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0)
        {
            gm.letterScrolling = true;
        }
        else if (dropdown.value == 1)
        {
            gm.letterScrolling = false;
        }
    }
}