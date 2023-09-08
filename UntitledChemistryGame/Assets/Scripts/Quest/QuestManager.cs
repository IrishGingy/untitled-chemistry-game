using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public QuestList questList;
    public GameObject questNotificationUI;
    public FishType questCompleteType;
    public Quest bookTutorialQuest;
    //public Quest numFishQuest;
    public Quest mainFishingQuest;

    private GameManager gm;
    private Inventory inventory;
    private int numberFishToCatch;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        inventory = gm.GetComponent<Inventory>();
        numberFishToCatch = 5;

        gameObject.SetActive(true);
        // setting the "Quests" gameobject's children to false so that way the QuestList Start function can create all the quests
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void AddQuestToMenu(Quest q, Quest prevQuest)
    {
        CompleteQuest(prevQuest);
        //if (prevQuest != null)
        //{
        //    questList.buttonGameObjects.TryGetValue(prevQuest, out GameObject prevButton);
        //    if (prevButton)
        //    {
        //        prevButton.GetComponent<Button>().interactable = false;
        //    }
        //    else
        //    {
        //        Debug.Log($"No quest button gameobject created for {prevQuest}");
        //    }
        //}
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
        q.inList = true;
    }

    public void CompleteQuest(Quest q)
    {
        if (q != null)
        {
            questList.buttonGameObjects.TryGetValue(q, out GameObject questButton);
            if (questButton)
            {
                questButton.GetComponent<Button>().interactable = false;
                q.completed = true;
            }
            else
            {
                Debug.Log($"No quest button gameobject created for {q}");
            }
        }
    }

    private IEnumerator ShowQuestNotification()
    {
        questNotificationUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        questNotificationUI.SetActive(false);
    }

    public void QuestCheck(Item item)
    {
        // number of fish, 
        Debug.Log("Checking if a quest was completed...");
        if (item.fishType == questCompleteType)
        {
            if (!bookTutorialQuest.inList)
            {
                AddQuestToMenu(bookTutorialQuest, null);
            }
        }
        if (inventory.items.Count >= numberFishToCatch && bookTutorialQuest.completed)
        {
            CompleteQuest(mainFishingQuest);
        }

        //if (numFishQuest.completed && )
        //{
        //    AddQuestToMenu(mainFishingQuest, null);
        //}
    }
}
