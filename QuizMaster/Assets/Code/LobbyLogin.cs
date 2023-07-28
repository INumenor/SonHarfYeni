using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using TMPro;

public class LobbyLogin : MonoBehaviour
{
    [SerializeField] GameObject Popup;
    [SerializeField] string Roomkey;

    public void ControlTypeRoom(GameObject obj)
    {
        Transform parentTransform = obj.transform;
        GameObject RoomType = parentTransform.GetChild(2).gameObject;
        if (RoomType.GetComponent<Text>().text == "False")
        {
            GlobalKullanıcıBilgileri._Room_key = obj.name;
            Debug.Log(GlobalKullanıcıBilgileri._Room_key);
            StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/JoinRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
        }
        else
        {
            GlobalKullanıcıBilgileri.LoginRoom_key = obj.name;
            Popup.active = true;
            //Buraya popup olan yeri yapacaksın
        }
    }
    public void ControlKeyRoom()
    {
        if(Roomkey == GlobalKullanıcıBilgileri.LoginRoom_key)
        {
            GlobalKullanıcıBilgileri._Room_key = GlobalKullanıcıBilgileri.LoginRoom_key;
            GlobalKullanıcıBilgileri.LoginRoom_key = null;
            StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/JoinRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
        }
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
        Status stat = JsonUtility.FromJson<Status>(_url);
        if(stat.status == "success")
        {
            SceneManager.LoadScene(3);
        }
    }

    private string processJson(string _url, string room_key)
    {
        CreatRoom info = new CreatRoom();
        info.playerName = _url;
        info.roomKey = room_key;
        string json = JsonUtility.ToJson(info);
        return json;
    }
    private class CreatRoom
    {
        public string playerName;
        public string roomKey;
    }
    public void ReadInputRoomKey(string s)
    {
        Roomkey = s;
    }
}
