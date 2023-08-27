using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestList questList;
    public GameObject questNotificationUI;

    private GameManager gm;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void AddQuestToMenu(Quest q)
    {
        questList.buttonGameObjects.TryGetValue(q, out GameObject button);
        if (button)
        {
            button.SetActive(true);
            StartCoroutine(ShowQuestNotification());
        }
        else
        {
            Debug.Log($"No quest button gameobject created for {q}");
        }
    }

    private IEnumerator ShowQuestNotification()
    {
        questNotificationUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        questNotificationUI.SetActive(false);
    }

    public void QuestCheck()
    {
        // number of fish, 
        Debug.Log("Checking if a quest was completed...");
    }
}
