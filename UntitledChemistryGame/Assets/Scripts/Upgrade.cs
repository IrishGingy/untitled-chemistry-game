using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    new public string name = "New Upgrade";
    public string description = "";
    public Sprite icon = null;
    public bool used = false;
    // Upgrade Ability
    // Upgrade Category (fishing gear, engine, lighting, etc.)

    // "Virtual" derives different objects from the item and that way you can do different things to different items.
    public virtual void Use(Upgrade upgrade)
    {
        if (upgrade.used)
        {
            Debug.LogWarning("This upgrade has already been used!");
            return;
        }

        if (upgrade.name.Contains("Engine"))
        {
            // Change preview image
            Debug.Log("Changing max speed of boat...");
            FindObjectOfType<UpgradeMenu>().IncreaseBoatSpeed();
            used = true;
        }
        else
        {
            Debug.Log("Doesn't do anything yet");
        }
    }

    public virtual void Tooltip(Upgrade upgrade)
    {
        // display the description of this upgrade on mouse hover
        Debug.Log("Tooltip: " + upgrade.description);
    }
}
