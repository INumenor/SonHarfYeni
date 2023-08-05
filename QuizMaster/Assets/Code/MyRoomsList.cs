using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using System;
using System.Linq;

public class MyRoomsList : MonoBehaviour
{
    [SerializeField] GameObject RoomName;
    [SerializeField] GameObject RoomPlayers;
    [SerializeField] GameObject RoomType;
    [SerializeField] GameObject isTime;
    [SerializeField] GameObject PuanType;
    TextMeshProUGUI LobbySearchText;
    GameObject ScRoomName;
    GameObject ScRoomPlayers;
    GameObject ScRoomType;
    GameObject ScPuanType;
    [SerializeField] GameObject Button;
    GameObject CopyButton;
    [SerializeField] GameObject Content;
    [SerializeField] List<GameObject> clones;

    void Start()
    {
        StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/getActiveRooms",processJson(GlobalKullanıcıBilgileri._OyuncuIsim)));
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
        string jsonString = fixJson(req);
        LobbySQL[] lobby = JsonHelper.FromJson<LobbySQL>(jsonString);
        if(lobby.Length > 0)
        {
            RoomName.GetComponent<Text>().text = "Oda İsmi :" + lobby[0].RoomName.ToString();

            string[] nokta = { ";;;" };
            string[] players = lobby[0].Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);

            RoomPlayers.GetComponent<Text>().text = "Oyuncu Sayısı/Oda Limiti :" + players.Length + "/" + lobby[0].NumberOfPeople;
            RoomType.GetComponent<Text>().text = lobby[0].isPrivate.ToString();
            isTime.GetComponent<Text>().text = lobby[0].Time.ToString();
            PuanType.GetComponent<Text>().text = lobby[0].PointType.ToString();
            Button.name = lobby[0].RoomKey;

            CopyButton = Button;
            for (int i = 1; i < lobby.Length; i++)
            {
                GameObject clone = Instantiate(CopyButton, new Vector3(CopyButton.transform.position.x, CopyButton.transform.position.y - 1.4f, CopyButton.transform.position.z), CopyButton.transform.rotation);
                Transform parentTransform = clone.transform;
                GameObject firstChild = parentTransform.GetChild(0).gameObject;
                GameObject secondChild = parentTransform.GetChild(1).gameObject;
                GameObject thridChild = parentTransform.GetChild(2).gameObject;
                GameObject fourthChild = parentTransform.GetChild(3).gameObject;
                GameObject fifthChild = parentTransform.GetChild(4).gameObject;

                firstChild.GetComponent<Text>().text = "Oda İsmi :" + lobby[i].RoomName.ToString();
                string[] cloneplayers = lobby[i].Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
                secondChild.GetComponent<Text>().text = "Oyuncu Sayısı/Oda Limiti :" + cloneplayers.Length + "/" + lobby[i].NumberOfPeople;
                thridChild.GetComponent<Text>().text = lobby[i].isPrivate.ToString();
                fourthChild.GetComponent<Text>().text = lobby[i].Time.ToString();
                fifthChild.GetComponent<Text>().text = lobby[i].PointType.ToString();
                clone.name = lobby[i].RoomKey;
                clone.GetComponent<Button>().onClick.Equals(clone);

                clone.transform.parent = Content.transform;
                clone.transform.localScale = new Vector3(0.9f, 3.15f, 1);
                CopyButton = clone;
                clones.Add(clone);
            }
        }
        else
        {
            Button.active = false;
        }

    }
    private string processJson(string LobbySearch)
    {
        MyRooms ser = new MyRooms();
        ser.playerName = LobbySearch;
        string json = JsonUtility.ToJson(ser);
        return json;
    }
    public class MyRooms
    {
        public string playerName;
    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
