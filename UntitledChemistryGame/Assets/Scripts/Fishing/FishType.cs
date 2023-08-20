using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Fish Type", menuName = "ScriptableObjects/Fishing/FishType")]
public class FishType : ScriptableObject
{
    public Sprite icon;
    public float minWeight;
    public float maxWeight;
    public float avgWeight;
    public float pricePerPound;
}
