using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Page Content", menuName = "ScriptableObjects/PageContent")]
public class PageContent : ScriptableObject
{
    public int pageIndex = 1;
    public string fishName;
    // this will be the greyed out version of the fishType
    public Sprite icon;
    public Sprite mapLocation;
    // description of the fishType
    public string description;

    [Header("Dropdown values")]
    public int locationValue;
    public int weightRangeValue;
    public FishType nameFishType;
}
