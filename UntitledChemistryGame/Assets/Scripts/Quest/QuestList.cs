using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    public GameObject questButtonPrefab;
    public Transform questListParent;
    public Transform questDetailsParent;
    public Button activeQuestButton;
    public Button[] buttons;
    public Quest[] quests;
    
    private int id;

    // Start is called before the first frame update
    void Start()
    {
        // This is to append to the name so I know which quest belongs to which button
        id = 0;
        foreach(Quest quest in quests)
        {
            Debug.Log("Instantiating...");
            GameObject button = Instantiate(questButtonPrefab, questListParent);
            button.name = $"Quest {id}";
            button.GetComponentInChildren<TextMeshProUGUI>().text = quest.title;
            // disable the gameobject indicating the active quest
            button.transform.GetChild(1).GetComponent<Image>().enabled = false;

            // TODO: This might be an issue later...
            quest.activeQuest = false;

            // Create quest details objects
            GameObject description = Instantiate(quest.descriptionObject, questDetailsParent);
            GameObject tasks = Instantiate(quest.tasksObject, questDetailsParent);
            description.GetComponent<TextMeshProUGUI>().text = quest.description;
            description.SetActive(false);
            tasks.SetActive(false);

            // set QuestButton fields to identify quest objects from button
            QuestButton qb = button.GetComponent<QuestButton>();
            qb.quest = quest;
            qb.questButtonObject = button;
            qb.questDescriptionObject = description;
            qb.questTasksObject = tasks;

            id++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
