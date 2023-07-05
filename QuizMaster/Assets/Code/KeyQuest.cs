using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class KeyQuest : MonoBehaviour
{
    public GameObject loading;
    public GameObject yanlış;
    public GameObject isimvar;
    string KOD;
    void Start()
    {
        GameObject.Find("KoduGönder").GetComponent<Button>().onClick.AddListener(PostData);
    }
    
    void PostData() => StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/JoinRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim,KOD)));
    IEnumerator Post(string url, string bodyJsonString)
    {
        Debug.Log("gel");
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
        Debug.Log(request.downloadHandler.text);
        processJsonData(request.downloadHandler.text);
    }
    public void Yolla()
    {
        
    }
    private void processJsonData(string _url)
    {
        Debug.Log("Geldim");
        success _suc = JsonUtility.FromJson<success>(_url);
        Debug.Log(_suc.status);
        if (_suc.status == "success")
        {
            Debug.Log("odada");
            GlobalKullanıcıBilgileri._Room_key = KOD;
            GameObject.Find("KeyQuest").active = false;
            GameObject.Find("KoduGönder").active = false;
            loading.active = true;
            yanlış.active = false;
        }
        else if(_suc.status == "playerExist")
        {
            if(isimvar.active == false)
            {
                isimvar.active = true;
            }
        }
        else if(_suc.status == "roomNotFound")
        {
            yanlış.active = true;
        }
    }

    private string processJson(string _url,string _roomKey)
    {
        Debug.Log(_url);
        Debug.Log(_roomKey);
        JoinRoom roomkey = new JoinRoom();
        roomkey.roomKey = _roomKey;
        roomkey.playerName = _url;
        string json = JsonUtility.ToJson(roomkey);
        Debug.Log(json);
        return json;
    }
    private class JoinRoom
    {
        public string playerName;
        public string roomKey;
    }
    public void ReadInput(string s)
    {
        KOD = s;
        Debug.Log(KOD);
        
    }

}