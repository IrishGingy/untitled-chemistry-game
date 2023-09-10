using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public GameObject panel;
    public Animator anim;

    private Image panelImage;
    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("fade", true);
            StartCoroutine(Fade());
        }
        //// play a fade to black animation, then load the next scene
        //anim.Play("Base Layer.FadeToBlack", 0, 0);
        ////panelImage.color = new Color(0, 0, 0, 255);
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(2f);
        gm.LoadNextScene();
    }
}
