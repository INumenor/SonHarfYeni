﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
public class SecondSahneSendWord : MonoBehaviour
{
    string send;
    [SerializeField] Text Uyarı;
    [SerializeField] Animator PopUp;
    [SerializeField] GameObject Gönder;
    static string Stats;
    public void PostData() => StartCoroutine(Post("https://appjam.inseres.com/servicekelimeoyunu/Service/OnlineSendWord", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key, send)));

    IEnumerator Post(string url, string bodyJsonString)
    {
        //yield return new WaitForSeconds(30);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
        processJsonData(request.downloadHandler.text);
    }
    private void processJsonData(string _url)
    {
        Status stat = JsonUtility.FromJson<Status>(_url);
        if(stat.status == "wordWasUsed")
        {
            Uyarı.enabled = true;
            Uyarı.text = "Kullanılmış kelime";
        }
        else if(stat.status == "firstLetterMatchFail")
        {
            Uyarı.enabled = true;
            Uyarı.text = "Kelimenin ilk harfi uyumsuz";
        }
        else if(stat.status == "wordNotFound")
        {
            Uyarı.enabled = true;
            Uyarı.text = "Geçersiz kelime";
        }
        else
        {
            Uyarı.text = "Gönderildi";
            Uyarı.enabled = true;
            Gönder.active = false;
        }
        PopUp.SetTrigger("Anim");
        //OyunSahnesiInfoRoom InfoAl = new OyunSahnesiInfoRoom();
        //InfoAl.InfoAl();
    }

    private string processJson(string _url, string room_key,string word)
    {
        //Debug.Log(_url);
        InfoRoom info = new InfoRoom();
        //Debug.Log(room_key);
        info.playerName = _url;
        info.roomKey = room_key;
        info.word = word;
        string json = JsonUtility.ToJson(info);
        Debug.Log(json);
        return json;
    }
    private class InfoRoom
    {
        public string word;
        public string playerName;
        public string roomKey;
    }
    public void ReadInput(string s)
    {
        char[] charsToTrim = { '*', ' ', '\'' };
        string banner = s;
        send = banner.Trim(charsToTrim);
        send = send.ToLower();
        //Debug.Log(send);
    }
    public static string _Stats
    {
        get
        {
            return Stats;
        }
        set
        {
            Stats = value;
        }
    }
}
