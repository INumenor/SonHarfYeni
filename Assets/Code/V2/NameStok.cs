using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameStok : MonoBehaviour
{
    public void PlayerPref()
    {
        PlayerPrefs.SetString("KullaniciAdi", GlobalKullanıcıBilgileri._OyuncuIsim);
        Debug.Log("İsminiz:" + PlayerPrefs.GetString("KullaniciAdi"));
    }
}
