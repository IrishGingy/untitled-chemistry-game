using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    public GameObject questButtonPrefab;
    public Transform questListParent;
    public Button activeQuestButton;
    public Button[] buttons;
    public Quest[] quests;
    
    private int id;

    // Start is called before the first frame update
    void Start()
    {
        id = 0;
        foreach(Quest quest in quests)
        {
            GameObject button = Instantiate(questButtonPrefab, questListParent);
            button.name = $"Quest {id}";
            button.GetComponentInChildren<TextMeshProUGUI>().text = quest.title;
            // disable the gameobject indicating the active quest
            button.transform.GetChild(1).GetComponent<Image>().enabled = false;
            // TODO: This might be an issue later...
            quest.activeQuest = false;
            id++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
