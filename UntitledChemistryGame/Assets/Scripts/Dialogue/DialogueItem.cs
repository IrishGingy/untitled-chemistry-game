using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueItem", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueItem : ScriptableObject
{
    public string characterName;
    public TextAsset inkFile;
    public bool played;
    public DialogueItem dependency = null;
}
