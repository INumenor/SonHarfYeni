using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class RegisterPlayer : MonoBehaviour
{
    string send;
    void PostData() => StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/SendWord", processJson(send)));
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
        Debug.Log("Geldim");
        TestWordJson TestWord = JsonUtility.FromJson<TestWordJson>(_url);
        Debug.Log(TestWord.new_word);
        Debug.Log(TestWord.status);
    }

    private string processJson(string _url)
    {
        Debug.Log(_url);
        Word word2 = new Word();
        word2.word = _url;
        string json = JsonUtility.ToJson(word2);
        Debug.Log(json);
        return json;
    }

    //public void ReadInput(string s)
    //{
    //    send = s;
    //    Debug.Log(send);
    //}
    private class Word
    {
        public string word;
    }
}
