using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_sound : MonoBehaviour
{
    public AudioSource audio;
    public int music = 0;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            audio.Stop();
            music = 1;
        }
        else if(Time.timeScale == 1 && music == 1)
        {
            audio.Play();
            music = 0;
        }
    }
}
