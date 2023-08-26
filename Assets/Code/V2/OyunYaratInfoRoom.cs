using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class OyunYaratInfoRoom : MonoBehaviour
{
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Oyuncu2G;
    [SerializeField] GameObject Oyuncu3G;
    [SerializeField] GameObject Oyuncu4G;
    [SerializeField] GameObject Oyuncu5G;
    [SerializeField] GameObject Oyuncuyer;
    [SerializeField] GameObject Content;
    GameObject Sonclone;
    GameObject clone;
    int Flag = 0;
    int Oyuncu2Flag = 1;

    float timeLeft = 5.0f;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/InfoRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
            timeLeft = 2.0f;
        }
    }

    //public void PostData() => StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/InfoRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim,GlobalKullanıcıBilgileri._Room_key)));


    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
        //Debug.Log(request.downloadHandler.text);
        processJsonData(request.downloadHandler.text);
        
    }
    private void processJsonData(string _url)
    {
        //Debug.Log("InfoRoom");
        InfoRoomSec infoRoom = JsonUtility.FromJson<InfoRoomSec>(_url);
        //Debug.Log(infoRoom.players);
        //Debug.Log(infoRoom.word);
        //Debug.Log(infoRoom.tour);
        //Debug.Log(infoRoom.playersTurn);
        //Debug.Log(infoRoom.isGameStarted);
        //Debug.Log(infoRoom.status);
        string[] nokta = {";;;"};
        string[] players = infoRoom.players.Split(nokta,System.StringSplitOptions.RemoveEmptyEntries);

        if(players.Length == 1)
        {
            Oyuncuyer.GetComponent<TextMeshProUGUI>().text = GlobalKullanıcıBilgileri._OyuncuIsim;
        }       
        else if(players.Length == 2 && Oyuncu2Flag == 1)
        {
            clone = Instantiate(Oyuncuyer, new Vector3(Oyuncuyer.transform.position.x, Oyuncuyer.transform.position.y-0.5f, Oyuncuyer.transform.position.z), Oyuncuyer.transform.rotation);
            clone.transform.parent = Content.transform;
            clone.transform.localScale = new Vector3(0.725f, 1.36f, 1);
            clone.GetComponent<TextMeshProUGUI>().text = players[1];
            Button.active = true;
            Oyuncu2Flag = 0;
            Sonclone = clone;
        }
        else if(players.Length > 2)
        {
            if (Sonclone.GetComponent<TextMeshProUGUI>().text == players[players.Length - 1])
            {
                Flag = 0;
            }
            else
            {
                Flag = 1;
            }
            if (Flag == 1 )
            {
                Debug.Log("2");
                clone = Instantiate(Sonclone, new Vector3(Sonclone.transform.position.x, Sonclone.transform.position.y - 0.5f, Sonclone.transform.position.z), Sonclone.transform.rotation);
                clone.transform.parent = Content.transform;
                clone.transform.localScale = new Vector3(1, 1, 1);
                clone.GetComponent<TextMeshProUGUI>().text = players[players.Length - 1];
                Sonclone = clone;
                Flag = 0;
            }
        }
        
        //clone = Instantiate(Oyuncuyer, new Vector3(Oyuncuyer.transform.position.x, Oyuncuyer.transform.position.y-0.5f, Oyuncuyer.transform.position.z), Oyuncuyer.transform.rotation);
        //Debug.Log(clone.transform.position);
        //clone.transform.parent = Content.transform;
        //clone.transform.localScale = new Vector3(1, 1, 1);
        //clone.GetComponent<TextMeshProUGUI>().text = "Yağmur";

        //if (players.Length >= 2)
        //{
        //    Button.active = true;
        //    Oyuncu2G.active = true;
        //    GameObject.Find("Oyuncu2").GetComponent<TextMeshProUGUI>().text = players[1];
        //    GameObject.Find("Oyuncu2").GetComponent<TextMeshProUGUI>().color = Color.white;
        //}
        //if (players.Length >= 3)
        //{
        //    Button.active = true;
        //    Oyuncu3G.active = true;
        //    GameObject.Find("Oyuncu3").GetComponent<TextMeshProUGUI>().text = players[2];
        //    GameObject.Find("Oyuncu3").GetComponent<TextMeshProUGUI>().color = Color.white;
        //}
        //if (players.Length >= 4)
        //{
        //    Button.active = true;
        //    Oyuncu4G.active = true;
        //    GameObject.Find("Oyuncu4").GetComponent<TextMeshProUGUI>().text = players[3];
        //    GameObject.Find("Oyuncu4").GetComponent<TextMeshProUGUI>().color = Color.white;
        //}
        //if (players.Length >= 5)
        //{
        //    Button.active = true;
        //    Oyuncu5G.active = true;
        //    GameObject.Find("Oyuncu5").GetComponent<TextMeshProUGUI>().text = players[4];
        //    GameObject.Find("Oyuncu5").GetComponent<TextMeshProUGUI>().color = Color.white;
        //}


    }

    private string processJson(string _url,string room_key)
    {
        //Debug.Log(_url);
        InfoRoom info = new InfoRoom();
        //Debug.Log(room_key);
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
}
