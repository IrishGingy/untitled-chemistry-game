using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public QuestList questList;
    public GameObject questNotificationUI;
    public GameObject generalNotificationUI;
    // this is used to enable the player to dock their boat after they've completed the main fishing quest
    public GameObject dockTrigger;
    public FishType questCompleteType;
    public Quest bookTutorialQuest;
    //public Quest numFishQuest;
    public Quest mainFishingQuest;
    public Quest talkToCoachQuest;

    private GameManager gm;
    private Inventory inventory;
    private int numberFishToCatch;
    private TextMeshProUGUI gNotificationText;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        inventory = gm.GetComponent<Inventory>();
        gNotificationText = generalNotificationUI.GetComponentInChildren<TextMeshProUGUI>();
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
        if (bookTutorialQuest.completed)
        {
            // add boost upgrade on hold shift
            gm.canBoost = true;
            StartCoroutine(ShowGeneralNotification("Upgrade acquired!\n- Engine V2 (Hold Shift)"));
        }
        if (inventory.items.Count >= numberFishToCatch && !bookTutorialQuest.completed)
        {
            // show general notification that num fish task has been completed
            StartCoroutine(ShowGeneralNotification($"{numberFishToCatch} fish successfully caught!"));
        }
        else if (inventory.items.Count >= numberFishToCatch && bookTutorialQuest.completed)
        {
            AddQuestToMenu(talkToCoachQuest, mainFishingQuest);
            // enable the player to dock their boat
            dockTrigger.SetActive(true);
        }

        //if (numFishQuest.completed && )
        //{
        //    AddQuestToMenu(mainFishingQuest, null);
        //}
    }

    private IEnumerator ShowGeneralNotification(string description)
    {
        gNotificationText.text = description;
        generalNotificationUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        generalNotificationUI.SetActive(false);
    }
}
