using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public QuestList questList;
    public GameObject questNotificationUI;

    private GameManager gm;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gameObject.SetActive(true);
        // setting the "Quests" gameobject's children to false so that way the QuestList Start function can create all the quests
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void AddQuestToMenu(Quest q, Quest prevQuest)
    {
        if (prevQuest != null)
        {
            questList.buttonGameObjects.TryGetValue(prevQuest, out GameObject prevButton);
            if (prevButton)
            {
                prevButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                Debug.Log($"No quest button gameobject created for {prevQuest}");
            }
        }
        Debug.Log("Button Game objects: " + questList.buttonGameObjects);
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
