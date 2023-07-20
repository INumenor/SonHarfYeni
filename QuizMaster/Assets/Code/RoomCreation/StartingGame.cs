using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class StartingGame : MonoBehaviour
{
    public void PostData() => StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/StartOnlineGame", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
    IEnumerator Post(string url, string bodyJsonString)
    {
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
        Status stat = JsonUtility.FromJson<Status>(_url);
        if (stat.status == "success")
        {
            SceneManager.LoadScene(6);
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
