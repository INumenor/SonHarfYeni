using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class KeyCode : MonoBehaviour
{
    TextMeshProUGUI keycode;
    [SerializeField] GameObject Oyuncu1tik;
    //[SerializeField] TextMeshProUGUI oyuncu1name;
    string send;
    void Start()
    {
        keycode = GameObject.Find("KeyCode").GetComponent<TextMeshProUGUI>();
        StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/CreateRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim)));
    }

     
    IEnumerator Post(string url, string bodyJsonString)
    {
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
        Debug.Log("Geldim");
        Key key = JsonUtility.FromJson<Key>(_url);
        GlobalKullanıcıBilgileri._Room_key = key.room_key;
        keycode.text = key.room_key;
        //if(key.status == "success")
        //{
        //    Oyuncu1tik.active = true;
        //    GameObject.Find("Oyuncu1").GetComponent<TextMeshProUGUI>().text = GlobalKullanıcıBilgileri._OyuncuIsim;
        //    GameObject.Find("Oyuncu1").GetComponent<TextMeshProUGUI>().color = Color.white;
        //}
        Debug.Log(key.room_key);
        Debug.Log(key.status);
    }

    private string processJson(string _url)
    {
        Debug.Log(_url);
        PlayerName word2 = new PlayerName();
        word2.playerName = _url;
        string json = JsonUtility.ToJson(word2);
        Debug.Log(json);
        return json;
    }
    private class PlayerName
    {
        public string playerName;
    }
}