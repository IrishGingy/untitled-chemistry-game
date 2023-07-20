using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueItem", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueItem : ScriptableObject
{
    public string characterName;
    public TextAsset inkFile;
    /// <summary>
    /// A DialogueItem that must be played in order for this DialogueItem to be played.
    /// </summary>
    public DialogueItem dependency = null;
    [Header("Set in script")]
    /// <summary>
    /// Whether this specific dialogue has been played yet.
    /// </summary>
    public bool played;
}
