using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KlavyeAc : MonoBehaviour
{
    private TouchScreenKeyboard keyboard;
    public void KlavyeyiAc()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
