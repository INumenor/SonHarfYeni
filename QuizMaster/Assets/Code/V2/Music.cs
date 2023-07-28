using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] public Animator FlamentSettings;
    [SerializeField] public Animator EkipAnim;
    [SerializeField] public GameObject FlamentSettingsMusic;
    [SerializeField] public GameObject FlamentLeader;
    [SerializeField] public Text PlayerName;

    public static Music Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            FlamentLeader.active = false;
            PlayerName.text = GlobalKullanıcıBilgileri._OyuncuIsim;
        }
        else if(FlamentSettings.GetInteger("Open/Close") == 1)
        {
            FlamentSettings.SetInteger("Open/Close", 2);
            FlamentSettingsMusic.active = false;
            FlamentLeader.active = false;
        }
        else
        {
            FlamentSettings.SetInteger("Open/Close", 1);
            FlamentSettingsMusic.active = true;
            FlamentLeader.active = false;
            PlayerName.text = GlobalKullanıcıBilgileri._OyuncuIsim;
        }
        
    }
    public void FlamentOpenLeaderBoard()
    {
        if (FlamentSettings.GetInteger("Open/Close") == 0)
        {
            FlamentSettings.SetInteger("Open/Close", 1);
            FlamentSettingsMusic.active = false;
            FlamentLeader.active = true;
        }
        else if (FlamentSettings.GetInteger("Open/Close") == 1)
        {
            FlamentSettings.SetInteger("Open/Close", 2);
            FlamentSettingsMusic.active = false;
            FlamentLeader.active = false;
        }
        else
        {
            FlamentSettings.SetInteger("Open/Close", 1);
            FlamentSettingsMusic.active = false;
            FlamentLeader.active = true;
        }

    }
    public void Ekip()
    {
        EkipAnim.SetTrigger("Ekip");
    }
}
