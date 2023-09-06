using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName[1] == 'f')
        {
            Debug.Log("FISHING SCENE!");
        }
        else if (currentSceneName[1] == 'r')
        {
            Debug.Log("ROOM SCENE");
        }
        else
        {
            Debug.LogError("Unfamiliar scene naming convention ([f] for fishing scene, [r] for room scene)");
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
