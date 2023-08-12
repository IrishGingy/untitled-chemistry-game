using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public GameObject questDetails;
    private QuestButton qb;

    private void Start()
    {
        //questDetails.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // change details to be the specific button's quest description
        //GameObject button = eventData.pointerEnter;
        qb = gameObject.GetComponent<QuestButton>();
        qb.questDescriptionObject.SetActive(true);
        qb.questTasksObject.SetActive(true);
        //questDetails.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // change details to be the specific button's quest description
        //questDetails.SetActive(false);
        qb.questDescriptionObject.SetActive(false);
        qb.questTasksObject.SetActive(false);
    }
}
