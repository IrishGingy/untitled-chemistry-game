using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class Page : MonoBehaviour
{
    // 0 indicates it is the left page, while 1 indicates it is the right page
    public int pageLeftOrRight;
    public GameObject clickInstructions;

    public event Action ContentChanged;

    [Header("Content UI Objects")]
    public Image locationIcon;
    public Image nameIcon;
    public TextMeshProUGUI description;
    public Text fishName;

    private GameManager gm;
    private BookManager bm;
    private bool initialized;

    private PageContent _content;
    public PageContent content
    {
        get { return _content; }
        set
        {
            _content = value;
            OnContentChanged(); // Call the event when content changes
        }
    }

    private void OnContentChanged()
    {
        // Call the UpdateUI function whenever content changes
        if (ContentChanged != null)
        {
            ContentChanged.Invoke();
        }
    }

    private void Start()
    {
        ContentChanged += UpdateUI;
        ContentChanged();
    }

    public void UpdateUI()
    {
        if (!initialized)
        {
            gm = FindObjectOfType<GameManager>();
            bm = gm.bm;
            initialized = true;
        }

        //Debug.Log(pageLeftOrRight);
        //Debug.Log(bm);
        //Debug.Log(bm.leftPageIndex);
        int index = pageLeftOrRight == 0 ? bm.leftPageIndex : bm.rightPageIndex;
        PageContent curPageContent = bm.pageContents[index];

        //// Your logic to update the UI here
        //Debug.Log("UI Updated");
        //Debug.Log("Content: " + _content.fishName);
        //Debug.Log("Content: " + _content.description);
        //Debug.Log("Content: " + _content.icon);
        fishName.text = _content.fishName;
        description.text = _content.description;
        nameIcon.sprite = _content.icon;
        locationIcon.sprite = _content.mapLocation;

        //curPageContent.icon = _content.icon;
        ////curPageContent.mapLocation = _content.location;
        ///
        //Debug.Log("curpage icon: " + curPageContent.icon);
        //Debug.Log("fishtype icon: " + bm.fishTypes[2].icon);
        //if (curPageContent.icon == bm.fishTypes[2].icon)
        //{
        //    curPageContent.completed = true;
        //}

        //curPageContent.completed = IsPageComplete();
        Button locationButton = locationIcon.GetComponent<Button>();
        Button nameButton = nameIcon.transform.parent.GetComponent<Button>();
        if (curPageContent.completed)
        {
            locationButton.enabled = false;
            nameButton.enabled = false;
            clickInstructions.SetActive(false);
        }
        else
        {
            if (bm.leftPageIndex != 2)
            {
                locationButton.enabled = true;
            }
            nameButton.enabled = true;
            clickInstructions.SetActive(true);
        }
    }

    //private bool IsPageComplete()
    //{
    //    if (fishName.text == "Peach Bearded Fish")
    //    {
    //        // TODO: THIS IS VERY MUCH HARDCODED, CHANGE LATER!!!
    //        FishType fType = bm.fishTypes[2];
    //        if (fType.icon != nameIcon.sprite)
    //        {
    //            Debug.Log("Name icon is wrong!");
    //            return false;
    //        }
    //        return true;
    //    }

    //    return true;
    //}
}
