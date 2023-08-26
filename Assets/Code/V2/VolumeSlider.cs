using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _Slider;
    void Start()
    {
        _Slider.onValueChanged.AddListener(val => Music.Instance.ChangeMasterVolume(val));
    }
}
