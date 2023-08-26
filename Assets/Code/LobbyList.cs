using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using System;
using System.Linq;

public class LobbyList : MonoBehaviour
{
    [SerializeField] GameObject RoomName;
    [SerializeField] GameObject RoomPlayers;
    [SerializeField] GameObject RoomType;
    [SerializeField] GameObject isTime;
    [SerializeField] GameObject PuanType;
    TextMeshProUGUI LobbySearchText;
    public string LobbySearch;
    GameObject ScRoomName;
    GameObject ScRoomPlayers;
    GameObject ScRoomType;
    GameObject ScPuanType;
    [SerializeField] GameObject Button;
    [SerializeField] GameObject LobbySearchButton;
    GameObject CopyButton;
    [SerializeField] GameObject Content;
    [SerializeField] List<GameObject> clones;

    void Start()
    {
        StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/getOpenRooms", "{}"));
    }
    public void PostData()
    {
        for(int i = 0; i < clones.Count; i++)
        {
            Destroy(clones[i]);
        }
        clones.Clear();
        StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/getOpenRooms", "{}"));
    }
    public void PD()
    {
        for (int i = 0; i < clones.Count; i++)
        {
            Destroy(clones[i]);
        }
        clones.Clear();
        StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/getSearchRooms", processJson(LobbySearch)));
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
        Debug.Log(lobby.Length);
        RoomName.GetComponent<Text>().text = lobby[0].RoomName.ToString();

        string[] nokta = { ";;;" };
        string[] players = lobby[0].Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);

        RoomPlayers.GetComponent<Text>().text = "Oyuncu Sayısı :" + players.Length+"/"+lobby[0].NumberOfPeople;
        if (lobby[0].isPrivate == true)
        {
            RoomType.GetComponent<Text>().text = "Herkese Açık";
        }
        else
        {
            RoomType.GetComponent<Text>().text = "Özel";
        }
        if (lobby[0].Time == 0)
        {
            isTime.GetComponent<Text>().text = "Süresiz";
        }
        else
        {
            isTime.GetComponent<Text>().text = "Süre : " + lobby[0].Time.ToString();
        }
        if (lobby[0].PointType == true)
        {
            PuanType.GetComponent<Text>().text = "Kelime Zorluğu";
        }
        else
        {
            PuanType.GetComponent<Text>().text = "Kelime Uzunluğu";
        }
        Button.name = lobby[0].RoomKey;
        Button.transform.GetChild(1).gameObject.name = players.Length + "/" + lobby[0].NumberOfPeople;
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

            firstChild.GetComponent<Text>().text = lobby[i].RoomName.ToString();
            string[] cloneplayers = lobby[i].Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
            secondChild.GetComponent<Text>().text = "Oyuncu Sayısı :" + cloneplayers.Length + "/" + lobby[i].NumberOfPeople;
            if (lobby[i].isPrivate == true)
            {
                thridChild.GetComponent<Text>().text = "Herkese Açık";
            }
            else
            {
                thridChild.GetComponent<Text>().text = "Özel";
            }
            if (lobby[i].Time == 0)
            {
                fourthChild.GetComponent<Text>().text = "Süresiz";
            }
            else
            {
                fourthChild.GetComponent<Text>().text = "Süre : " + lobby[i].Time.ToString();
            }
            if (lobby[i].PointType == true)
            {
                fifthChild.GetComponent<Text>().text = "Kelime Zorluğu";
            }
            else
            {
                fifthChild.GetComponent<Text>().text = "Kelime Uzunluğu";
            }
            clone.name = lobby[i].RoomKey;
            clone.transform.GetChild(1).gameObject.name = cloneplayers.Length + "/" + lobby[i].NumberOfPeople;
            clone.GetComponent<Button>().onClick.Equals(clone);

            clone.transform.parent = Content.transform;
            clone.transform.localScale = new Vector3(1, 1.4f, 1);
            CopyButton = clone;
            clones.Add(clone);
        }

    }
    private string processJson(string LobbySearch)
    {
        Search ser = new Search();
        ser.roomName = LobbySearch;
        Debug.Log(ser.roomName);
        string json = JsonUtility.ToJson(ser);
        return json;
    }
    public void ReadInputRoomName(string s)
    {
        LobbySearch = s;
    }
    public class Search
    {
        public string roomName;
    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
