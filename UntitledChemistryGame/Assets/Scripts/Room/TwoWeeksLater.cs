using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoWeeksLater : MonoBehaviour
{
    public GameObject panel;
    public Animator anim;

    private Image panelImage;
    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>(); 
        anim.SetBool("fade", true);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(2f);
        gm.LoadNextScene();
    }
}
