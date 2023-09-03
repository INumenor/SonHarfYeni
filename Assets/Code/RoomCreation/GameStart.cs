using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject LobbyExitButton;
    [SerializeField] GameObject RoomName;
    [SerializeField] GameObject RoomPlayers;
    [SerializeField] GameObject RoomType;
    [SerializeField] GameObject isTime;
    [SerializeField] GameObject PuanType;
    [SerializeField] GameObject RoomKey;
    [SerializeField] GameObject RawImage;
    [SerializeField] GameObject CopyRawImage;
    [SerializeField] GameObject Content;
    [SerializeField] GameObject StartButton;
    [SerializeField] List<GameObject> clones;
    bool exitbutton;
    int Flag = 0;

    float timeLeft = 10.0f;
   
    void Start()
    {
        StartCoroutine(Post("https://appjam.inseres.com/servicekelimeoyunu/Service/getRoomsSettingsInfo", processJson(GlobalKullanıcıBilgileri._OyuncuIsim,GlobalKullanıcıBilgileri._Room_key)));
    }
    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            StartCoroutine(Post("https://appjam.inseres.com/servicekelimeoyunu/Service/getRoomsSettingsInfo", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
            timeLeft = 5.0f;
        }
    }
    public void LobbyQuit()
    {
        StartCoroutine(Post("https://appjam.inseres.com/servicekelimeoyunu/Service/quitGame", processJson(GlobalKullanıcıBilgileri._OyuncuIsim, GlobalKullanıcıBilgileri._Room_key)));
        GlobalKullanıcıBilgileri._Room_key = null;
        SceneManager.LoadScene(1);
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
        LobbyInfoSQL lobby = JsonUtility.FromJson<LobbyInfoSQL>(req);
        string[] nokta = { ";;;" };
        string[] players = lobby.Players.Split(nokta, System.StringSplitOptions.RemoveEmptyEntries);
        if(players[0] == GlobalKullanıcıBilgileri._OyuncuIsim)
        {
            StartButton.active = true;
        }
        if (players.Length != clones.Count)
        {
            RoomName.GetComponent<Text>().text = lobby.RoomName.ToString();
            RoomKey.GetComponent<Text>().text = lobby.RoomKey.ToString();
            RoomPlayers.GetComponent<Text>().text = "Oyuncu Sayısı/Oda Limiti :" + players.Length + "/" + lobby.NumberOfPeople;
            isTime.GetComponent<Text>().text = "Tur Süresi : " + lobby.Time.ToString();
            if (lobby.isPrivate = true)
            {
                RoomType.GetComponent<Text>().text = "Herkese Açık Oda";
            }
            else
            {
                RoomType.GetComponent<Text>().text = "Özel Oda";
            }
            if(lobby.PointType == true)
            {
                PuanType.GetComponent<Text>().text = "Oyun Tipi : Kelime Zorluğu";
            }
            else
            {
                PuanType.GetComponent<Text>().text = "Oyun Tipi : Kelime Uzunluğu";
            }
            CopyRawImage = RawImage;
            for (int i = 0; i < clones.Count; i++)
            {
                Destroy(clones[i]);
            }
            clones.Clear();
                for (int i = 0; i < players.Length; i++)
                {
                    GameObject clone = Instantiate(CopyRawImage, new Vector3(CopyRawImage.transform.position.x, CopyRawImage.transform.position.y - 1.4f, CopyRawImage.transform.position.z), CopyRawImage.transform.rotation);
                    Transform parentTransform = clone.transform;
                    clone.active = true;
                    clone.GetComponentInChildren<Text>().text = players[i];
                    clone.name = players[i];
                    clone.transform.parent = Content.transform;
                    clone.transform.localScale = new Vector3(1.5f, 0.75f, 1);
                    CopyRawImage = clone;
                    clones.Add(clone);
                }
        }
        if(lobby.isGameStarted == true)
        {
            Debug.Log("lobby.Time:" + lobby.Time);
            GlobalKullanıcıBilgileri._iRoomGameTime = lobby.Time;
            SceneManager.LoadScene(5);
        }
    }

    private string processJson(string _url, string room_key)
    {
        LobbyRoomInfo info = new LobbyRoomInfo();
        info.playerName = _url;
        info.roomKey = room_key;
        string json = JsonUtility.ToJson(info);
        Debug.Log(json);
        return json;
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
    private class LobbyRoomInfo
    {
        public string playerName;
        public string roomKey;
    }
}
