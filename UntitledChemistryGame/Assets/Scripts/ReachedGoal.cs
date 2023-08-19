using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachedGoal : MonoBehaviour
{
    public GameObject fishingUI;

    [Header("Goal Result")]
    public bool catchFish;

    private GameManager gm;
    private FishDensity fishDensity;
    private FishWeightGenerator weightGenerator;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        fishDensity = gm.player.boat.GetComponent<FishDensity>();
        weightGenerator = gm.GetComponent<FishWeightGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && catchFish)
        {
            // hide fishingUI
            // change cameras
            gm.player.thirdPersonMovement.gameObject.SetActive(true);
            fishingUI.SetActive(false);

            // can pass in amount of times hitting the walls to make weight of fish lower the more you hit the walls
            CatchFish();
        }
    }

    void CatchFish()
    {
        // calculate the point rating of the fish based on the weight and type
        // use depth (deeper = larger)

        weightGenerator.CalculateWeight();
        Fish[] fishTypes = fishDensity.currentTextureDensity.fish;
        int i = Random.Range(0, fishTypes.Length-1);
        Debug.Log(fishDensity.currentTextureDensity.fish[i]);
    }
}
