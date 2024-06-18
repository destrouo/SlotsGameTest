using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoosterButton : MonoBehaviour
{
    public bool shield, activator;
    public int price = 5;
    MainSys main;
    [Header("Shield")]
    public Text timertext;
    public Image shieldimg;
    Button bttn;
    [Header("Activator")]
    public GameObject crystal;
    public CrystalsBases cb;
    // Start is called before the first frame update
    void Start()
    {
        if (shield) { bttn = gameObject.GetComponent<Button>();
            
        }

        if (activator)
        {
           
        }

      
        main = GameObject.Find("Main Camera").GetComponent<MainSys>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {

        int money = PlayerPrefs.GetInt("money");
        

       
        if (money >= price)
        {
            
            if (shield)
            {

                StartCoroutine(ShieldTimer());
            }

            if (activator)
            {
                if (crystal != null)
                {
                    crystal.SetActive(false);
                    cb.BaseActivate();
                    gameObject.SetActive(false);
                }
                else
                {
                    price = 0;
                    gameObject.SetActive(false);
                }
               
            }

            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - price);
            main.UpdateMoneyText();
        }
        else
        {
            main.notenoughmoney.SetActive(true);
        }

    }

    IEnumerator ShieldTimer()
    {
        main.shielded = true;
        timertext.gameObject.SetActive(true);
        bttn.interactable = false;
        var tempColor = shieldimg.color;
        tempColor.a = 0.7f;
        shieldimg.color = tempColor;

        for (int i = 3; i > 0; i--)
        {
            timertext.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        timertext.gameObject.SetActive(false);
        bttn.interactable = true;
        tempColor.a = 1f;
        shieldimg.color = tempColor;
        main.shielded = false;
    }
}
