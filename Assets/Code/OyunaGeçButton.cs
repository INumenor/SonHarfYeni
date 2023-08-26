using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OyunaGeçButton : MonoBehaviour
{
    public void OyunButton(int SahneNumarası)
    {
        SceneManager.LoadScene(SahneNumarası);
    }
}
