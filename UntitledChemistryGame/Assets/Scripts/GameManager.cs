using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Used by the "Trigger" abstract class
    public GameObject buttonPrompt;
    public DialogueItem nextSceneTrigger;
    [Header("Dock")]
    public Transform spawnLocation;
    public Transform boatDockLocation;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        buttonPrompt.SetActive(false);
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
}

