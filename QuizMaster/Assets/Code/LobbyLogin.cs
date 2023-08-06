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
    [SerializeField] GameObject FullLobby;
    [SerializeField] Animator PopupAnim;
    [SerializeField] Text RoomName;
    [SerializeField] string Roomkey;

    public void ControlTypeRoom(GameObject obj)
    {
        Transform parentTransform = obj.transform;

        string[] nokta = { "/" };
        string[] players = parentTransform.GetChild(1).gameObject.name.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
        if(int.Parse(players[0]) >= int.Parse(players[1]))
        {
            FullLobby.active = true;
        }
        else 
        { 
            GameObject RoomType = parentTransform.GetChild(2).gameObject;
            Debug.Log(RoomType.GetComponent<Text>().text);
            if (RoomType.GetComponent<Text>().text == "Herkese Açık")
            {
                GlobalKullanıcıBilgileri._Room_key = obj.name;
                Debug.Log(GlobalKullanıcıBilgileri._Room_key);
                StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/JoinRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
            }
            else
            {
                GlobalKullanıcıBilgileri.LoginRoom_key = obj.name;
                RoomName.text = parentTransform.GetChild(0).gameObject.GetComponent<Text>().text;
                Popup.active = true;
                PopupMain();
            }
        }
    }
    public void CloseFullLobby()
    {
        FullLobby.active = false;
    }
    public void ClosePopup()
    {
        PopupMain();
        StartCoroutine(Wait());
    }
    public void ControlKeyRoom()
    {
        if(Roomkey == GlobalKullanıcıBilgileri.LoginRoom_key)
        {
            GlobalKullanıcıBilgileri._Room_key = GlobalKullanıcıBilgileri.LoginRoom_key;
            GlobalKullanıcıBilgileri.LoginRoom_key = null;
            StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/JoinRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
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
    public void PopupMain()
    {
        if (PopupAnim.GetInteger("Open/ClosePopup") == 0)
        {
            PopupAnim.SetInteger("Open/ClosePopup", 1);
        }
        else if (PopupAnim.GetInteger("Open/ClosePopup") == 1)
        {
            PopupAnim.SetInteger("Open/ClosePopup", 2);
        }
        else
        {
            PopupAnim.SetInteger("Open/ClosePopup", 1);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        Popup.active = false;
    }

}
