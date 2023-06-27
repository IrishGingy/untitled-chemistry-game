using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    new public string name = "New Upgrade";
    public string description = "";
    public Sprite icon = null;
    // Upgrade Ability
    // Upgrade Category (fishing gear, engine, lighting, etc.)

    // "Virtual" derives different objects from the item and that way you can do different things to different items.
    public virtual void Use(Item item)
    {
        if (item.name.Contains("Engine"))
        {
            // Change preview image
            Debug.Log("Changing max speed of boat...");
        }
    }

    public virtual void Tooltip(Upgrade upgrade)
    {
        // display the description of this upgrade on mouse hover
    }
}
