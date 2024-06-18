using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioSys : MonoBehaviour
{
    public GameObject music;
    public AudioClip spin, wincrystal, winslots, tap, crystalenter, bonussound, gameover, coinssmall,coinsmed,coinslarge;
    bool ismusictruned;
    public Image soundimg;
    AudioSource aus;
    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();   
    }


    public void PlaySound(AudioClip clip)
    {
        aus.PlayOneShot(clip);
    }

    public void PlayTap()
    {
        aus.PlayOneShot(tap);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        if (!music.GetComponent<AudioSource>().isPlaying)
        {
            music.GetComponent<AudioSource>().Play();
        }
       
    }

    public void MusicTurn()
    {
        ismusictruned = !ismusictruned;

        

        if (ismusictruned)
        {
            var tempColor = soundimg.color;
            tempColor.a = 0.7f;
            soundimg.color = tempColor;
            music.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            var tempColor = soundimg.color;
            tempColor.a = 1f;
            soundimg.color = tempColor;
            music.GetComponent<AudioSource>().volume = 0.3f;
        }
    }
}
