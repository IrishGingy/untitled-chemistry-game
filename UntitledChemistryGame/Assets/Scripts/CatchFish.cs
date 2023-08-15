using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFish : MonoBehaviour
{
    FishingManager fm;
    bool chosen;

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FishingManager>();
        //Debug.Log(fm.chosenCatchArea);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (fm.chosenCatchArea == gameObject.transform))
        {
            fm.FishOn();
        }
    }
}
