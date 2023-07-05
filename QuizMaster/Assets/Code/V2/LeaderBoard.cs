using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using System;
using System.Linq;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject Oyuncuyer;
    [SerializeField] GameObject ScOyuncuyer;
    [SerializeField] GameObject OyuncuPuanyer;
    [SerializeField] GameObject ScOyuncuPuanyer;
    [SerializeField] GameObject Content;
    void Start()
    {
        StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/getLeaderboard", "{}"));
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
    private void processJsonData(string req)
    {
        Debug.Log("Geldim");
        string jsonString = fixJson(req);
        Player[] player = JsonHelper.FromJson<Player>(jsonString);
        Oyuncuyer.GetComponent<TextMeshProUGUI>().text = player[0].player;
        OyuncuPuanyer.GetComponent<TextMeshProUGUI>().text = player[0].puan;
        ScOyuncuyer = Oyuncuyer;
        ScOyuncuPuanyer = OyuncuPuanyer;
        //Convert to JSON
        Debug.Log("Geldim2");
        for(int i = 1; i < 10; i++)
        {
            GameObject clone = Instantiate(ScOyuncuyer, new Vector3(ScOyuncuyer.transform.position.x, ScOyuncuyer.transform.position.y - 0.5f, ScOyuncuyer.transform.position.z), ScOyuncuyer.transform.rotation);
            GameObject clonepuan = Instantiate(ScOyuncuPuanyer, new Vector3(ScOyuncuPuanyer.transform.position.x, ScOyuncuPuanyer.transform.position.y - 0.5f, ScOyuncuPuanyer.transform.position.z), ScOyuncuPuanyer.transform.rotation);
            clone.transform.parent = Content.transform;
            clonepuan.transform.parent = Content.transform;
            clone.transform.localScale = new Vector3(1, 1, 1);
            clonepuan.transform.localScale = new Vector3(1, 1, 1);
            clone.GetComponent<TextMeshProUGUI>().text = player[i].player;
            clonepuan.GetComponent<TextMeshProUGUI>().text = player[i].puan;
            ScOyuncuyer = clone;
            ScOyuncuPuanyer = clonepuan;
        }
        
    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
