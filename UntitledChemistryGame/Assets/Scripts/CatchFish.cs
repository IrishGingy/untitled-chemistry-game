using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFish : MonoBehaviour
{
    FishingManager fm;

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FishingManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fm.FishOn();
        }
    }
}
