﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void PostData() => StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/StartGame", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
    IEnumerator Post(string url, string bodyJsonString)
    {
        //yield return new WaitForSeconds(5);
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
    private void processJsonData(string _url)
    {
        Debug.Log("InfoRoom");
        Status stat = JsonUtility.FromJson<Status>(_url);
        Debug.Log("AAAAAAAAAAAA");
        Debug.Log(stat.status);
        if(stat.status == "success")
        {
            Debug.Log("Geldik");
            SceneManager.LoadScene(3);
        }
    }

    private string processJson(string _url, string room_key)
    {
        Debug.Log(_url);
        StartGameButton button = new StartGameButton();
        Debug.Log(room_key);
        button.playerName = _url;
        button.roomKey = room_key;
        string json = JsonUtility.ToJson(button);
        Debug.Log(json);
        return json;
    }
    private class StartGameButton
    {
        public string playerName;
        public string roomKey;
    }
}
