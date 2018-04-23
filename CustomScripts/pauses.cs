using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class pauses : MonoBehaviour
{

    public CanvasGroup SettingCanvas, pause;

    [Header("Audio stuff")]
    public AudioClip[] audios;

	public AudioMixerSnapshot up, down;
    bool setting, paused;
    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
			GameObject[] g = GameObject.FindGameObjectsWithTag("Finish");
			foreach (GameObject gg in g){
				if (gg!=gameObject){
					GameObject.FindGameObjectWithTag("GameController").transform.GetChild(0).GetChild(0).
					GetChild(1).GetComponent<Button>().onClick.AddListener(()=>{
						pauses p = gg.GetComponent<pauses>();
						p.Fadein(p.SettingCanvas);
					});
				}
			}
            Destroy(gameObject);
        }else{
				GameObject.FindGameObjectWithTag("GameController").transform.GetChild(0).GetChild(0).
					GetChild(1).GetComponent<Button>().onClick.AddListener(()=>{
						
						Fadein(SettingCanvas);
					});
		}
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneAudioChange;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                paused = true;
                Fadein(pause);

            }
            else
            {
                paused = false;
                FadeOut(pause);

            }


        }
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
	public IEnumerator waitthenUp(float sec, AudioSource ass, int clip){
		yield return new WaitForSeconds(sec);
	 ass.clip = audios[clip];
            ass.Play();
		up.TransitionTo(0.7f);
	}

    void SceneAudioChange(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
			down.TransitionTo(2f);
            AudioSource ass = GetComponent<AudioSource>();
          
			StartCoroutine(waitthenUp(2f, ass,0));

        }

        if (scene.name == "Start")
        {
			down.TransitionTo(2f);
            AudioSource ass = GetComponent<AudioSource>();
           
			StartCoroutine(waitthenUp(2f, ass,1));

        }
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }


}
