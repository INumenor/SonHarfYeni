﻿using System.Collections;
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
    GameObject ScRoomName;
    GameObject ScRoomPlayers;
    GameObject ScRoomType;
    GameObject ScPuanType;
    [SerializeField] GameObject Button;
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
        LobbySQL[] lobby = JsonHelper.FromJson<LobbySQL>(jsonString);
        Debug.Log(lobby[0].RoomName.ToString());
        RoomName.GetComponent<Text>().text = "Oda İsmi :" + lobby[0].RoomName.ToString();

        string[] nokta = { ";;;" };
        string[] players = lobby[0].Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);

        RoomPlayers.GetComponent<Text>().text = "Oyuncu Sayısı/Oda Limiti :" + players.Length+"/"+lobby[0].NumberOfPeople;
        RoomType.GetComponent<Text>().text = lobby[0].isPrivate.ToString();
        isTime.GetComponent<Text>().text = lobby[0].Time.ToString();
        PuanType.GetComponent<Text>().text = lobby[0].PointType.ToString();
        ScRoomName = RoomName;
        ScRoomPlayers = RoomPlayers;
        Debug.Log(lobby.Length);
        for (int i = 1; i < lobby.Length; i++)
        {
            GameObject clone = Instantiate(Button, new Vector3(Button.transform.position.x, Button.transform.position.y - 1.4f, Button.transform.position.z), Button.transform.rotation);
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

            clone.transform.parent = Content.transform;
            clone.transform.localScale = new Vector3(1, 2, 1);
            Button = clone;
        }

    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}