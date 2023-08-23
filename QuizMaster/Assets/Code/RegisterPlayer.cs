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
    [SerializeField] Text PopUpText;
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
        Debug.Log(status.status);
        if(status.status == "success")
        {
            PopUpText.text = "Kayıt Oldun";
        }
        else if (status.status == "error")
        {
            PopUpText.text = "Kayıt Başarısız Oldu";
        }
        else if(status.status == "there is an account")
        {
            PopUpText.text = "Böyle bir hesap var";
        }
        else if(status.status == "Empty")
        {
            PopUpText.text = "Alanları Boş Bırakmayınız";
        }
        else
        {
            PopUpText.text = "Sistemde Bir sıkıntı oluştu";
        }
        PopUp.SetTrigger("Open/ClosePopup");
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
