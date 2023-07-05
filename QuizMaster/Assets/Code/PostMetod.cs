using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class PostMetod : MonoBehaviour
{
    TextMeshProUGUI RandomKelime;
    
    void Start()
    {
        RandomKelime = GameObject.Find("RandomKelime").GetComponent<TextMeshProUGUI>();
        GameObject.Find("Başla").GetComponent<Button>().onClick.AddListener(PostData);
    }

    void PostData() => StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/getRandomWord", "{}"));
    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
        RandomKelime.text = request.downloadHandler.text;
        processJsonData(request.downloadHandler.text);
    }
    private void processJsonData(string _url)
    {
        Debug.Log("Geldim");
        jsonDataClass jsnData = JsonUtility.FromJson<jsonDataClass>(_url);
        RandomKelime.text = jsnData.word;
        Debug.Log(jsnData.word);
    }
}
