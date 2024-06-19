using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainSys : MonoBehaviour
{
    int lvl;
    public GameObject notenoughmoney;
    public GameObject levels, slotgame, tutor;
    public GameObject[] tutorialpages;
    AudioSys aus;
    int tutorpage;
    [Header("Levels")]
    public GameObject leveltoopen, openlevelwindow;
    public Text openlevelwindowcointext, openlevelwindowlvltext, levelsmoneytext;
    public int pricetoopenlevel;
    [Header("Slots Game")]
    bool isplaying;
    public int energyint;
    public Text slotgameenergyteext, slotgamemoneytext;
    public GameObject crystalgameflare, bonuscoeffbutton, bonustriplebutton, goscreen;
    public Button crystalgamebutton;
    public ParticleSystem coinparts;
    public Image slot1, slot2, slot3, crystalgamecazan;
    public Sprite[] slotitems;
    public GameObject[] bonusbuttons;
    bool isspin1, isspin2, bonusleveled;
    public bool slottripled, isspining;
    float timer1, timer2, timer3;
    int curslot1, curslot2, curslot3, s1, s2, s3;
    public int bonuscoeff = 1;
    [Header("Crystal Game")]
    public bool crystalgameplaying, shielded;
    public int needtoactivate, amountactivated;
    public GameObject cgwinscreen;
    public Text cgwincoinstext, cgwinstarstext, cgmoneytext;
    public GameObject[] crystalgames;



    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("restarted") == 1)
        {
            PlayerPrefs.SetInt("restarted", 0);
            levels.SetActive(true);
        }
        aus = GameObject.Find("AudioSys").GetComponent<AudioSys>();
        UpdateMoneyText();
    }

    public void RestartGo()
    {
        PlayerPrefs.SetInt("restarted", 1);
        Application.LoadLevel(1);

    }
    public void TutorArrow(int index)
    {
        tutorpage += index;


        if (tutorpage > 8)
        {
            tutorialpages[8].SetActive(false);
            tutorialpages[0].SetActive(true);
            tutorpage = 0;
            tutor.SetActive(false);
        }
        else if (tutorpage < 0)
        {
            tutor.SetActive(false);
        }
        else
        {

            if (index > 0)
            {
                if (tutorpage != 0)
                {
                    tutorialpages[tutorpage - 1].SetActive(false);
                }

            }
            else
            {
                tutorialpages[tutorpage + 1].SetActive(false);
            }
            tutorialpages[tutorpage].SetActive(true);
        }

    }


    public void BuyLevel()
    {
        int money = PlayerPrefs.GetInt("money");

        if (money >= pricetoopenlevel)
        {
            openlevelwindow.SetActive(false);
            PlayerPrefs.SetInt("level" + leveltoopen.GetComponent<LevelButton>().index.ToString(), 1);
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - pricetoopenlevel);
            leveltoopen.GetComponent<LevelButton>().IsOpenedLevel();
            UpdateMoneyText();

        }
        else
        {
            openlevelwindow.SetActive(false);
            notenoughmoney.SetActive(true);
        }
    }

    public void UpdateMoneyText()
    {
        string m = PlayerPrefs.GetInt("money").ToString();
        slotgamemoneytext.text = m;
        cgmoneytext.text = m;
        levelsmoneytext.text = m;
    }
    public void StartSlots(int lvlindex)
    {
        goscreen.SetActive(false);


        crystalgameflare.SetActive(false);
        crystalgamebutton.interactable = false;
        var tempColor = crystalgamecazan.color;
        tempColor.a = 0.7f;
        crystalgamecazan.color = tempColor;


        foreach (GameObject g in bonusbuttons)
        {
            g.SetActive(false);
        }
        isplaying = true;
        lvl = lvlindex;
        bonusleveled = false;
        bonusbuttons[lvl].SetActive(true);
        UpdateMoneyText();
        energyint = 20;

    }

    [SerializeField] private Button _slotMachineButton;
    public void SpinSlots()
    {
        if (!isspining)
        {
            aus.PlaySound(aus.spin);
            energyint--;
            slotgameenergyteext.text = energyint.ToString() + "/20";
            StartCoroutine(SpiningSlots());
            _slotMachineButton.interactable = false;
        }

    }


    IEnumerator SpiningSlots()
    {
        isspining = true;
        isspin1 = true;
        isspin2 = true;
        curslot1 = Random.Range(0, slotitems.Length);
        curslot2 = Random.Range(0, slotitems.Length);
        curslot3 = Random.Range(0, slotitems.Length);
        yield return new WaitForSeconds(1);
        s1 = Random.Range(0, slotitems.Length);
        if (!slottripled)
        {
            int lucky3 = Random.Range(0, 5);
            int lucky2 = Random.Range(0, 5);

            if (lucky2 != 0)
            {
                s2 = Random.Range(0, slotitems.Length);
            }
            else
            {
                s1 = s2;
            }

            if (lucky3 != 0)
            {
                s3 = Random.Range(0, slotitems.Length);
            }
            else
            {
                s2 = s3;
            }

        }
        else
        {
            s2 = s1;
            s3 = s1;
        }

        yield return new WaitForSeconds(0.5f);
        isspin1 = false;
        slot1.sprite = slotitems[s1];
        yield return new WaitForSeconds(0.5f);
        isspin2 = false;
        slot2.sprite = slotitems[s2];
        yield return new WaitForSeconds(0.5f);
        slot3.sprite = slotitems[s3];


        if (s1 != s2 && s2 != s3)
        {

        }
        else if (s1 == s2 && s2 == s3)
        {
            SlotWin(s1, 2);
            aus.PlaySound(aus.bonussound);
        }
        else if (s1 == s2 || s2 == s3)
        {

            SlotWin(s2, 1);
            aus.PlaySound(aus.winslots);
        }

        slottripled = false;
        isspining = false;

        if (bonustriplebutton != null)
        {
            bonustriplebutton.GetComponent<BonusButton>().flare.SetActive(false);
            bonustriplebutton.GetComponent<Button>().interactable = true;
            bonustriplebutton = null;
        }

        if (bonuscoeff > 1)
        {
            bonuscoeffbutton.GetComponent<BonusButton>().flare.SetActive(false);
            bonuscoeffbutton.GetComponent<Button>().interactable = true;
            bonuscoeff = 1;
        }
        if (energyint < 10 && !bonusleveled && !crystalgameflare.activeSelf)
        {
            crystalgamebutton.interactable = true;
            var tempColor = crystalgamecazan.color;
            tempColor.a = 1f;
            crystalgamecazan.color = tempColor;
            crystalgameflare.SetActive(true);
        }

        _slotMachineButton.interactable = true;


        CheckGO();
    }

    public void SlotWin(int index, int combo)
    {
        int wsm = 3 * combo * (index + 1) * bonuscoeff;
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + wsm);

        if (wsm < 20)
        {
            aus.PlaySound(aus.coinssmall);
        }
        else if (wsm >= 20 && wsm < 40)
        {
            aus.PlaySound(aus.coinsmed);
        }
        else
        {
            aus.PlaySound(aus.coinslarge);
        }

        coinparts.Play();
        UpdateMoneyText();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isspining)
        {
            if (isspin1)
            {
                SpinSlot(slot1, curslot1, timer1, 0);
            }
            if (isspin2)
            {
                SpinSlot(slot2, curslot2, timer2, 1);
            }


            SpinSlot(slot3, curslot3, timer3, 2);

        }
    }

    private void Update()
    {
        if (crystalgameplaying && amountactivated == needtoactivate)
        {
            cgwinscreen.SetActive(true);
            int wcoins = Random.Range(10, 30);
            int wstars = Random.Range(3, 10);
            cgwincoinstext.text = wcoins.ToString();
            cgwinstarstext.text = wstars.ToString();
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + wcoins);
            UpdateMoneyText();
            energyint += wstars;
            if (energyint > 20) { energyint = 20; }
            slotgameenergyteext.text = energyint.ToString() + "/20";
            crystalgameplaying = false;
            aus.PlaySound(aus.wincrystal);
        }



    }


    void CheckGO()
    {
        if (!goscreen.activeSelf && energyint <= 0 && isplaying)
        {
            goscreen.SetActive(true);
            aus.PlaySound(aus.gameover);
            isplaying = false;
        }
    }



    void SpinSlot(Image img, int curslot, float timer, int index)
    {

        timer -= Time.deltaTime;


        if (timer < 0)
        {
            timer = 0.05f;
            curslot++;

            if (curslot < slotitems.Length)
            {
                img.sprite = slotitems[curslot];
            }
            else
            {
                img.sprite = slotitems[0];
                curslot = 0;

            }

        }

        if (index == 0)
        {
            timer1 = timer;
            curslot1 = curslot;
        }
        if (index == 1)
        {
            timer2 = timer;
            curslot2 = curslot;
        }
        if (index == 2)
        {
            timer3 = timer;
            curslot3 = curslot;
        }


    }



    public void StartCrystalGame()
    {
        bonusleveled = true;
        UpdateMoneyText();
        crystalgameplaying = true;
        amountactivated = 0;
        crystalgames[lvl].SetActive(true);

        if (lvl == 0)
        {
            needtoactivate = 2;
        }
        if (lvl == 1)
        {
            needtoactivate = 3;
        }
        if (lvl == 2)
        {
            needtoactivate = 4;
        }
        if (lvl == 3)
        {
            needtoactivate = 4;
        }
        if (lvl == 4)
        {
            needtoactivate = 5;
        }
        if (lvl == 5)
        {
            needtoactivate = 4;
        }
        if (lvl == 6)
        {
            needtoactivate = 6;
        }
        if (lvl == 7)
        {
            needtoactivate = 6;
        }
        if (lvl == 8)
        {
            needtoactivate = 7;
        }
        if (lvl == 9)
        {
            needtoactivate = 8;
        }


    }
}
