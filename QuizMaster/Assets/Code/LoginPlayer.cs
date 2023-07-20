using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginPlayer : MonoBehaviour
{
    string Player,Password;
    string deviceUniqueIdentifier;
    private void Start()
    {
        deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
    }
    public void PostData() => StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/loginplayer", processJson(Player, Password, deviceUniqueIdentifier)));
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
        Status status = JsonUtility.FromJson<Status>(_url);
        if("success" == status.status)
        {
            GlobalKullanıcıBilgileri._OyuncuIsim = Player;
            SceneManager.LoadScene(1);
        }
    }

    private string processJson(string Player, string Password,string UniqId)
    {
        Debug.Log(Player);
        Login loginpl = new Login();
        loginpl.playerName = Player;
        loginpl.password = Password;
        loginpl.uniqId = UniqId;
        string json = JsonUtility.ToJson(loginpl);
        return json;
    }

    public void ReadInputPlayer(string s)
    {
        Player = s;
    }
    public void ReadInputPassword(string s)
    {
        Password = s;
    }

    private class Login
    {
        public string playerName;
        public string password;
        public string uniqId;

    }
}
