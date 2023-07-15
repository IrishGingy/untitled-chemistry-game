using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueItem", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueItem : ScriptableObject
{
    public string characterName;
    public TextAsset inkFile;
    /// <summary>
    /// Whether this specific dialogue has been played yet.
    /// </summary>
    public bool played;
    /// <summary>
    /// A DialogueItem that must be played in order for this DialogueItem to be played.
    /// </summary>
    public DialogueItem dependency = null;
    /// <summary>
    /// A dependency where an event can only occur after this DialogueItem has been played.
    /// </summary>
    public EventDependency eventDependency = null;
}
