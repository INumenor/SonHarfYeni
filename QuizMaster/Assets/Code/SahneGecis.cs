﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{
    public Animator Anim;
    [SerializeField] AudioSource Audio;
    public void SahneGit(int SahneNumarasi)
    {
        Anim.SetBool("Destroy", true);
        Audio.Play();
        StartCoroutine(Delay(0.30f,SahneNumarasi));
    }

    IEnumerator Delay(float delay , int SahneNumarasi)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SahneNumarasi);
    }
}
