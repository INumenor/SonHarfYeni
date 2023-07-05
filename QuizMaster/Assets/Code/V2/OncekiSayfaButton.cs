using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OncekiSayfaButton : MonoBehaviour
{
    public void OyunButton(int SahneNumarası)
    {
        GlobalKullanıcıBilgileri._Room_key = null;
        SceneManager.LoadScene(0);
    }
}

