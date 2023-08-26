using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using System;
using System.Linq;

public class SingleGameLeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject FirstPlayer;
    [SerializeField] GameObject SecondPlayer;
    [SerializeField] GameObject ThirdPlayer;
    void Start()
    {
        StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/getSingleLeaderboard", "{}"));
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
        LeaderPlayer[] player = JsonHelper.FromJson<LeaderPlayer>(jsonString);
        FirstPlayer.GetComponent<Text>().text = player[0].player;
        SecondPlayer.GetComponent<Text>().text = player[1].player;
        ThirdPlayer.GetComponent<Text>().text = player[2].player;
    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
