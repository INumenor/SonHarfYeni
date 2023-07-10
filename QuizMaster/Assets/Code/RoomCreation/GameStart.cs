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
    [SerializeField] GameObject RoomName;
    [SerializeField] GameObject RoomPlayers;
    [SerializeField] GameObject RoomType;
    [SerializeField] GameObject isTime;
    [SerializeField] GameObject PuanType;
    [SerializeField] GameObject RoomKey;
    [SerializeField] GameObject RawImage;
    [SerializeField] GameObject Content;
    void Start()
    {
        StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/getOpenRooms", "{}"));
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
        LobbySQL lobby = JsonUtility.FromJson<LobbySQL>(jsonString);
        Debug.Log(lobby.RoomName.ToString());
        RoomName.GetComponent<Text>().text = "Oda İsmi :" + lobby.RoomName.ToString();
        RoomKey.GetComponent<Text>().text = lobby.RoomKey.ToString();
        string[] nokta = { ";;;" };
        string[] players = lobby.Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);

        RoomPlayers.GetComponent<Text>().text = "Oyuncu Sayısı/Oda Limiti :" + players.Length + "/" + lobby.NumberOfPeople;
        RoomType.GetComponent<Text>().text = lobby.isPrivate.ToString();
        isTime.GetComponent<Text>().text = lobby.Time.ToString();
        PuanType.GetComponent<Text>().text = lobby.PointType.ToString();

        for (int i = 1; i < players.Length; i++)
        {
            GameObject clone = Instantiate(RawImage, new Vector3(RawImage.transform.position.x, RawImage.transform.position.y - 1.4f, RawImage.transform.position.z), RawImage.transform.rotation);
            Transform parentTransform = clone.transform;
            string[] cloneplayers = lobby.Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
            clone.transform.parent = Content.transform;
            clone.transform.localScale = new Vector3(1, 2, 1);
            RawImage = clone;
        }
    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
