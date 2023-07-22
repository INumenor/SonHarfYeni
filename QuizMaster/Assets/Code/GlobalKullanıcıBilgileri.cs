using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalKullanıcıBilgileri : MonoBehaviour
{
    private static string OyuncuIsim;
    public static string Room_key;
    public static string LoginRoom_key;
    public static string Day;
    public static string playerTurn;
    public static int iRoomGameTime;
    public static bool Music;

    private void Start()
    {
        Music = false;
    }


    public static string _OyuncuIsim
    {
        get
        {
            return OyuncuIsim;
        }
        set
        {
            OyuncuIsim = value;
        }
    }
    public static string _Room_key
    {
        get
        {
            return Room_key;
        }
        set
        {
            Room_key = value;
        }
    }
    public static string _Day
    {
        get
        {
            return Day;
        }
        set
        {
            Day = value;
        }
    }
    public static string _playerTurn
    {
        get
        {
            return playerTurn;
        }
        set
        {
            playerTurn = value;
        }
    }
    public static bool _Music
    {
        get
        {
            return Music;
        }
        set
        {
            Music = value;
        }
    }
    public static int _iRoomGameTime
    {
        get
        {
            return iRoomGameTime;
        }
        set
        {
            iRoomGameTime = value;
        }
    }

}

