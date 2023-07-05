using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using TMPro;


public class OyunSahnesiInfoRoom : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject Cevap;
    [SerializeField] GameObject Gönder;
    [SerializeField] GameObject Puan;
    [SerializeField] TextMeshProUGUI Uyarı;
    [SerializeField] TextMeshProUGUI Cevap2;
    TextMeshProUGUI RandomKelime;
    int Flag;
    void Start()
    {
        RandomKelime = GameObject.Find("RandomKelime").GetComponent<TextMeshProUGUI>();
    }

    float timeLeft = 10.0f;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/InfoRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
            timeLeft = 5.0f;
        }
    }
    public void InfoAl()
    {
        StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/InfoRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
    }
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
        //Debug.Log(request.downloadHandler.text);
        processJsonData(request.downloadHandler.text);

    }
    private void processJsonData(string _url)
    {
        InfoRoomSec infoRoom = JsonUtility.FromJson<InfoRoomSec>(_url);
        //Debug.Log(infoRoom.players);
        //Debug.Log(infoRoom.word);
        //Debug.Log(infoRoom.tour);
        //Debug.Log(infoRoom.playersTurn);
        //Debug.Log(infoRoom.isGameStarted);
        //Debug.Log(infoRoom.status);
        //Debug.Log(infoRoom.kisiSayisi);
        // Debug.Log(infoRoom.puan);
        GlobalKullanıcıBilgileri._playerTurn = infoRoom.playersTurn;
        RandomKelime.text = infoRoom.word;
        GameObject.Find("SıraSahibi").GetComponent<TextMeshProUGUI>().text = infoRoom.playersTurn;
        if (infoRoom.kisiSayisi < 2) 
        {
            GameObject.Find("Game").GetComponent<PlayerQuit>().PostData();
            GlobalKullanıcıBilgileri._Room_key =null;
            SceneManager.LoadScene(0);
        }
        if (GlobalKullanıcıBilgileri._OyuncuIsim == infoRoom.playersTurn)
        {
            if(Flag == 0)
            {
                audio.Play();
                Handheld.Vibrate();
                Flag++;
            }
            Cevap.active = true;
            Gönder.active = true;
            Uyarı.enabled = true;
        }
        else
        {
            Flag = 0;
            Cevap.GetComponent<TMP_InputField>().text = "";
            Uyarı.text = "";
            Cevap.active = false;
            Gönder.active = false;
            Uyarı.enabled = false;
        }
        Puan.GetComponent<TextMeshProUGUI>().text = infoRoom.puan.ToString();
    }

    private string processJson(string _url, string room_key)
    {
        //Debug.Log(_url);
        InfoRoom info = new InfoRoom();
        //Debug.Log(room_key);
        info.playerName = _url;
        info.roomKey = room_key;
        string json = JsonUtility.ToJson(info);
        Debug.Log(json);
        return json;
    }
    private class InfoRoom
    {
        public string playerName;
        public string roomKey;
    }
    
}
