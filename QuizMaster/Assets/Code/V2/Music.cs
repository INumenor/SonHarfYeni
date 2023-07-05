using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject sound;
    [SerializeField] GameObject mute;
    private void Start()
    {
        if(GlobalKullanıcıBilgileri._Music == true)
        {
            Destroy(audio);
        }
        else
        {
            GlobalKullanıcıBilgileri._Music = true;
            DontDestroyOnLoad(audio);
        }
        
    }
    public void MusicStart()
    {
        audio = GameObject.Find("Music").GetComponent<AudioSource>();
        mute.active = false;
        sound.active = true;
        audio.Play();
    }

    public void MusicStop()
    {
        audio = GameObject.Find("Music").GetComponent<AudioSource>();
        sound.active = false;
        mute.active = true;
        audio.Stop();
    }
}
