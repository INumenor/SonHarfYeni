using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using TMPro;

public class OyunGirişİnfoRoom : MonoBehaviour
{
    [SerializeField] GameObject isimvar;
    float timeLeft = 5.0f;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/InfoRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
            timeLeft = 2.0f;
        }
        if (isimvar.active == true)
        {
            Debug.Log("a");
            for (float abc = 2.0f; abc >= -1; abc -= Time.deltaTime)
            {
                if (abc < 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
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
        Debug.Log(request.downloadHandler.text);
        processJsonData(request.downloadHandler.text);

    }
    private void processJsonData(string _url)
    {
        Debug.Log("InfoRoom");
        InfoRoomSec infoRoom = JsonUtility.FromJson<InfoRoomSec>(_url);
        Debug.Log(infoRoom.players);
        Debug.Log(infoRoom.word);
        Debug.Log(infoRoom.tour);
        Debug.Log(infoRoom.playersTurn);
        Debug.Log(infoRoom.isGameStarted);
        Debug.Log(infoRoom.status);
        string[] nokta = { ";;;" };
        string[] players = infoRoom.players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
        if (infoRoom.isGameStarted == true)
        {
            SahneGit(3);    
        }
    }

    private string processJson(string _url, string room_key)
    {
        Debug.Log(_url);
        InfoRoom info = new InfoRoom();
        Debug.Log(room_key);
        info.playerName = _url;
        info.roomKey = room_key;
        string json = JsonUtility.ToJson(info);
        Debug.Log(json);
        return json;
    }
    private class InfoRoom
    {
        public string playerName;
        public string roomKey;
    }
    public void SahneGit(int SahneNumarasi)
    {
        SceneManager.LoadScene(SahneNumarasi);
    }
}
