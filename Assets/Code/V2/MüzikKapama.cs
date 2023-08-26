using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MüzikKapama : MonoBehaviour
{
     AudioSource audio;
    public void MusicStop()
    {
        audio=GameObject.Find("Music").GetComponent<AudioSource>();
        Debug.Log(audio);
        Destroy(audio);
    }

}
