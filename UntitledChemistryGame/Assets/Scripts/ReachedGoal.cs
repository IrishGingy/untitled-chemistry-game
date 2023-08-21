using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachedGoal : MonoBehaviour
{
    [Header("Goal Result")]
    public bool catchFish;

    private GameManager gm;
    private FishDensity fishDensity;
    private FishWeightGenerator weightGenerator;
    private Inventory inventory;
    private FishingManager fishingManager;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        fishingManager = gm.fm;
        fishDensity = gm.player.boat.GetComponent<FishDensity>();
        weightGenerator = gm.GetComponent<FishWeightGenerator>();
        inventory = gm.GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && catchFish)
        {
            // hide fishingUI
            // change cameras
            fishingManager.ExitFishing();
            //gm.player.thirdPersonMovement.gameObject.SetActive(true);
            //fishingUI.SetActive(false);

            // can pass in amount of times hitting the walls to make weight of fish lower the more you hit the walls
            CatchFish();
        }
    }

    void CatchFish()
    {
        // calculate the point rating of the fish based on the weight and type
        // use depth (deeper = larger)

        FishType[] fishTypes = fishDensity.currentTextureDensity.fish;
        int i = Random.Range(0, fishTypes.Length);
        FishType fishType = fishDensity.currentTextureDensity.fish[i];

        var result = weightGenerator.CalculateWeightAndPoints(fishType);
        float weight = result.Item1;
        float pointValue = result.Item2;

        Item item = Item.CreateFish(fishType, weight, pointValue);

        inventory.Add(item);
    }
}
