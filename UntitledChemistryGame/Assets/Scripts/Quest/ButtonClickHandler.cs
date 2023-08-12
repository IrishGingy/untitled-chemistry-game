using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public QuestList questList;

    private void Start()
    {
        questList = FindObjectOfType<QuestList>();
    }

    public void SelectQuest(Button clickedButton)
    {
        if (questList.activeQuestButton != null)
        {
            foreach (Quest q in questList.quests)
            {
                if (q.activeQuest)
                {
                    q.activeQuest = false;
                }
            }

            questList.activeQuestButton.transform.GetChild(1).GetComponent<Image>().enabled = false;
            if (questList.activeQuestButton == clickedButton)
            {
                questList.activeQuestButton = null;
                return;
            }
        }
        questList.quests[Int32.Parse(clickedButton.gameObject.name.Substring(clickedButton.gameObject.name.Length - 1))].activeQuest = true;
        clickedButton.transform.GetChild(1).GetComponent<Image>().enabled = true;
        questList.activeQuestButton = clickedButton;
    }
}
