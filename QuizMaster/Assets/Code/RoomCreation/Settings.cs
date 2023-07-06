using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class Settings : MonoBehaviour
{
    TextMeshProUGUI RoomName;
    [SerializeField] GameObject Private;
    [SerializeField] GameObject Time;
    [SerializeField] GameObject Point;
    [SerializeField] Slider SureSlider;
    [SerializeField] Slider KisiSayisiSlider;
    public string RoomNameText;
    public bool PrivatePublicButton=false;
    public bool TimeButton;
    public int TimeBreak;
    public int NumberOfPeople;
    public bool PointType;

    private void Update()
    {
        RoomName = GameObject.Find("RoomNameText").GetComponent<TextMeshProUGUI>();
        if (RoomName != null)
        {
            RoomNameText = RoomName.text.ToString();
            Debug.Log(RoomNameText);
        }
      
    }
    public void IsPrivate()
    {
        
        if(Private.active != true)
        {
            
            Private.active = true;
            PrivatePublicButton = true;
        }
        else
        {
            Private.active = false;
            PrivatePublicButton = false;
        }
        
    }
    public void IsTime()
    {
        if (Time.active != true)
        {
            Time.active = true;
            SureSlider.interactable = true;
            TimeButton = true;
        }
        else
        {
            Time.active = false;
            SureSlider.interactable = false;
            TimeButton = false;
        }
    }
    public void SureSli()
    {
        
        if(SureSlider.interactable==true)
        {
            TimeBreak = ((int)SureSlider.value);
            Debug.Log(TimeBreak);
        }
    }
    public void KisiSayisiSli()
    {
        
        NumberOfPeople= ((int)KisiSayisiSlider.value);
      
    }
    public void point()
    {
        if (Point.active==true)
        {
            Point.active = false;
            PointType = true;
        }
        else 
        { 
            Point.active = true;
            PointType = false; 
        }
        Debug.Log(PointType);
    }

    //void Start()
    //{
    //    keycode = GameObject.Find("KeyCode").GetComponent<TextMeshProUGUI>();
    //    StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/CreateRoom", processJson(GlobalKullanıcıBilgileri._OyuncuIsim)));
    //}
    //IEnumerator Post(string url, string bodyJsonString)
    //{
    //    var request = new UnityWebRequest(url, "POST");
    //    byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
    //    request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
    //    request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    //    request.SetRequestHeader("Content-Type", "application/json");
    //    yield return request.SendWebRequest();
    //    Debug.Log("Status Code: " + request.responseCode);
    //    processJsonData(request.downloadHandler.text);
    //}
    //private void processJsonData(string _url)
    //{
    //    Debug.Log("Geldim");
    //    Key key = JsonUtility.FromJson<Key>(_url);
    //    GlobalKullanıcıBilgileri._Room_key = key.room_key;
    //    keycode.text = key.room_key;
    //    //if(key.status == "success")
    //    //{
    //    //    Oyuncu1tik.active = true;
    //    //    GameObject.Find("Oyuncu1").GetComponent<TextMeshProUGUI>().text = GlobalKullanıcıBilgileri._OyuncuIsim;
    //    //    GameObject.Find("Oyuncu1").GetComponent<TextMeshProUGUI>().color = Color.white;
    //    //}
    //    Debug.Log(key.room_key);
    //    Debug.Log(key.status);
    //}

    //private string processJson(string _url)
    //{
    //    Debug.Log(_url);
    //    PlayerName word2 = new PlayerName();
    //    word2.playerName = _url;
    //    string json = JsonUtility.ToJson(word2);
    //    Debug.Log(json);
    //    return json;
    //}
    //private class PlayerName
    //{
    //    public string playerName;
    //}
}
