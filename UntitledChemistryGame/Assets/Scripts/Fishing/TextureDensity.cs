using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Texture Density", menuName = "ScriptableObjects/Fishing/TextureDensity")]
public class TextureDensity : ScriptableObject
{
    public TerrainLayer terrainLayer;
    public float density;
    public Fish[] fish;
}
