using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetMetod : MonoBehaviour
{
    InputField outputArea;
    [SerializeField] string word;
    
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(GetData);
    }

    void GetData() => StartCoroutine(GetData_Coroutine());
    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading...";
        string uri = "http://appjam.inseres.com/servicekelimeoyunu/Service/getRandomWord";
        using(UnityWebRequest request  = UnityWebRequest.Get(uri))
        {

            
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                outputArea.text = request.error;
            else
            {
                processJsonData(request.ToString());
                //word = request.downloadHandler.text;
                //processJsonData(word);
                //outputArea.text = word;
            }
                
        }
    }
    private void processJsonData(string _url)
    {
        Debug.Log("Geldim");
        jsonDataClass jsnData = JsonUtility.FromJson<jsonDataClass>(_url);
        outputArea.text = jsnData.word;
        Debug.Log(jsnData.word);
    }
}
