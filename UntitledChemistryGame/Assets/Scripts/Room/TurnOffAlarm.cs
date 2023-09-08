using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAlarm : MonoBehaviour
{
    public GameObject textPrompt;
    public GameObject alarmClockCam;
    public GameObject player;

    private GameManager gm;

    void Start()
    {
        alarmClockCam.SetActive(true);
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // turn off alarm, wait a second, get out of bed
            StartCoroutine(GetUp());
        }
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(1f);
        alarmClockCam.SetActive(false);
        player.SetActive(true);
        textPrompt.SetActive(false);
    }
}
