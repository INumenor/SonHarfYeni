using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] public Animator FlamentSettings;
    [SerializeField] public GameObject FlamentSettingsMusic;

    public static Music Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void MusicStart(AudioClip clip)
    {
        
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void FlamentOpenSettings()
    {
        if(FlamentSettings.GetInteger("Open/Close") == 0)
        {
            FlamentSettings.SetInteger("Open/Close", 1);
            FlamentSettingsMusic.active = true;
        }
        else if(FlamentSettings.GetInteger("Open/Close") == 1)
        {
            FlamentSettings.SetInteger("Open/Close", 2);
            FlamentSettingsMusic.active = false;
        }
        else
        {
            FlamentSettings.SetInteger("Open/Close", 1);
            FlamentSettingsMusic.active = true;
        }
        
    }
}
