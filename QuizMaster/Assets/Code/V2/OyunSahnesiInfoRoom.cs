using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using TMPro;
using DG.Tweening;
using System;


public class OyunSahnesiInfoRoom : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject Cevap;
    [SerializeField] GameObject Gönder;
    [SerializeField] GameObject Puan;
    [SerializeField] Text GameTime;
    [SerializeField] TextMeshProUGUI Uyarı;
    [SerializeField] TextMeshProUGUI Cevap2;
    [SerializeField] GameObject Timer;
    float iTime;
    int Flag;
    [SerializeField] GameObject Kutucuk;
    [SerializeField] List<GameObject> Kutucuklar;
    [SerializeField] GameObject Canvas;
    [SerializeField] List<GameObject> Ballons;
    GameObject copyyenikutucuk;
    float left = -2.5f;
    float right = 2.5f;
    float aralık;
    float Length;

    float timeLeft = 10.0f;

    private void Start()
    {
        if (GlobalKullanıcıBilgileri._iRoomGameTime > 0)
        {
            Timer.active = true;
        }
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        
        if (timeLeft < 0)
        {
            StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/InfoRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
            timeLeft = 3.0f;
        }
        if(Timer.active = true && Ballons.Count > 0)
        { 
            iTime -= Time.deltaTime;
            GameTime.text = (Convert.ToInt32(iTime)).ToString();
        }
    }
    //public void InfoAl()
    //{
    //    StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/InfoRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
    //}
    IEnumerator Post(string url, string bodyJsonString)
    {
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
        GlobalKullanıcıBilgileri._playerTurn = infoRoom.playersTurn;
        if (GameObject.Find("SıraSahibi").GetComponent<TextMeshProUGUI>().text != infoRoom.playersTurn)
        {
            for (int i = 0; i < Ballons.Count; i++)
            {
                Destroy(Ballons[i]);
            }
            Ballons.Clear();
            SecondSahneSendWord._Stats = null;
            BallonsCreate(infoRoom.word);
            iTime = Convert.ToSingle(GlobalKullanıcıBilgileri._iRoomGameTime);
            Debug.Log("Time : " + iTime);
            Debug.Log("Time2 : " + GlobalKullanıcıBilgileri._iRoomGameTime);
            GameObject.Find("SıraSahibi").GetComponent<TextMeshProUGUI>().text = infoRoom.playersTurn;
        }
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
        InfoRoom info = new InfoRoom();
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
    
    void BallonsCreate(string word)
    {
        char[] karakterler = word.ToCharArray();
        Length = karakterler.Length;
        if (Length % 2 == 0)
        {
            if (Length > 10)
            {
                aralık = 0.5f * (10 / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;
            }
            else
            {
                aralık = 0.5f * (Length / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;
            }
            GameObject copyyenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
            copyyenikutucuk.transform.parent = Canvas.transform;
            copyyenikutucuk.transform.position = new Vector3(left, 2f, 0);
            copyyenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
            copyyenikutucuk.GetComponentInChildren<Text>().text = karakterler[0].ToString();
            Ballons.Add(copyyenikutucuk);
            for (int i = 1; i < karakterler.Length; i++)
            {
                if (i == 10)
                {
                    break;
                }
                GameObject yenikutucuk = Instantiate(copyyenikutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                yenikutucuk.transform.parent = Canvas.transform;
                yenikutucuk.transform.position = new Vector3(copyyenikutucuk.transform.position.x + 0.5f, 2f, 0);
                yenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                yenikutucuk.GetComponentInChildren<Text>().text = karakterler[i].ToString();
                copyyenikutucuk = yenikutucuk;
                Ballons.Add(copyyenikutucuk);
            }
        }
        else if (Length % 2 == 1)
        {
            if (Length > 10)
            {
                aralık = 0.5f * (10 / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;
            }
            else
            {
                aralık = 0.5f * (Length / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;
            }
            GameObject copyyenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
            copyyenikutucuk.transform.parent = Canvas.transform;
            copyyenikutucuk.transform.position = new Vector3(left, 2f, 0);
            copyyenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
            copyyenikutucuk.GetComponentInChildren<Text>().text = karakterler[0].ToString();
            Ballons.Add(copyyenikutucuk);
            for (int i = 1; i < karakterler.Length; i++)
            {
                if (i == 10)
                {
                    break;
                }
                GameObject yenikutucuk = Instantiate(copyyenikutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                yenikutucuk.transform.parent = Canvas.transform;
                yenikutucuk.transform.position = new Vector3(copyyenikutucuk.transform.position.x + 0.5f, 2f, 0);
                yenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                yenikutucuk.GetComponentInChildren<Text>().text = karakterler[i].ToString();
                copyyenikutucuk = yenikutucuk;
                Ballons.Add(copyyenikutucuk);
            }
        }
        if (Length > 10 && Length % 2 == 0)
        {
            aralık = 0.5f * ((Length - 10) / 2);
            aralık = right - aralık;
            left = -2.5f + (aralık) + 0.25f;

            GameObject copyyenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
            copyyenikutucuk.transform.parent = Canvas.transform;
            copyyenikutucuk.transform.position = new Vector3(left, 1.5f, 0);
            copyyenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
            copyyenikutucuk.GetComponentInChildren<Text>().text = karakterler[10].ToString();
            Ballons.Add(copyyenikutucuk);
            for (int i = 11; i < karakterler.Length; i++)
            {

                GameObject yenikutucuk = Instantiate(copyyenikutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                yenikutucuk.transform.parent = Canvas.transform;
                yenikutucuk.transform.position = new Vector3(copyyenikutucuk.transform.position.x + 0.5f, 1.5f, 0);
                yenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                yenikutucuk.GetComponentInChildren<Text>().text = karakterler[i].ToString();
                copyyenikutucuk = yenikutucuk;
                Ballons.Add(copyyenikutucuk);
            }
        }
        else if (Length > 10 && Length % 2 == 1)
        {
            aralık = 0.5f * ((Length - 10) / 2);
            aralık = right - aralık;
            left = -2.5f + (aralık) + 0.25f;

            GameObject copyyenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
            copyyenikutucuk.transform.parent = Canvas.transform;
            copyyenikutucuk.transform.position = new Vector3(left, 1.5f, 0);
            copyyenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
            copyyenikutucuk.GetComponentInChildren<Text>().text = karakterler[10].ToString();
            Ballons.Add(copyyenikutucuk);
            for (int i = 11; i < karakterler.Length; i++)
            {

                GameObject yenikutucuk = Instantiate(copyyenikutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                yenikutucuk.transform.parent = Canvas.transform;
                yenikutucuk.transform.position = new Vector3(copyyenikutucuk.transform.position.x + 0.5f, 1.5f, 0);
                yenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                yenikutucuk.GetComponentInChildren<Text>().text = karakterler[i].ToString();
                copyyenikutucuk = yenikutucuk;
                Ballons.Add(copyyenikutucuk);
            }
        }
        StartCoroutine("ItemsAnimation");
    }

    IEnumerator ItemsAnimation()
    {
        for (int i = 0; i < Ballons.Count; i++)
        {
            Ballons[i].transform.localScale = Vector3.zero;
        }
        for (int i = 0; i < Ballons.Count; i++)
        {
            Ballons[i].transform.DOScale(new Vector3(1.75f, 5, 1), 1f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
