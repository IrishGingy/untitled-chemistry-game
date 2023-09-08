using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Quest", menuName = "ScriptableObjects/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public GameObject descriptionObject;
    public GameObject tasksObject;
    // whether this quest has been clicked in the quest menu
    public bool activeQuest;
    // whether this quest is in the list of quests
    public bool inList;
    public bool completed;
}
