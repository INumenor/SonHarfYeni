using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using TMPro;

public class BotGameInfo : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject Cevap;
    [SerializeField] GameObject Gönder;
    [SerializeField] GameObject Puan;
    [SerializeField] TextMeshProUGUI Uyarı;
    TextMeshProUGUI RandomKelime;
    int Flag;
    void Start()
    {
        RandomKelime = GameObject.Find("RandomKelime").GetComponent<TextMeshProUGUI>();
        StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/InfoBotRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
    }

    float timeLeft = 5.0f;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/InfoBotRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
            timeLeft = 3.0f;
        }
    }
    //public void InfoAl()
    //{
    //    StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/InfoBotRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
    //}
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
        InfoBotRoomSec infoRoom = JsonUtility.FromJson<InfoBotRoomSec>(_url);
        Debug.Log(infoRoom.isPlayer);
        Debug.Log(infoRoom.Player);
        Debug.Log(infoRoom.puan);
        Debug.Log(infoRoom.word);
        Debug.Log(infoRoom.tour);
        if (infoRoom.isPlayer == "bot")
        {
            RandomKelime.text = infoRoom.word;
        }
        //if (Flag == 0)
        //{
        //    audio.Play();
        //    Handheld.Vibrate();
        //    Flag++;
        //}
        Cevap.active = true;
        Gönder.active = true;
        Uyarı.enabled = true;
    }

    private string processJson(string _url, string room_key)
    {
        InfoRoom info = new InfoRoom();
        info.playerName = _url;
        info.roomKey = room_key;
        string json = JsonUtility.ToJson(info);
        return json;
    }
    private class InfoRoom
    {
        public string playerName;
        public string roomKey;
    }
}
