using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "ScriptableObjects/Fishing/Fish")]
public class Fish : ScriptableObject
{
    public FishType type;
    public float weight;
    public float pointValue;
}
