using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Quest", menuName = "ScriptableObjects/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public bool activeQuest;
}
