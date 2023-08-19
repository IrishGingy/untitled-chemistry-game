using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Fish", menuName = "ScriptableObjects/Fishing/Fish")]
public class Fish : ScriptableObject
{
    public Sprite icon;
    public float minWeight;
    public float maxWeight;
    public float avgWeight;
    public float pricePerPound;
}
