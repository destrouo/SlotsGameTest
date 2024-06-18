using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Crystal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int colorindex; // 1- green , 2 - red, 3- blue,4 - yellow
    private Vector3 startPosition;
    private Vector3 offset;
    bool buttonPressed;
    public bool standcrystal;
    MainSys main;
    private void Start()
    {
       
        startPosition = transform.position;

        main = GameObject.Find("Main Camera").GetComponent<MainSys>();
    }


    void Update()
    {
      

        if (buttonPressed && !standcrystal)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }



    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<CrystalsBases>())
        {

            var cryst = col.gameObject.GetComponent<CrystalsBases>();

            if (!standcrystal)
            {
                if (cryst.isbeam || cryst.colorindex != colorindex)
                {
                    if (transform.position != startPosition)
                    {
                        if (!main.shielded)
                        {
                            transform.position = startPosition;
                            buttonPressed = false;
                        }
                        
                    }
                   
                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<CrystalsBases>())
        {

            var cryst = col.gameObject.GetComponent<CrystalsBases>();

            if (!standcrystal)
            {

                if (cryst.isbeam || cryst.colorindex != colorindex)
                {
                    if(transform.position != startPosition)
                    {
                        if (!main.shielded)
                        {
                            transform.position = startPosition;
                            buttonPressed = false;
                        }
                    }                    
                    
                }
                else if (cryst.colorindex == colorindex)
                {

                    cryst.BaseActivate();
                    main.amountactivated++;
                    Destroy(gameObject);
                }
            }
            else
            {
                if(cryst.isbeam && cryst.colorindex == colorindex)
                {
                    var bc = col.gameObject.GetComponent<Image>();


                    if (colorindex ==1)
                    {
                        bc.color = Color.green;
                    }
                    if (colorindex == 2)
                    {
                        bc.color = Color.red;
                    }
                    if (colorindex == 3)
                    {
                        bc.color = Color.blue;
                    }
                    if (colorindex == 4)
                    {
                        bc.color = Color.yellow;
                    }

                    main.amountactivated++;
                }
            }
            
            
            
           
        }

    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (standcrystal)
        {
            if (col.gameObject.GetComponent<CrystalsBases>())
            {
                var cryst = col.gameObject.GetComponent<CrystalsBases>();


                if (cryst.isbeam && cryst.colorindex == colorindex)
                {

                    col.gameObject.GetComponent<Image>().color = Color.white;
                    main.amountactivated--;
                }
            }


        }
    
    }




            public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

}
