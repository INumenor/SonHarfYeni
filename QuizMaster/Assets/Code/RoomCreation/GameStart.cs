using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using System;
using System.Linq;
public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject LobbyExitButton;
    [SerializeField] GameObject RoomName;
    [SerializeField] GameObject RoomPlayers;
    [SerializeField] GameObject RoomType;
    [SerializeField] GameObject isTime;
    [SerializeField] GameObject PuanType;
    [SerializeField] GameObject RoomKey;
    [SerializeField] GameObject RawImage;
    [SerializeField] GameObject CopyRawImage;
    [SerializeField] GameObject Content;
    [SerializeField] List<GameObject> clones;
    bool exitbutton;
    int Flag = 0;

    float timeLeft = 10.0f;
   
    void Start()
    {
        StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/getRoomsSettingsInfo", processJson(GlobalKullanıcıBilgileri._OyuncuIsim,GlobalKullanıcıBilgileri._Room_key)));
    }
    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/getRoomsSettingsInfo", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
            timeLeft = 5.0f;
        }
    }
    public void LobbyQuit()
    {
        StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/quitGame", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
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
    private void processJsonData(string req)
    {
        LobbyInfoSQL lobby = JsonUtility.FromJson<LobbyInfoSQL>(req);
        if (Flag == 0)
        {
            RoomName.GetComponent<Text>().text = "Oda İsmi :" + lobby.RoomName.ToString();
            RoomKey.GetComponent<Text>().text = lobby.RoomKey.ToString();

            string[] nokta = { ";;;" };
            string[] players = lobby.Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);

            RoomPlayers.GetComponent<Text>().text = "Oyuncu Sayısı/Oda Limiti :" + players.Length + "/" + lobby.NumberOfPeople;
            RoomType.GetComponent<Text>().text = lobby.isPrivate.ToString();
            isTime.GetComponent<Text>().text = lobby.Time.ToString();
            PuanType.GetComponent<Text>().text = lobby.PointType.ToString();
            RawImage.GetComponentInChildren<Text>().text = players[0];
            RawImage.name = players[0];
            CopyRawImage = RawImage;
            clones.Add(CopyRawImage);
            for (int i = 1; i < players.Length; i++)
            {
                GameObject clone = Instantiate(CopyRawImage, new Vector3(CopyRawImage.transform.position.x, CopyRawImage.transform.position.y - 1.4f, CopyRawImage.transform.position.z), CopyRawImage.transform.rotation);
                Transform parentTransform = clone.transform;
                string[] cloneplayers = lobby.Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
                clone.GetComponentInChildren<Text>().text = players[i];
                clone.name = players[i];
                clone.transform.parent = Content.transform;
                clone.transform.localScale = new Vector3(1, 0.5f, 1);
                CopyRawImage = clone;
                clones.Add(clone);
            }
        }
        else
        {
            string[] nokta = { ";;;" };
            string[] players = lobby.Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
            int iBrake = 0;
            Debug.Log(players.Length+"a"+clones.Count);
            if (players.Length != clones.Count)
            {
                iBrake = 1;
            }
            if(iBrake == 1) 
            {
                for (int i = 1; i < clones.Count; i++)
                {
                    DestroyImmediate(clones[i]);
                    clones.Remove(clones[i]);
                }
                CopyRawImage = RawImage;
                for (int i = 1; i < players.Length; i++)
                {
                    GameObject clone = Instantiate(CopyRawImage, new Vector3(CopyRawImage.transform.position.x, CopyRawImage.transform.position.y - 1.4f, CopyRawImage.transform.position.z), CopyRawImage.transform.rotation);
                    Transform parentTransform = clone.transform;
                    string[] cloneplayers = lobby.Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
                    clone.GetComponentInChildren<Text>().text = players[i];
                    clone.name = players[i];
                    clone.transform.parent = Content.transform;
                    clone.transform.localScale = new Vector3(1, 0.5f, 1);
                    CopyRawImage = clone;
                    clones.Add(clone);
                }
            }
        }
        Flag = 1;
    }

    private string processJson(string _url, string room_key)
    {
        LobbyRoomInfo info = new LobbyRoomInfo();
        info.playerName = _url;
        info.roomKey = room_key;
        string json = JsonUtility.ToJson(info);
        Debug.Log(json);
        return json;
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
    private class LobbyRoomInfo
    {
        public string playerName;
        public string roomKey;
    }
}
