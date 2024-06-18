using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusButton : MonoBehaviour
{
    public Text pricetext;
    public int price,coef;
    public bool energy, combo,tripled;
    public GameObject flare;
    MainSys main;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Main Camera").GetComponent<MainSys>();

       
       pricetext.text = price.ToString();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        

       
            int money = PlayerPrefs.GetInt("money");

            if (money >= price)
        {   
                if (!main.isspining)
                {

                pricetext.text = price.ToString();
                PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - price);
                main.UpdateMoneyText();

                if (energy)
                {
                    main.energyint += coef;


                    //if (main.energyint > 20)
                    // {
                    //    main.energyint = 20;
                    // }

                    main.slotgameenergyteext.text = main.energyint.ToString() + "/20";

                    price += 10;

                }

                if (combo)
                {
                    GetComponent<Button>().interactable = false;
                    flare.SetActive(true);
                    main.bonuscoeff = coef;
                    main.bonuscoeffbutton = this.gameObject;
                    if (price > 23)
                    {
                        price -= 3;
                    }
                    else
                    {
                        price = 20;
                    }

                }

                if (tripled)
                {
                    main.slottripled = true;
                    GetComponent<Button>().interactable = false;
                    flare.SetActive(true);
                    main.bonustriplebutton = this.gameObject;
                    if (price > 55)
                    {
                        price -= 5;
                    }
                    else
                    {
                        price = 50;
                    }
                }

               
                }
            }
            else
            {
                main.notenoughmoney.SetActive(true);

            }
        }
       
}

