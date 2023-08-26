using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSesKapamaAçma : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] GameObject sound;
    [SerializeField] GameObject mute;
    
    public void MusicContinue()
    {
        audio = GameObject.Find("Music").GetComponent<AudioSource>();
        mute.active = false;
        sound.active = true;
        audio.volume = 0.03f;
    }

    public void MusicStop()
    {
        audio = GameObject.Find("Music").GetComponent<AudioSource>();
        sound.active = false;
        mute.active = true;
        audio.volume = 0;
    }
}
