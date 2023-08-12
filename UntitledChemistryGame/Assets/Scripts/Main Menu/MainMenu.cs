using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject mainMenuCam;

    private void Start()
    {
        playerCam.SetActive(false);
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Later smoothly transition to the playerCam
        playerCam.SetActive(true);
        mainMenuCam.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Game quit.");
    }
}