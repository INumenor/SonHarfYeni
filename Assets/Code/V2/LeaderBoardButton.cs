using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] GameObject Bilgi;
    public void Aktifleştir()
    {
        if (Bilgi.active == false)
        {
            Bilgi.active = true;
        }
        else
        {
            Bilgi.active = false;
        }
    }
}
