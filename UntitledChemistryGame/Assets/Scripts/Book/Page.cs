using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Page : MonoBehaviour
{
    // 0 indicates it is the left page, while 1 indicates it is the right page
    public int pageLeftOrRight;

    public event Action ContentChanged;

    [Header("Content UI Objects")]
    public Image locationIcon;
    public Image nameIcon;
    public TextMeshProUGUI description;
    public Text fishName;

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

    public void UpdateUI()
    {
        // Your logic to update the UI here
        Debug.Log("UI Updated");
        Debug.Log("Content: " + _content.fishName);
        Debug.Log("Content: " + _content.description);
        Debug.Log("Content: " + _content.icon);
        fishName.text = _content.fishName;
        description.text = _content.description;
        nameIcon.target = _content.icon;
    }

    private void Start()
    {
        ContentChanged += UpdateUI;
        ContentChanged();
    }
}
