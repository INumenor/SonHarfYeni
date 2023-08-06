using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class RegisterPlayer : MonoBehaviour
{
    [SerializeField] GameObject PopUpReg;
    [SerializeField] Animator PopUp;
    string Player,Email,Password;
    string deviceUniqueIdentifier;
    private void Start()
    {
         deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
    }
    public void PostData() => StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/registerPlayer", processJson(Player,Email,Password,deviceUniqueIdentifier)));
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
        if(status.status == deviceUniqueIdentifier)
        {
            PopUp.SetInteger("Open/ClosePopup", 1);
        }
    }

    private string processJson(string Player,string Email ,string Password,string UniqId)
    {
        Debug.Log(UniqId);
        Register registerpl = new Register();
        registerpl.playerName = Player;
        registerpl.email = Email;
        registerpl.password = Password;
        registerpl.uniqId = UniqId;
        string json = JsonUtility.ToJson(registerpl);
        return json;
    }

    public void ReadInputPlayer(string s)
    {
        Player = s;
    }
    public void ReadInputEmail(string s)
    {
        Email = s;
    }
    public void ReadInputPassword(string s)
    {
        Password = s;
    }

    private class Register
    {
        public string playerName;
        public string email;
        public string password;
        public string uniqId;
    }
}
