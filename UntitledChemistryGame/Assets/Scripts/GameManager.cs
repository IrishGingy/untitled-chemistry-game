using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadOnlyAttribute : PropertyAttribute { }

public class GameManager : MonoBehaviour
{
    // Used by the "Trigger" abstract class
    public GameObject buttonPrompt;
    [Header("Dock")]
    public Transform spawnLocation;
    public Transform boatDockLocation;

    private Player player;

    [Header("Don't set")] 
    private Scene _currentScene;

    private void Start()
    {
        player = GetComponent<Player>();
        buttonPrompt.SetActive(false);
        _currentScene = SceneManager.GetActiveScene();
        //StartGame();
    }

    private void StartGame()
    {
        //player.DockBoat(spawnLocation, boatDockLocation);
    }


}

