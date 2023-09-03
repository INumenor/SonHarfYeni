using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;
public class BotGameStart : MonoBehaviour
{
    public Animator Anim;
    [SerializeField] AudioSource Audio;
    public void PostData() => StartCoroutine(Post("https://appjam.inseres.com/servicekelimeoyunu/Service/SingelStartGame", processJson(GlobalKullanıcıBilgileri._OyuncuIsim)));
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
        BotGameStatus stat = JsonUtility.FromJson<BotGameStatus>(_url);
        if (stat.status == "success")
        {
            GlobalKullanıcıBilgileri.Room_key = stat.roomkey;
            Anim.SetBool("Destroy", true);
            Audio.Play();
            StartCoroutine(Delay(0.30f));
        }
    }
    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
    }

    private string processJson(string _url)
    {
        StartGameButton button = new StartGameButton();
        button.playerName = _url;
        string json = JsonUtility.ToJson(button);
        return json;
    }
    private class StartGameButton
    {
        public string playerName;
    }
}
