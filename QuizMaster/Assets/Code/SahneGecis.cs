using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{

    public void SahneGit(int SahneNumarasi)
    {
        SceneManager.LoadScene(SahneNumarasi);
    }
}
