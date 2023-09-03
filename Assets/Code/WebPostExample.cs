using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebPostExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PostData("TestStr"));
    }

    IEnumerator PostData(string dataStr)
    {
        WWWForm form = new WWWForm();
        form.AddField("", dataStr);

        UnityWebRequest www = UnityWebRequest.Post("https://appjam.inseres.com/servicekelimeoyunu/Service/getRandomWord", form);

        yield return www.SendWebRequest();

        www.Dispose();
    }

}
