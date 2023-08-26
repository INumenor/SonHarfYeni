using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GirişEkranıButonAktifleştirme : MonoBehaviour
{
    string name = null;
    [SerializeField] GameObject OyunYaratButton;
    [SerializeField] GameObject KullanıcıAdi;
    [SerializeField] GameObject OyunKatılButton;
    public TMP_InputField KullaniciAdi;
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("KullaniciAdi"));
        if (PlayerPrefs.GetString("KullaniciAdi") == "" || KullaniciAdi.text == "(Kullanıcı Adınız...)")
        {
            KullaniciAdi.text = "(Kullanıcı Adınız...)";
            OyunYaratButton.active = false;
            OyunKatılButton.active = false;
        } 
        else
        {
            KullaniciAdi.text = PlayerPrefs.GetString("KullaniciAdi");
            OyunYaratButton.active = true;
            OyunKatılButton.active = true;
        }
    }
    public void ReadInput(string s)
    {
        name = s;
        Debug.Log(name);
        if (name == "")
        {
            OyunYaratButton.active = false;
            OyunKatılButton.active = false;
            Debug.Log("İsim giriniz");
        }
        else
        {
            OyunYaratButton.active = true;
            OyunKatılButton.active = true;
            Debug.Log("İsim var");
            GlobalKullanıcıBilgileri._OyuncuIsim = name;
            if (name.Length > 20)
            {
               name = name.Substring(0, 20);
            }
            Debug.Log(GlobalKullanıcıBilgileri._OyuncuIsim);
        }
        
    }
}
