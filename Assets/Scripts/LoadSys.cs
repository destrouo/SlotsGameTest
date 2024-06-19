using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSys : MonoBehaviour
{
    [SerializeField] private Text _textValueLoading;
    [SerializeField] private Image _fillImageLoadingGame;
    int valuePrecentage;

    void Start()
    {
        _fillImageLoadingGame.fillAmount = 0f;
        
        StartCoroutine(Persentage());
        StartCoroutine(Loading());
    }



    IEnumerator Persentage()
    {
        for (int i = 0; i < 101; i++)
        {
            _textValueLoading.text = i.ToString() + "%";
            _fillImageLoadingGame.fillAmount = (float)i / 100f;
            yield return new WaitForSeconds(0.02f);
        }

        Application.LoadLevel(1);
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(2);

    }
}
