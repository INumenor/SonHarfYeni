using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using TMPro;
using DG.Tweening;

public class BotGameInfo : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject Cevap;
    [SerializeField] GameObject Gönder;
    [SerializeField] TextMeshProUGUI Puan;
    [SerializeField] TextMeshProUGUI Uyarı;
    TextMeshProUGUI RandomKelime;

    [SerializeField] GameObject Kutucuk;
    [SerializeField] List<GameObject> Kutucuklar;
    [SerializeField] GameObject Canvas;
    [SerializeField] List<GameObject> Ballons;
    GameObject copyyenikutucuk;
    float left = -2.5f;
    float right = 2.5f;
    float aralık;
    float Length;

    int Flag;
    void Start()
    {
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
        if (infoRoom.isPlayer == "bot" && Ballons.Count == 0)
        {
            BallonsCreate(infoRoom.word);
            Flag = 0;
        }
        if (Flag == 0)
        {
            audio.Play();
            Handheld.Vibrate();
            Flag++;
            Puan.text = infoRoom.puan.ToString();
            Cevap.active = true;
            Gönder.active = true;
        }
        else if(infoRoom.isPlayer != "bot" && Flag == 1)
        {
            Debug.Log("Geldim");
            Cevap.active = false;
            Gönder.active = false;
        }
        
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
