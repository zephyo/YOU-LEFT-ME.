using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fungus;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using Kino;
using UnityEngine.EventSystems;

public enum scenes
{
    bed,

    cat,
    fridge,
    metroB,
    metroA,
    work,

    workTalk,
    workTalkManager,
    cliff,

    doctors,

    docTalk,
    grave,
    goodending,
    badending
}

public enum endings
{
    suicide,
    good,
    ghost
}

public class manager : MonoBehaviour
{

    new private Transform camera;
    public float smoothTime = 0.05f;
    float fac;
    private Vector3 velocity = Vector3.zero, velocity2 = Vector3.zero;


    [Header("Aesthetic stuff")]
    public GlitchEffect glitch;
    public Fluid2DBlur fluid;

    public Sprite[] bed, metro, work, cliff, doctors, grave, endings;

    private Dictionary<string, EventTrigger> selectors;

    private Queue<EventTrigger> unusedSelectors;

    public GameObject clock;

    [HideInInspector]
    public static manager instance;

    public Material basicbitchfont;


    // [Header("Dialogue Stuff")]
    // public SayDialog say;
    // public MenuDialog menu;

    [Header("data stuff")]
    public TextMeshProUGUI time;


    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        selectors = new Dictionary<string, EventTrigger>();
        unusedSelectors = new Queue<EventTrigger>();
    }



    void Start()
    {
        Application.targetFrameRate = 40;
        camera = Camera.main.transform;

        if (PlayerPrefs.GetInt("new") != 1)
        {
            newUser();
        }
        else
        {
            wakeup();
            setTime();
            bedroom();

        }
    }
    void removeBoxes()
    {
        Transform c = transform.GetChild(0);

        List<Transform> boxes = new List<Transform>();

        foreach (Transform cc in c)
        {
            if (cc.name == "box")
            {
                boxes.Add(cc);
            }
        }
        foreach (var item in boxes)
        {
            Destroy(item.gameObject);
        }
    }
    public IEnumerator switchscenes(scenes before, scenes after)
    {
        fluid._accum = 1;
        fluid._atten = 0;
        fluid.enabled = true;
        setTime();
        /**
        from accum 1 to 0.05
        atten, from 0 to 0.525 
        affter accum is 0.05, atten becomes 0
        accum slowly becomes 1
        enabled=false
        
         */

        LeanTween.value(gameObject, (float val) =>
        {
            fluid._accum = 1 - val * 0.95f;
            fluid._atten = val * 0.525f;
        }, 0, 1, 0.4f).setEaseOutCirc();
        yield return new WaitForSeconds(0.3f);
        Black();
        switch (before)
        {
            case scenes.bed:
                reuseAllSelectors();
                Destroy(transform.GetChild(0).GetChild(0).gameObject);
                yield return null;
                break;
            case scenes.metroB:
                transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, 50);
                transform.GetChild(0).GetChild(3).localPosition = new Vector3(0, 0, -30);
                transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, 70);
                dustparticles(Color.white).transform.localPosition = new Vector3(889f, 320, 0);
                break;
            case scenes.metroA:

                ParticleSystem ps = setParticleColor(Color.white);
                ps.transform.localPosition = new Vector3(889f, 320, 0);
                var main = ps.main;
                main.maxParticles = 15;
                ParticleSystem.EmissionModule em = ps.emission;
                em.rateOverTime = 2;
                ParticleSystem.ShapeModule sm = ps.shape;
                sm.scale = new Vector3(1, 4.137f, 1);


                transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, 70);
                transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, 50);
                break;
            case scenes.fridge:
                transform.GetChild(0).GetChild(3).localPosition = new Vector3(0, 0, -30);
                break;
            case scenes.work:
                transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, 50);
                removeBoxes();
                break;
            case scenes.workTalk:
                transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, 50);
                transform.GetChild(0).GetChild(3).localPosition = new Vector3(0, 0, -30);
                break;
            case scenes.workTalkManager:
                transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, 50);
                transform.GetChild(0).GetChild(3).localPosition = new Vector3(0, 0, -30);
                break;
            case scenes.cliff:
                ParticleSystem pss = setParticleColor(Color.white);
                pss.transform.localPosition = new Vector3(889f, 320, 0);
                var mains = pss.main;
                mains.maxParticles = 15;
                transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, 70);
                transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, 50);
                break;
            case scenes.doctors:
                reuseAllSelectors();
                break;

            case scenes.docTalk:
                BlackBegone();
                break;
            case scenes.grave:

                transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, 50);
                transform.GetChild(0).GetChild(2).localPosition = new Vector3(0, 0, 20);
                break;
            case scenes.goodending:
                break;
            case scenes.badending:
                break;

            default:
                break;
        }


        yield return new WaitForSeconds(0.1f);
        BlackBegone();
        wakeup();
        switch (after)
        {
            case scenes.bed:
                bedroom();
                break;
            case scenes.cat:
                cat();
                break;
            case scenes.fridge:
                fridge();
                break;
            case scenes.metroB:
                metroBEFORE();
                if (showFlowers()) flowerparticles(new Color(0.64f, 0.52f, 1, 1)).transform.localPosition = new Vector3(531.8f, 564, 793);

                break;
            case scenes.metroA:
                ParticleSystem ps = setParticleColor(Color.red);
                ps.transform.localPosition = new Vector3(510, 305.03f, 0);
                var main = ps.main;
                main.maxParticles = 40;
                ParticleSystem.EmissionModule em = ps.emission;
                em.rate = 5;
                ParticleSystem.ShapeModule sm = ps.shape;
                sm.scale = new Vector3(4.8f, 4.137f, 1);
                metroAFTER();
                break;
            case scenes.work:
                workFactory();
                break;
            case scenes.workTalk:
                workTalkBoxface();
                break;
            case scenes.workTalkManager:
                workTalkManager();
                break;
            case scenes.cliff:
                ParticleSystem pss = setParticleColor(new Color(1, 0.427f, 0.43f));
                pss.transform.localPosition = new Vector3(660, 432f, 0);
                var mains = pss.main;
                mains.maxParticles = 40;
                cliffInit();
                break;
            case scenes.doctors:
                doctorsWaitingArea();
                break;
            case scenes.grave:
                graveyard();
                break;

                  case scenes.docTalk:
                Black();
                break;

            case scenes.goodending:

                aliveEnding();
                break;
            case scenes.badending:
                ghostEnding();
                break;

            default:
                break;
        }
    }


    void wakeup()
    {
        fluid.enabled = true;
        LeanTween.value(gameObject, (float val) =>
      {

          fluid._atten = val * 0.525f;
      }, 1, 0, 0.5f).setEaseInCubic().setOnComplete(() =>
      {
          LeanTween.value(gameObject, (float val) =>
          {

              fluid._accum = val;
          }, 0.05f, 1, 1.3f).setEaseOutCubic().setOnComplete(() =>
             {
                 fluid.enabled = false;
             });
      });
    }


    void newUser()
    {
        Camera.main.nearClipPlane = 85.9f;
        PlayerPrefs.SetInt("new", 1);
        DateTime morning = DateTime.Now;
        morning = morning.AddHours(7 - morning.Hour);
        Debug.Log(morning.Hour);
        PlayerPrefs.SetString("time", morning.ToString("M/d/yyyy h:mm tt"));
        time.gameObject.SetActive(false);
        setTime();
        Tutorial1();

    }

    void setUpSelector(Vector2 pos, Vector2 size, string name, Sprite key = null)
    {
        Image holymoly;
        RectTransform rt;
        EventTrigger et;
        if (unusedSelectors.Count > 0)
        {

            et = unusedSelectors.Dequeue();
            et.gameObject.SetActive(true);
            Debug.Log("found " + et.name + " to use as selector!");
            rt = (RectTransform)et.transform;
            holymoly = et.GetComponent<Image>();
        }
        else
        {
            GameObject SP = new GameObject(name);
            holymoly = SP.AddComponent<Image>();
            holymoly.raycastTarget = true;
            rt = (RectTransform)SP.transform;
            rt.SetParent(transform.GetChild(0), false);
            et = SP.AddComponent<EventTrigger>();
        }
        holymoly.color = Color.clear;
        rt.sizeDelta = size;
        rt.anchoredPosition = pos;
        sendmessage sm = GetComponent<sendmessage>();


        EventTrigger.Entry entry = checkForEntry(et, EventTriggerType.PointerEnter);
        entry.callback.AddListener((eventData) => { sm.Enter(); });

        EventTrigger.Entry entry2 = checkForEntry(et, EventTriggerType.PointerExit);
        entry2.callback.AddListener((eventData) => { sm.Exit(); });

        if (key != null)
        {
            holymoly.sprite = key;

            EventTrigger.Entry entry3 = checkForEntry(et, EventTriggerType.PointerClick);
            entry3.callback.AddListener((eventData) => { sm.Click(holymoly); });

        }
        selectors.Add(name, et);


    }

    EventTrigger.Entry checkForEntry(EventTrigger et, EventTriggerType ett)
    {
        EventTrigger.Entry clickEventHandler = et.triggers.FirstOrDefault(
             t => t.eventID == ett
         );
        if (clickEventHandler == null)
        {
            clickEventHandler = new EventTrigger.Entry();
            clickEventHandler.eventID = ett;
            et.triggers.Add(clickEventHandler);
        }
        return clickEventHandler;
    }

    void changeClickSelector(EventTrigger triggerTarget, Action a)
    {

        EventTrigger.Entry clickEventHandler = checkForEntry(triggerTarget, EventTriggerType.PointerClick);
        clickEventHandler.callback.RemoveAllListeners();
        clickEventHandler.callback.AddListener((eventData) => { a(); });



    }

    void removeSelector(string name)
    {
        Destroy(selectors[name]);
    }

    void reuseAllSelectors()
    {
        List<string> remove = new List<string>();
        foreach (KeyValuePair<string, EventTrigger> entry in selectors)
        {
            unusedSelectors.Enqueue(entry.Value);

            entry.Value.gameObject.SetActive(false);
            remove.Add(entry.Key);
        }
        foreach (string s in remove)
        {
            selectors.Remove(s);
        }
    }


    void Tutorial1()
    {

        GameObject SP = Instantiate(time.gameObject, GameObject.FindGameObjectWithTag("Player").transform, false);


        RectTransform rt = (RectTransform)SP.transform;

        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMax = new Vector2(0, -400);
        rt.offsetMin = new Vector2(0, 400);
        TextMeshProUGUI TXT = SP.GetComponent<TextMeshProUGUI>();

        TXT.text = "<b>" + PlayerPrefs.GetString("time") + "</b>: <color=#9affd9>wake up.";
        TXT.raycastTarget = true;
        TXT.fontSizeMax = 65;
        TXT.color = Color.white;

        SP.SetActive(true);
        AnalogGlitch an = Camera.main.gameObject.AddComponent<AnalogGlitch>();
        an.scanLineJitter = 0.01f;
        an.verticalJump = 0.01f;
        Button b = SP.AddComponent<Button>();
        ColorBlock cb = b.colors;
        cb.highlightedColor = Color.blue;
        cb.pressedColor = Color.black;
        b.colors = cb;

        b.onClick.AddListener(() => TutListener(b, an, TXT, 0));
    }

    void MoonListener(Button b, TextMeshProUGUI txt, CanvasGroup cg, int i)
    {
        EventSystem.current.SetSelectedGameObject(null);
        b.onClick.RemoveAllListeners();

        if (i == 2)
        {
            float time4 = 1.2f;
            LeanTween.value(txt.gameObject, (float v) =>
    {
        cg.alpha = v;
    }, 1f, 0f, time4).setEaseInExpo().setOnComplete(() =>
     {
         string listen = "moony";
         setUpSelector(new Vector3(-30.734f, 376.9445f, 70), new Vector2(548.8f, 339.5f), listen);

         changeClickSelector(selectors["moony"], () =>
         {
             GameObject g = Instantiate(clock, transform.GetChild(0), false);
             g.transform.SetSiblingIndex(0);

             ListenForMoonClick(b, txt, 0);
             LeanTween.value(txt.gameObject, (float v) =>
  {
      cg.alpha = v;
  }, 0f, 1, 1).setEaseInOutExpo().setDelay(1.5f);
         });
     });

            return;
        }



        string[] hooks = { "I wake up in the same bed - but a different world.", "Where am I? Why am I here?", };
        txt.text = hooks[i];
        b.onClick.AddListener(() =>
        {
            MoonListener(b, txt, cg, i + 1);

        });



    }

    void ListenForMoonClick(Button b, TextMeshProUGUI txt, int i)
    {

        if (i == 3)
        {
            StartCoroutine(Strobe(1, 1, () =>
            {
                //replace moon listener with - talk to moon
                Transform c = transform.GetChild(0);
                foreach (Transform child in c)
                {
                    child.gameObject.SetActive(true);
                }

                sendmessage sm = GetComponent<sendmessage>();
                changeClickSelector(selectors["moony"], () => { sm.ClickString("talkmoon"); });

                string listen = "cat";
                setUpSelector(new Vector3(-38.3f, -192.9915f, 70), new Vector2(216.2f, 125.5f), listen);

                changeClickSelector(selectors["cat"], () =>
                {
                    sm.ClickString("cat");
                });

                listen = "girl";
                setUpSelector(new Vector3(-261.96f, 0, 70), new Vector2(196.4f, 470.3f), listen);

                changeClickSelector(selectors["girl"], () =>
                {
                    sm.ClickString("girl");
                });
            }));
            Destroy(txt.gameObject);
            return;
        }
        EventSystem.current.SetSelectedGameObject(null);
        b.onClick.RemoveAllListeners();

        time.gameObject.SetActive(true);
        string[] hooks = { "On the moon clicks a 12 hour timer.", "What happens when night falls, and time runs up?",
         "How do I get out of here before then?"};
        txt.text = hooks[i];
        b.onClick.AddListener(() => ListenForMoonClick(b, txt, i + 1));

    }

    void TutListener(Button b, AnalogGlitch an, TextMeshProUGUI txt, int i)
    {



        if (i == 3)
        {
            float time4 = 2f;
            CanvasGroup cg = txt.rectTransform.parent.GetComponent<CanvasGroup>();
            LeanTween.value(txt.gameObject, (float v) =>
     {
         cg.alpha = v;
     }, 1f, 0f, time4).setEaseOutCubic().setOnComplete(() =>
      {

          Transform c = transform.GetChild(0);
          foreach (Transform child in c)
          {
              if (child.name != "mid2" && child.name != "clock")
                  child.gameObject.SetActive(false);
          }
          wakeup();
          Camera.main.nearClipPlane = 0.3f;

          MoonListener(b, txt, cg, 0);
          Destroy(an);

          cg.alpha = 1;


      });

            return;
        }
        EventSystem.current.SetSelectedGameObject(null);

        b.onClick.RemoveAllListeners();




        string[] hooks = { ": <color=#9affd9>work.", ": <color=#9affd9>sleep.", ": <color=#ff5e5e>wake up." };

        float time3 = 0.4f;
        DateTime dt = getTime();
        if (i == 2)
        {


            dt = dt.AddDays(1);
            dt = dt.AddHours(7 - dt.Hour);
            PlayerPrefs.SetString("time", dt.ToString("M/d/yyyy h:mm tt"));
            time3 *= 2f;
        }
        else if (i == 0)
        {
            dt = dt.AddHours(9 - dt.Hour);
            PlayerPrefs.SetString("time", dt.ToString("M/d/yyyy h:mm tt"));
        }
        else if (i == 1)
        {
            dt = dt.AddHours(22 - dt.Hour);
            PlayerPrefs.SetString("time", dt.ToString("M/d/yyyy h:mm tt"));
        }

        LeanTween.value(txt.gameObject, (float v) =>
        {
            txt.rectTransform.eulerAngles = new Vector3(Mathf.Lerp(0, 90, v), 0, 0);
        }, 0, 1f, time3).setEaseOutCubic().setOnComplete(() =>
         {
             txt.rectTransform.eulerAngles = Vector3.zero;
             an.scanLineJitter *= 1.5f;
             an.verticalJump *= 1.5f;
             txt.text = "<b>" + PlayerPrefs.GetString("time") + "</b>" + hooks[i];
             b.onClick.AddListener(() => TutListener(b, an, txt, i + 1));
             if (i == 2)
             {
                 ColorBlock cb = b.colors;
                 cb.highlightedColor = Color.red;
                 b.colors = cb;
             }
         });


    }


    public DateTime getTime()
    {
        return DateTime.ParseExact(PlayerPrefs.GetString("time"), "M/d/yyyy h:mm tt", CultureInfo.InvariantCulture);
    }
    public void setTime()
    {
        DateTime dt = DateTime.ParseExact(PlayerPrefs.GetString("time"), "M/d/yyyy h:mm tt", CultureInfo.InvariantCulture);
        time.text = dt.ToString("h:mm tt");
        TimeSpan span = DateTime.Now.Subtract(dt);
        if (span.TotalMinutes > 0)
            PlayerPrefs.SetString("time", dt.AddMinutes(span.Minutes).ToString("M/d/yyyy h:mm tt"));

    }



    public void Black()
    {
        Camera.main.cullingMask = 1 << 8;
    }
    public void BlackBegone()
    {
        Camera.main.cullingMask = -1;
    }
    public IEnumerator Strobe(int times, float yieldTime, Action onComplete = null)
    {
        WaitForSeconds between = new WaitForSeconds(yieldTime);
        bool black = true;
        Camera cam = Camera.main;

        /*
          GUI.color = new Color(1,1,1, fadeAlpha);    
                GUI.depth = -1000;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), screenFadeTexture); */
        for (int i = 0; i < times; i++)
        {


            cam.cullingMask = (black) ? 0 : -1;
            black = !black;

            yield return between;
        }
        cam.cullingMask = -1;
        if (onComplete != null) onComplete();
    }



    public void bedroom()
    {
        // if (selectors.ContainsKey("girl")){
        //     if (!selectors["girl"].gameObject.activeSelf){
        //         Image i = transform.GetChild(0).GetChild(3).GetComponent<Image>();
        //         i.sprite=bed[4];
        //         i.rectTransform.anchoredPosition=new Vector2(-415.5425f,336.6863f);
        //         i.SetNativeSize();
        //         foreach (Transform t in transform.GetChild(0)){
        //             t.gameObject.SetActive(true);
        //         }
        //     }
        //     return;
        // } 
        reuseAllSelectors();
        List<Image> stuff = new List<Image>();

        Image bg = transform.GetChild(0).GetComponent<Image>();
        //  if (bg.sprite == bed[0]) return;
        stuff.Add(bg);
        stuff.Add(bg.rectTransform.GetChild(0).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(1).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(2).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(3).GetComponent<Image>());


        //handle transforms
        Vector2[] positions = new Vector2[]{
            Vector2.zero,
            new Vector2(-184,-82.05063f),
            new Vector2(557.6664f,-252.4147f),
            new Vector2(-46.96589f,-145),
            new Vector2(-415.5425f,336.6863f),

        };

        GameObject g = Instantiate(clock, bg.rectTransform, false);
        g.transform.SetSiblingIndex(0);



        for (int i = 0; i < stuff.Count; i++)
        {

            stuff[i].sprite = bed[i];
            stuff[i].SetNativeSize();
            stuff[i].gameObject.SetActive(true);
            stuff[i].rectTransform.anchoredPosition = positions[i];


        }


        sendmessage sm = GetComponent<sendmessage>();


        string listen = "moony";
        setUpSelector(new Vector3(-30.734f, 376.9445f, 70), new Vector2(548.8f, 339.5f), listen);

        changeClickSelector(selectors["moony"], () =>
        {
            sm.ClickString("talkmoon");
        });

        listen = "cat";
        setUpSelector(new Vector3(-38.3f, -192.9915f, 70), new Vector2(216.2f, 125.5f), listen);

        changeClickSelector(selectors["cat"], () =>
        {
            sm.ClickString("cat");
        });

        listen = "girl";
        setUpSelector(new Vector3(-261.96f, 0, 70), new Vector2(196.4f, 470.3f), listen);

        changeClickSelector(selectors["girl"], () =>
        {
            sm.ClickString("girl");
        });

    }

    public void cat()
    {
        Image cat = null;
        Transform one = transform.GetChild(0);
        foreach (Transform c in one)
        {
            if (c.name == "fore")
            {
                cat = c.GetComponent<Image>(); continue;
            }
            c.gameObject.SetActive(false);

        }
        cat.sprite = bed[bed.Length - 1];
        cat.SetNativeSize();
        cat.rectTransform.anchoredPosition = new Vector2(0, -20.98f);

    }

    public void fridge()
    {
        Image fridge = null;
        Transform one = transform.GetChild(0);
        foreach (Transform c in one)
        {
            if (c.name == "fore")
            {
                fridge = c.GetComponent<Image>(); continue;
            }
            c.gameObject.SetActive(false);

        }
        fridge.sprite = bed[bed.Length - 2];
        fridge.SetNativeSize();
        fridge.rectTransform.localPosition = new Vector3(-1.2f, 1.7f, 6.9f);

    }



    public void metroBEFORE()
    {
        List<Image> stuff = new List<Image>();

        Image bg = transform.GetChild(0).GetComponent<Image>();
        //  if (bg.sprite == metro[0]) return;
        stuff.Add(bg);
        stuff.Add(bg.rectTransform.GetChild(0).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(1).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(3).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(2).GetComponent<Image>());



        for (int i = 0; i < stuff.Count; i++)
        {
            stuff[i].gameObject.SetActive(true);
            if (stuff[i].name == "mid2")
            {
                stuff[i].sprite = metro[7];
                stuff[i].rectTransform.anchoredPosition = new Vector2(-386.2f, -173.5f);
            }
            else
                stuff[i].sprite = metro[i];
            stuff[i].SetNativeSize();
            if (stuff[i].name == "mid")
                stuff[i].rectTransform.localPosition = new Vector3(341.3f, 19.3f, -17.7f);
            else if (stuff[i].name == "fore")
                stuff[i].rectTransform.localPosition = new Vector3(-128.59f, 17f, 6.6f);



        }
        stuff[1].transform.localPosition = new Vector3(0, 0, -7);

    }

    public bool showFlowers()
    {
        bool f = PlayerPrefs.GetInt("flowers") == 0;
        if (f)
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        return !f;
    }


    public void ForceshowFlowers()
    {

        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        flowerparticles(new Color(0.64f, 0.52f, 1, 1)).transform.localPosition = new Vector3(531.8f, 564, 793);

    }



    public void metroAFTER()
    {
        List<Image> stuff = new List<Image>();

        Image bg = transform.GetChild(0).GetComponent<Image>();
        //  if (bg.sprite == metro[4]) return;
        bg.sprite = metro[4];
        Image big = bg.rectTransform.GetChild(0).GetComponent<Image>();
        big.sprite = metro[1];
        big.transform.localPosition = new Vector3(0, 0, -7);
        big.gameObject.SetActive(true);
        stuff.Add(bg.rectTransform.GetChild(1).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(3).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(2).GetComponent<Image>());


        //handle transforms
        Vector2[] positions = new Vector2[]{

            new Vector2(195f,22),
              new Vector2(-303f,364),

        };

        for (int i = 0; i < stuff.Count; i++)
        {
            if (stuff[i].name == "mid2")
            {
                stuff[i].gameObject.SetActive(false);
                continue;
            }
            stuff[i].gameObject.SetActive(true);

            stuff[i].sprite = metro[i + 5];
            stuff[i].SetNativeSize();
            stuff[i].rectTransform.anchoredPosition = positions[i];

        }

        stuff[0].transform.localPosition = new Vector3(stuff[0].transform.localPosition.x, stuff[0].transform.localPosition.y, -8);


    }

    public void cliffInit()
    {

        List<Image> stuff = new List<Image>();

        Image bg = transform.GetChild(0).GetComponent<Image>();
        //    if (bg.sprite == cliff[0]) return;
        stuff.Add(bg);
        stuff.Add(bg.rectTransform.GetChild(0).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(1).GetComponent<Image>());
        bg.rectTransform.GetChild(2).gameObject.SetActive(false);
        stuff.Add(bg.rectTransform.GetChild(3).GetComponent<Image>());

        for (int i = 0; i < cliff.Length - 1; i++)
        {
            stuff[i].sprite = cliff[i];
            stuff[i].SetNativeSize();
            stuff[i].gameObject.SetActive(true);
            if (stuff[i].name == "bg2")
            {
                stuff[i].transform.localPosition = new Vector3(0, -34.8f, 0.49f);
                continue;
            }
            if (stuff[i].name == "mid")
            {
                stuff[i].transform.localPosition = new Vector3(-599.3f, 177.2f, -3f);
                continue;
            }
            if (stuff[i].name == "fore")
                stuff[i].rectTransform.anchoredPosition = new Vector2(168.3f, 156.49f);
        }

    }

    public void ghost()
    {
        //slowly blacks/fluid blurs out
        //staffatan glitch
        fluid._accum = 1;
        fluid._atten = 0;

        fluid.enabled = true;
        GameObject cache = GameObject.FindGameObjectWithTag("Finish");
        if (cache != null)
        {
            pauses p = cache.GetComponent<pauses>();
            p.down.TransitionTo(2f);
            AudioSource ass = p.GetComponent<AudioSource>();


            StartCoroutine(p.waitthenUp(2f, ass, 2));

        }
        LeanTween.value(gameObject, (float val) =>
      {

          fluid._atten = val * 0.525f;
      }, 0, 1, 2f).setEaseInSine().setOnComplete(() =>
      {
          LeanTween.value(gameObject, (float val) =>
          {

              fluid._accum = val;
          }, 1, 0f, 2.5f).setEaseOutCubic().setOnComplete(() =>
             {
                 ghostt2();
                 Camera.main.backgroundColor = new Color(0.309f, 0.66f, 0.8f);
                 StartCoroutine(Strobe(1, 1));
                 fluid.enabled = false;


             });
      });

    }

    public void ghostt2()
    {

        GameObject SP = Instantiate(time.gameObject, GameObject.FindGameObjectWithTag("Player").transform, false);
        SP.SetActive(false);

        RectTransform rt = (RectTransform)SP.transform;


        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMax = new Vector2(0, -400);
        rt.offsetMin = new Vector2(0, 400);
        TextMeshProUGUI TXT = SP.GetComponent<TextMeshProUGUI>();
        TXT.color = new Color(0.36f, 0.76f, 1f);

        TXT.text = "<b>" + PlayerPrefs.GetString("time") + "</b>: <color=white>join.";
        TXT.raycastTarget = true;
        TXT.fontSizeMax = 90.5f;

        SP.SetActive(true);

        Button b = SP.AddComponent<Button>();
        ColorBlock cb = b.colors;
        cb.highlightedColor = Color.blue;
        cb.pressedColor = Color.black;
        b.colors = cb;

        b.onClick.AddListener(() =>
        {
            fluid.enabled = true;

            LeanTween.value(gameObject, (float val) =>
    {

        fluid._accum = 1 - val;
        fluid._atten = 0.05f * val;
    }, 0f, 1, 4f).setEaseInOutCubic().setOnComplete(() =>
    {
        SceneManager.LoadScene(0);
    });

        });
    }



    public void good()
    {
        //slowly blacks/fluid blurs out
        //staffatan glitch
        fluid._accum = 0.01f;
        fluid._atten = 0;

        fluid.enabled = true;
        GameObject cache = GameObject.FindGameObjectWithTag("Finish");
        if (cache != null)
        {
            pauses p = cache.GetComponent<pauses>();
            p.down.TransitionTo(2f);
            AudioSource ass = p.GetComponent<AudioSource>();


            StartCoroutine(p.waitthenUp(2f, ass, 2));

        }

        Image x = transform.GetChild(0).GetComponent<Image>();
        x.color = Color.clear;


        LeanTween.value(gameObject, (float val) =>
      {

          fluid._accum = val * 0.8f;

          x.color = new Color(1, 1, 1, val);
      }, 0, 1, 3f).setEaseInSine().setOnComplete(() =>

      {
          goodpt2();
          Camera.main.backgroundColor = new Color(0.91f, 0.913f, 0.82f);
          StartCoroutine(Strobe(1, 1));
          fluid.enabled = false;


      });


    }

    public void goodpt2()
    {

        GameObject SP = Instantiate(time.gameObject, GameObject.FindGameObjectWithTag("Player").transform, false);
        SP.SetActive(false);

        RectTransform rt = (RectTransform)SP.transform;


        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMax = new Vector2(0, -400);
        rt.offsetMin = new Vector2(0, 400);
        TextMeshProUGUI TXT = SP.GetComponent<TextMeshProUGUI>();
        TXT.color = new Color(0.69f, 0.65f, 0.62f);

        TXT.text = "<b>" + PlayerPrefs.GetString("time") + "</b>: live.";
        TXT.raycastTarget = true;
        TXT.fontSizeMax = 48.7f;

        TXT.fontMaterial = basicbitchfont;

        SP.SetActive(true);

        Button b = SP.AddComponent<Button>();
        ColorBlock cb = b.colors;
        cb.highlightedColor = Color.yellow;
        cb.pressedColor = Color.black;
        b.colors = cb;

        b.onClick.AddListener(() =>
        {
            fluid.enabled = true;

            LeanTween.value(gameObject, (float val) =>
    {

        fluid._accum = 1 - val;
        fluid._atten = 0.05f * val;
    }, 0f, 1, 4f).setEaseInOutCubic().setOnComplete(() =>
    {
        SceneManager.LoadScene(0);
    });

        });
    }



    public void suicideending()
    {
        //slowly blacks/fluid blurs out
        //staffatan glitch
        fluid._accum = 1;
        fluid._atten = 0;

        fluid.enabled = true;
        LeanTween.value(gameObject, (float val) =>
      {

          fluid._atten = val * 0.525f;
      }, 0, 1, 2f).setEaseInSine().setOnComplete(() =>
      {
          LeanTween.value(gameObject, (float val) =>
          {

              fluid._accum = val;
          }, 1, 0f, 2.5f).setEaseOutCubic().setOnComplete(() =>
             {
                 suicipt2();
                 StartCoroutine(Strobe(1, 1));
                 fluid.enabled = false;


             });
      });

    }

    void suicipt2()
    {
        GameObject SP = Instantiate(time.gameObject, GameObject.FindGameObjectWithTag("Player").transform, false);
        SP.SetActive(false);

        RectTransform rt = (RectTransform)SP.transform;

        Camera.main.nearClipPlane = 5.85f;

        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMax = new Vector2(0, -400);
        rt.offsetMin = new Vector2(0, 400);
        TextMeshProUGUI TXT = SP.GetComponent<TextMeshProUGUI>();

        TXT.text = "<b>" + PlayerPrefs.GetString("time") + "</b>: die.";
        TXT.raycastTarget = true;
        TXT.fontSizeMax = 65;

        SP.SetActive(true);
        AnalogGlitch an = Camera.main.gameObject.AddComponent<AnalogGlitch>();
        an.scanLineJitter = 0.01f;
        an.verticalJump = 0.01f;
        Button b = SP.AddComponent<Button>();
        ColorBlock cb = b.colors;
        cb.highlightedColor = Color.red;
        cb.pressedColor = Color.black;
        b.colors = cb;
        glitch.enabled = true;
        LeanTween.value(gameObject, (float val) =>
          {

              glitch.colorIntensity = val;
          }, 0f, 1, 15f).setDelay(2);

        b.onClick.AddListener(() =>
        {
            fluid.enabled = true;

            LeanTween.value(gameObject, (float val) =>
    {

        fluid._accum = 1 - val;
        fluid._atten = 0.7f * val;
    }, 0f, 1, 4f).setEaseInOutCubic().setOnComplete(() =>
    {
        SceneManager.LoadScene(0);
    });

        });
    }



    public void cliffSuicide()
    {
        StartCoroutine(Strobe(1, 0.7f));
        Image x = transform.GetChild(0).GetChild(3).GetComponent<Image>();
        x.rectTransform.pivot = new Vector2(0.756943f, 0.03712533f);

        x.sprite = cliff[4];
        x.SetNativeSize();


        LeanTween.value(gameObject, (float v) =>
        {
            x.rectTransform.eulerAngles = new Vector3(0, 0, -34.2f * v);
            x.rectTransform.anchoredPosition = new Vector2(187.8389f + 25.4611f * v, 65.43295f - 10 * v);

        }, 0, 1, 4f).setEaseInQuint().setOnComplete(() =>
        {

            LeanTween.value(gameObject, (float v) =>
            {
                x.rectTransform.eulerAngles = new Vector3(0, 0, -34.2f - 5 * v);
                x.rectTransform.anchoredPosition = new Vector2(x.rectTransform.anchoredPosition.x, 55.43295f - 40 * v);

            }, 0, 1, 3f);
        }).setEaseInCubic();

    }

    public void workFactory()
    {
        List<Image> stuff = new List<Image>();

        Image bg = transform.GetChild(0).GetComponent<Image>();
        //  if (bg.sprite == work[0]) return;
        stuff.Add(bg);
        bg.rectTransform.GetChild(0).gameObject.SetActive(false);
        stuff.Add(bg.rectTransform.GetChild(1).GetComponent<Image>());
        GameObject fB = bg.rectTransform.GetChild(2).gameObject;
        fB.SetActive(false);
        stuff.Add(bg.rectTransform.GetChild(3).GetComponent<Image>());

        for (int i = 0; i < 3; i++)
        {
            stuff[i].sprite = work[i];
            stuff[i].SetNativeSize();
            stuff[i].gameObject.SetActive(true);
            if (stuff[i].name == "mid")
            {
                stuff[i].rectTransform.localPosition = new Vector3(-35.5f, 10.7f, 0);
                continue;
            }
            else if (stuff[i].name == "fore")
                stuff[i].rectTransform.anchoredPosition = new Vector2(29.26f, -27f);

        }
        //do box anim
        StartCoroutine(generateboxes(fB));

    }

    IEnumerator generateboxes(GameObject fB)
    {
        Image im = Instantiate(fB, fB.transform.parent, false).GetComponent<Image>();
        im.name = "box";
        im.sprite = work[3];
        im.SetNativeSize();
        im.gameObject.AddComponent<box>();

        GameObject[] arr = new GameObject[5];
        arr[0] = im.gameObject;
        for (int i = 1; i < 5; i++)
        {
            arr[i] = GameObject.Instantiate(im.gameObject, im.transform.parent, false);
            arr[i].name = "box";
        }
        Vector3 begin = new Vector3(1387.9f, 776.3f, 300.8f);
        for (int i = 4; i > -1; i--)
        {
            arr[i].transform.localPosition = begin;
            arr[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(UnityEngine.Random.Range(2.1f, 3f));
        }
    }

    public void workTalkManager()
    {
        List<Image> stuff = new List<Image>();

        Image bg = transform.GetChild(0).GetComponent<Image>();
        //  if (bg.sprite == work[4]) return;
        stuff.Add(bg);
        bg.rectTransform.GetChild(0).gameObject.SetActive(false);
        stuff.Add(bg.rectTransform.GetChild(1).GetComponent<Image>());
        bg.rectTransform.GetChild(2).gameObject.SetActive(false);
        stuff.Add(bg.rectTransform.GetChild(3).GetComponent<Image>());

        for (int i = 0; i < 3; i++)
        {
            stuff[i].sprite = work[i + 4];

            stuff[i].gameObject.SetActive(true);
            if (stuff[i].name == "mid")
            {
                stuff[i].rectTransform.sizeDelta = new Vector2(805, 375);

                stuff[i].rectTransform.localPosition = new Vector3(14, 328.7f, -45.3f);
                continue;
            }
            else if (stuff[i].name == "fore")
            {
                stuff[i].SetNativeSize();
                stuff[i].rectTransform.localPosition = new Vector3(-16.41536f, -350.6f, -63.6f);

            }

        }
    }

    public void workTalkBoxface()
    {

        List<Image> stuff = new List<Image>();

        Image bg = transform.GetChild(0).GetComponent<Image>();
        stuff.Add(bg);
        bg.rectTransform.GetChild(0).gameObject.SetActive(false);
        stuff.Add(bg.rectTransform.GetChild(1).GetComponent<Image>());
        bg.rectTransform.GetChild(2).gameObject.SetActive(false);
        stuff.Add(bg.rectTransform.GetChild(3).GetComponent<Image>());

        for (int i = 0; i < 3; i++)
        {
            if (i == 1)
            {
                stuff[i].sprite = work[7];
            }
            else
            {
                stuff[i].sprite = work[i + 4];
            }

            stuff[i].SetNativeSize();
            stuff[i].gameObject.SetActive(true);
            if (stuff[i].name == "mid")
            {
                stuff[i].rectTransform.localPosition = new Vector3(-53.8f, 106.3f, 37.5f);
                continue;
            }
            else if (stuff[i].name == "fore")
            {

                stuff[i].rectTransform.localPosition = new Vector3(-16.41536f, -350.6f, -63.6f);

            }

        }

    }



    public void graveyard()
    {
        reuseAllSelectors();
        List<Image> stuff = new List<Image>();

        Image bg = transform.GetChild(0).GetComponent<Image>();
        //  if (bg.sprite == grave[0]) return;
        stuff.Add(bg);
        stuff.Add(bg.rectTransform.GetChild(0).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(1).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(2).GetComponent<Image>());
        stuff.Add(bg.rectTransform.GetChild(3).GetComponent<Image>());

        //handle transforms
        Vector2[] positions = new Vector2[]{
            Vector2.zero,
            new Vector2(0,41),
            new Vector2(0,-287.4839f),
            new Vector2(0,-86.984f),
            new Vector2(0,-183.22f),

        };
        for (int i = 0; i < grave.Length; i++)
        {

            stuff[i].sprite = grave[i];
            stuff[i].SetNativeSize();
            stuff[i].gameObject.SetActive(true);
            if (stuff[i].name == "mid")
            {
                stuff[i].transform.localPosition = new Vector3(0, -287.4839f, -7.6f);
            }
            if (stuff[i].name == "mid2")
            {
                stuff[i].transform.localPosition = new Vector3(0, -86.984f, 11.9f);
            }

            stuff[i].rectTransform.anchoredPosition = positions[i];
        }

        sendmessage sm = GetComponent<sendmessage>();

        string listen = "hergrave";
        setUpSelector(new Vector3(0, 167.8951f, 70), new Vector2(299.9f, 286.23f), listen);

        changeClickSelector(selectors[listen], () =>
        {
            sm.ClickString(listen);
        });
    }

    public void doctorsWaitingArea()
    {

        Image bg = transform.GetChild(0).GetComponent<Image>();
        bg.sprite = doctors[0];
        foreach (Transform c in bg.rectTransform)
        {
            c.gameObject.SetActive(false);
        }

        sendmessage sm = GetComponent<sendmessage>();


        string listen = "doc";
        setUpSelector(new Vector3(629.6051f, -49.22672f, 70), new Vector2(165.3f, 373.3f), listen);

        changeClickSelector(selectors[listen], () =>
        {
            sm.ClickString(listen);
        });

        listen = "line";
        setUpSelector(new Vector3(-714.1f, -157.1519f, 70), new Vector2(722.5f, 701.1f), listen);

        changeClickSelector(selectors[listen], () =>
               {
                   sm.ClickString(listen);
               });

        listen = "girlwaiting";
        setUpSelector(new Vector3(-57.2f, -212.7f, 70), new Vector2(287f, 593.4f), listen);
        changeClickSelector(selectors[listen], () =>
               {
                   sm.ClickString(listen);
               });

    }


    public void ghostEnding()
    {
        Image bg = transform.GetChild(0).GetComponent<Image>();
        //  if (bg.sprite == endings[0]) return;
        bg.sprite = endings[0];
        foreach (Transform c in bg.rectTransform)
        {
            if (c.name == "mid")
            {
                Image x = c.GetComponent<Image>();
                x.sprite = endings[1];
                Destroy(c.GetComponent<EventTrigger>());
                x.SetNativeSize();
                x.rectTransform.anchoredPosition = new Vector2(0, -40);
                c.gameObject.SetActive(true);
                continue;
            }
            c.gameObject.SetActive(false);
        }
    }

    public void aliveEnding()
    {
        Image bg = transform.GetChild(0).GetComponent<Image>();
        bg.sprite = endings[2];
        foreach (Transform c in bg.rectTransform)
        {
            c.gameObject.SetActive(false);
        }
    }


    ParticleSystem flowerparticles(Color c)
    {
        ParticleSystem ps = setParticleColor(c);
        ps.gameObject.SetActive(false);

        var m = ps.main;
        m.startSize = new ParticleSystem.MinMaxCurve(UnityEngine.Random.Range(0.2f, 0.4f),
                UnityEngine.Random.Range(0.7f, 1.4f));

        m.startSpeed = new ParticleSystem.MinMaxCurve(0.1f,
      1f);


        ParticleSystemRenderer psr = ps.GetComponent<ParticleSystemRenderer>();
        psr.material = Resources.Load<Material>("petal");
        ps.gameObject.SetActive(true);
        return ps;

    }


    ParticleSystem dustparticles(Color c)
    {
        ParticleSystem ps = setParticleColor(c);
        ps.gameObject.SetActive(false);


        var m = ps.main;
        m.startSize = new ParticleSystem.MinMaxCurve(0.09f,
                0.02f);
        m.startSpeed = new ParticleSystem.MinMaxCurve(0.005f,
0.03f);


        ParticleSystemRenderer psr = ps.GetComponent<ParticleSystemRenderer>();
        psr.material = Resources.Load<Material>("particle");
        ps.gameObject.SetActive(true);
        return ps;
    }


    ParticleSystem setParticleColor(Color c)
    {
        ParticleSystem ps = transform.GetChild(1).GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = new ParticleSystem.MinMaxGradient(c,
            new Color(c.r * 0.75f, c.g * 0.75f, c.b * 0.75f, 0.2f));
        return ps;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (InScreen())
        {
            Vector3 m = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0);


            camera.position = Vector3.SmoothDamp(camera.position, new Vector3(Mathf.Clamp(m.x, -0.32f, 0.32f), Mathf.Clamp(m.y, 0f, 0.3f), camera.position.z), ref velocity, smoothTime);

            camera.localRotation = Quaternion.Lerp(camera.rotation, Quaternion.Euler(Mathf.Clamp(-m.y / (Screen.height * 0.05f), -0.2f, 1.7f), Mathf.Clamp(m.x, -1f, 1f), 0), Time.deltaTime * 2);

        }
    }

    bool InScreen()
    {

        Vector3 x = Input.mousePosition;
        if (x.x > 0 && x.y > 0 && x.x < Screen.width && x.y < Screen.height)
        {
            return true;
        }
        return false;


    }




}
