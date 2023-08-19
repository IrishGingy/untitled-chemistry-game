using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFlip : MonoBehaviour
{
    public GameObject[] pages;
    public int curPages;

    // Start is called before the first frame update
    void Start()
    {
        curPages = 0;
        for (int i = 1; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (curPages != 0)
            {
                PageLeft();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (curPages < pages.Length - 1)
            {
                PageRight();
            }
        }
    }

    void PageLeft()
    {
        pages[curPages - 1].SetActive(true);
        pages[curPages].SetActive(false);
        curPages--;
    }

    void PageRight()
    {
        pages[curPages + 1].SetActive(true);
        pages[curPages].SetActive(false);
        curPages++;
    }
}
