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
    public bool TimeButton=false;
    public int TimeBreak;
    public int NumberOfPeople=2;
    public bool PointType=false;

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

    public void Olustur()
    {
        if (RoomNameText != null && PrivatePublicButton != null && TimeButton != null && NumberOfPeople != null && PointType != null)
        {
            StartCoroutine(Post("http://appjam.inseres.com/servicekelimeoyunu/Service/CreateRoom", processJson(RoomNameText, GlobalKullanıcıBilgileri._OyuncuIsim, PrivatePublicButton, TimeButton, NumberOfPeople, PointType, TimeBreak))); ;
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
        processJsonData(request.downloadHandler.text);
    }
    private void processJsonData(string _url)
    { 
        Key key = JsonUtility.FromJson<Key>(_url);
        GlobalKullanıcıBilgileri._Room_key = key.room_key;
       
    }

    private string processJson(string RoomNameText, string PlayerName,bool PrivatePublicButton, bool TimeButton,  int NumberOfPeople, bool PointType,int TimeBreak)
    {
        SettingRoom sr = new SettingRoom();
        sr.playerName = PlayerName;
        sr.RoomNameText = RoomNameText;
        sr.PrivatePublicButton = PrivatePublicButton;
        sr.TimeButton = TimeButton;
        sr.NumberOfPeople = NumberOfPeople;
        sr.PointType = PointType;
        sr.TimeBreak = TimeBreak;
        string json = JsonUtility.ToJson(sr);
        return json;
    }
    private class SettingRoom
    {
        public string playerName;
        public string RoomNameText;
        public bool PrivatePublicButton;
        public bool TimeButton;
        public int NumberOfPeople;
        public bool PointType;
        public int TimeBreak;

    }
}
