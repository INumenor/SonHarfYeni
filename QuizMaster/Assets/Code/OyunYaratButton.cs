using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OyunYaratButton : MonoBehaviour
{
    public void OyunYarat()
    {
        SceneManager.LoadScene(2);
    }
}
