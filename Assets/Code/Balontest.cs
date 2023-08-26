using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balontest : MonoBehaviour
{
    public Animator Anim;

    public void Clicked()
    {
        Anim.SetBool("Destroy",true);
        StartCoroutine(Delay(0.30f));
    }
    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(6);
    }
}
