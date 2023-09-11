using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Required")]
    public GameObject boat;
    public GameObject sea;
    public GameObject boatCam;
    public GameObject terrain;
    public Quest mainFishingQuest;
    public DialogueItem startDialogue;
    // used in IP2 scene to conditionally activate the dock trigger
    public GameObject dockTrigger;
    public DialogueItem notCreativeEnough;

    public AudioSource knock;

    private GameManager gm;

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if scene is IP2, then change dock event dependency dependent dialogue to "not creative enough"
        if (scene.name == "[f]IP2")
        {
            Debug.Log("THIS IS THE SCENE!");
            gm.canFish = true;
            gm.lockBook = false;
            // Set docking the boat to inactive until quest has been completed (prerequisiteTrigger)
            gm.AddQuest(mainFishingQuest, null);
            gm.GetComponent<DialogueManager>().PlayDialogue(startDialogue);
            dockTrigger.SetActive(false);
            dockTrigger.GetComponent<EventDependency>().dependentDialogue = notCreativeEnough;
            //dockDependency.preventedMethod = Methods.dock;
            //dockDependency.dependentDialogue = mainFishingQuest;
        }

        //Debug.Log(scene.name);
        //if (scene.name == "[r]AlarmClock")
        //{
        //    // play door knocking sound
        //    Debug.Log("KNOCK KNOCK KNOCK!!!");
        //    knock.Play();
        //}

        // This function will be called every time a scene is loaded.
        if (scene.name[1] == 'f')
        {
            // show fishing specific objects
            boat.SetActive(true);
            sea.SetActive(true);
            boatCam.SetActive(true);
            terrain.SetActive(true);
        }
        else if (scene.name[1] == 'r')
        {
            // hide fishing specific objects
            boat.SetActive(false);
            sea.SetActive(false);
            boatCam.SetActive(false);
            terrain.SetActive(false);
        }
    }
}