using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameSahneGec : MonoBehaviour
{
    public void SahneGit(GameObject Button)
    {
        GlobalKullanıcıBilgileri.Room_key = Button.name;
        SceneManager.LoadScene(5);
    }
}
