using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;
public class QuickLogin : MonoBehaviour
{
    string Player, Password;

    private void Start()
    {
        string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        StartCoroutine(Post("https://appjam.inseres.com/servicekelimeoyunu/Service/logincontrol", processJson(deviceUniqueIdentifier)));
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
        Status status = JsonUtility.FromJson<Status>(_url);
        Debug.Log(status.status);
        if(status.status != "fail")
        {
            GlobalKullanıcıBilgileri._OyuncuIsim = status.status;
            SceneManager.LoadScene(1);
        }
    }

    private string processJson(string UniqId)
    {
        Login loginpl = new Login();
        loginpl.uniqId = UniqId;
        string json = JsonUtility.ToJson(loginpl);
        return json;
    }


    private class Login
    {
        public string uniqId;
    }
}
