using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrystalsBases : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int colorindex; // 1- green , 2 - red, 3- blue
    public Image crystalimg;
    AudioSys aus;
    [Header("Lighter")]   
    public GameObject lighterbeam;
    public bool lighter, lactivated;    
    bool buttonPressed;  
   
    [Header("Beams")]
    public bool isbeam;
    public GameObject lighterparent;
    // Start is called before the first frame update
    void Start()
    {

        aus = GameObject.Find("AudioSys").GetComponent<AudioSys>();

        if (lighter)
        {

            lighterbeam.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            if (!lactivated)
            {
                lighterbeam.SetActive(false);
            }
            else
            {
                BaseActivate();
            }
           
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {

        if (lighter)
        {
            if (buttonPressed)
            {

                lighterbeam.transform.Rotate(0, 0, 50 * Time.deltaTime);
            }
        }

        
      
    }




    public void BaseActivate()
    {
        if (lighter)
        {
            lactivated = true;
            lighterbeam.SetActive(true);
        }
        aus.PlaySound(aus.crystalenter);
        var tempColor = crystalimg.color;
        tempColor.a = 1f;
        crystalimg.color = tempColor;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
