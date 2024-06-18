using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSys : MonoBehaviour
{
    public Text persents;
    int persent;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Persentage());
        StartCoroutine(Loading());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator Persentage()
    {
        for (int i = 0; i < 101; i++)
        {
            persents.text = i.ToString() + "%";
            yield return new WaitForSeconds(0.02f);
        }

        Application.LoadLevel(1);
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(2);
       
    }
}
