using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BİlgiButonuAktifleştirme : MonoBehaviour
{
    [SerializeField] GameObject Bilgi;
    public void Aktifleştir()
    {
        if (Bilgi.active == false)
        {
            Bilgi.active = true;
        }
        else
        {
            Bilgi.active = false;
        }
    }
    
}
