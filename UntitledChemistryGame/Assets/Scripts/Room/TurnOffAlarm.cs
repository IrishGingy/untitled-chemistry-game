using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAlarm : MonoBehaviour
{
    public GameObject textPrompt;
    public GameObject alarmClockCam;

    private GameManager gm;

    void Start()
    {
        alarmClockCam.SetActive(true);
        gm = FindObjectOfType<GameManager>();
        Debug.Log(gm.player);
        Debug.Log(gm.player.playerPrefab);
        gm.player.playerPrefab.SetActive(false);
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
        gm.player.playerPrefab.SetActive(true);
        textPrompt.SetActive(false);
    }
}
