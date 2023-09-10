using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSceneLoader : MonoBehaviour
{
    private SceneLoader loader;
    private AudioSource doorAudio;

    // Start is called before the first frame update
    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
        doorAudio = GetComponent<AudioSource>();
        loader.knock = doorAudio;
    }
}
