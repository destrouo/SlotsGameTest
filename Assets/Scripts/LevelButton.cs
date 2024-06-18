using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int index,price;
    public bool opened;
    public Text numbertext;
    MainSys main;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Main Camera").GetComponent<MainSys>();


        IsOpenedLevel();
     
       

    }

    public void IsOpenedLevel()
    {
        if (index != 1)
        {
            int l = PlayerPrefs.GetInt("level" + index.ToString());

            if (l == 1)
            {
                numbertext.color = Color.magenta;
                opened = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        if (opened)
        {
            main.levels.SetActive(false);
            main.slotgame.SetActive(true);
            main.StartSlots(index - 1);
        }
        else
        {
            
            main.openlevelwindow.SetActive(true);
            main.leveltoopen = this.gameObject;
            main.openlevelwindowlvltext.text = "LEVEL " + index.ToString();
            main.pricetoopenlevel = price;
            main.openlevelwindowcointext.text = price.ToString();
        }
    }
}
