using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BackgroundControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI OyunKodu;
    [SerializeField] GameObject Yıldız;
    [SerializeField] GameObject Yıldız1;
    [SerializeField] GameObject Yıldız2;
    [SerializeField] GameObject Yıldız3;
    [SerializeField] GameObject Yıldız4;
    [SerializeField] GameObject Yıldız5;
    [SerializeField] GameObject Yıldız6;
    [SerializeField] GameObject Yıldız7;
    [SerializeField] GameObject Bulut;
    [SerializeField] GameObject Bulut1;
    [SerializeField] GameObject Bulut2;
    [SerializeField] GameObject gunes;
    [SerializeField] GameObject ArkaPlan;
    [SerializeField] Sprite Gece;
    [SerializeField] Sprite Gündüz;
    void Start()
    {
        if (GlobalKullanıcıBilgileri._Day == "Night")
        {
            Yıldız.active = true;
            Yıldız1.active = true;
            Yıldız2.active = true;
            Yıldız3.active = true;
            Yıldız4.active = true;
            Yıldız5.active = true;
            Yıldız6.active = true;
            Yıldız7.active = true;
            Bulut.active = false;
            Bulut1.active = false;
            Bulut2.active = false;
            gunes.active = false;
            ArkaPlan.GetComponent<SpriteRenderer>().sprite = Gece;
            OyunKodu.color = new Color32(255,227,73,255);
        }
        else
        {
            Yıldız.active = false;
            Yıldız1.active = false;
            Yıldız2.active = false;
            Yıldız3.active = false;
            Yıldız4.active = false;
            Yıldız5.active = false;
            Yıldız6.active = false;
            Yıldız7.active = false;
            Bulut.active = true;
            Bulut1.active = true;
            Bulut2.active = true;
            gunes.active = true;
            ArkaPlan.GetComponent<SpriteRenderer>().sprite = Gündüz;
            OyunKodu.color = new Color32(0,0,0,255);
        }
    }
}
