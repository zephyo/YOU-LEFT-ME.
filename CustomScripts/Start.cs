using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Start : MonoBehaviour
{

    public pauses pauses;

    public CanvasGroup ui, menu;
    void Awake()
    {
        Application.targetFrameRate = 40;
    }

    public void NewGame()
    {


        game();
    }
    void game()
    {


        LeanTween.value(gameObject, (float va)=>{
			ui.alpha=va;
			menu.alpha=va*1.3f;

		},1,0, 1.5f).setEaseInOutQuad().setOnComplete(() =>
        {



            SceneManager.LoadScene(1);
        });

    }

    public void Fadein(CanvasGroup c)
    {
        c.alpha = 0;
        c.gameObject.SetActive(true);
        LeanTween.alphaCanvas(c, 1f, 0.4f).setEaseOutQuint();
    }
    public void FadeOut(CanvasGroup c)
    {
        LeanTween.alphaCanvas(c, 0f, 0.4f).setEaseOutQuint().setOnComplete(() =>
        {
            c.gameObject.SetActive(false);
        });
    }


    public void Quit()
    {
        Application.Quit();
    }



}
