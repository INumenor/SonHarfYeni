using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

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
            TimeBreak = 15;
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
            PointType = false;
        }
        else 
        { 
            Point.active = true;
            PointType = true; 
        }
        Debug.Log(PointType);
    }

    public void Olustur()
    {
        if (RoomNameText != null && PrivatePublicButton != null && TimeButton != null && NumberOfPeople != null && PointType != null)
        {
            StartCoroutine(Post("http://localhost:8080/ServiceKelimeOyunu/Service/CreateOnlineRoom", processJson(RoomNameText,GlobalKullanıcıBilgileri._OyuncuIsim, PrivatePublicButton, TimeButton, NumberOfPeople, PointType, TimeBreak))); ;
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
        if(key.status == "success")
        {
            GlobalKullanıcıBilgileri._iRoomGameTime = TimeBreak;
            SceneManager.LoadScene(3);
        }
    }

    private string processJson(string RoomNameText, string PlayerName,bool PrivatePublicButton, bool TimeButton,  int NumberOfPeople, bool PointType,int TimeBreak)
    {
        SettingRoom sr = new SettingRoom();
        sr.playerName = PlayerName;
        sr.roomName = RoomNameText;
        sr.isPrivate = PrivatePublicButton;
        sr.isTimeOpen = TimeButton;
        sr.numberOfPeople = NumberOfPeople;
        sr.pointType = PointType;
        sr.time = TimeBreak;
        string json = JsonUtility.ToJson(sr);
        return json;
    }
    private class SettingRoom
    {
        public string roomName;
        public string playerName;
        public bool isPrivate;
        public bool isTimeOpen;
        public int numberOfPeople;
        public bool pointType;
        public int time;

    }
}
