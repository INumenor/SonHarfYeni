using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class QuitUser : MonoBehaviour
{
    [SerializeField] SahneGecis gec;
    string deviceUniqueIdentifier;
    private void Start()
    {
        deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
    }
    public void PostData() => StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/quitcontrol", processJson(deviceUniqueIdentifier)));
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
        Debug.Log(stat.status);
        if (stat.status == "success")
        {
            GlobalKullanıcıBilgileri._Room_key = null;
            GlobalKullanıcıBilgileri._OyuncuIsim = null;
            GlobalKullanıcıBilgileri._playerTurn = null;
            GlobalKullanıcıBilgileri._iRoomGameTime = 0;
            gec.QuitSahne(0);
        }
    }

    private string processJson(string _url)
    {
        QuitPlayer button = new QuitPlayer();
        button.uniqId = _url;
        string json = JsonUtility.ToJson(button);
        Debug.Log(json);
        return json;
    }
    private class QuitPlayer
    {
        public string uniqId;
    }
}
