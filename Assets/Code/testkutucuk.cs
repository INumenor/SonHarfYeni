using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class testkutucuk : MonoBehaviour
{
    [SerializeField] GameObject Kutucuk;
    [SerializeField] List<GameObject> Kutucuklar;
    [SerializeField] Camera Camera;
    [SerializeField] GameObject Canvas;
    [SerializeField] List<GameObject> Ballons;
    GameObject copyyenikutucuk;
    float left = -2.5f;
    float right = 2.5f;
    float ssaa = 0.750f;
    float aralık;
    float boşlukpayı;
    float Length;
    float ekrangenislik;
    float ekranyükseklik;
    public string text = "alaaaa";
    void Start()
    {
        ekrangenislik = Canvas.GetComponent<RectTransform>().rect.height;
        ekranyükseklik = Canvas.GetComponent<RectTransform>().rect.width;


        char[] karakterler = text.ToCharArray();
        Length = karakterler.Length;
        if (Length % 2 == 0)
        {
            if (Length > 10)
            {
                aralık = 0.5f * (10 / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;
            }
            else
            {
                aralık = 0.5f * (Length / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;
            }
            GameObject copyyenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
            copyyenikutucuk.transform.parent = Canvas.transform;
            copyyenikutucuk.transform.position = new Vector3(left, 2f, 0);
            copyyenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
            copyyenikutucuk.GetComponentInChildren<Text>().text = karakterler[0].ToString();
            Ballons.Add(copyyenikutucuk);
            for (int i = 1; i < karakterler.Length; i++)
            {
                if (i == 10)
                {
                    break;
                }
                GameObject yenikutucuk = Instantiate(copyyenikutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                yenikutucuk.transform.parent = Canvas.transform;
                yenikutucuk.transform.position = new Vector3(copyyenikutucuk.transform.position.x+0.5f, 2f,0);
                yenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                yenikutucuk.GetComponentInChildren<Text>().text = karakterler[i].ToString();
                copyyenikutucuk = yenikutucuk;
                Ballons.Add(copyyenikutucuk);
            }
        }
        else if(Length % 2 == 1)
        {
            if (Length > 10)
            {
                aralık = 0.5f * (10 / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;
            }
            else
            {
                aralık = 0.5f * (Length / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;
            }
            GameObject copyyenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
            copyyenikutucuk.transform.parent = Canvas.transform;
            copyyenikutucuk.transform.position = new Vector3(left, 2f, 0);
            copyyenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
            copyyenikutucuk.GetComponentInChildren<Text>().text = karakterler[0].ToString();
            Ballons.Add(copyyenikutucuk);
            for (int i = 1; i < karakterler.Length; i++)
            {
                if (i == 10)
                {
                    break;
                }
                GameObject yenikutucuk = Instantiate(copyyenikutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                yenikutucuk.transform.parent = Canvas.transform;
                yenikutucuk.transform.position = new Vector3(copyyenikutucuk.transform.position.x + 0.5f, 2f, 0);
                yenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                yenikutucuk.GetComponentInChildren<Text>().text = karakterler[i].ToString();
                copyyenikutucuk = yenikutucuk;
                Ballons.Add(copyyenikutucuk);
            }
        }
        if(Length > 10 && Length % 2 == 0)
        {
                aralık = 0.5f * ((Length-10) / 2);
                aralık = right - aralık;
                left = -2.5f + (aralık) + 0.25f;

                GameObject copyyenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                copyyenikutucuk.transform.parent = Canvas.transform;
                copyyenikutucuk.transform.position = new Vector3(left, 1.5f, 0);
                copyyenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                copyyenikutucuk.GetComponentInChildren<Text>().text = karakterler[10].ToString();
                Ballons.Add(copyyenikutucuk);
            for (int i = 11; i < karakterler.Length; i++)
            {
                
                GameObject yenikutucuk = Instantiate(copyyenikutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                yenikutucuk.transform.parent = Canvas.transform;
                yenikutucuk.transform.position = new Vector3(copyyenikutucuk.transform.position.x + 0.5f, 1.5f, 0);
                yenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                yenikutucuk.GetComponentInChildren<Text>().text = karakterler[i].ToString();
                copyyenikutucuk = yenikutucuk;
                Ballons.Add(copyyenikutucuk);
            }
        }
        else if (Length > 10 && Length % 2 == 1)
        {
            aralık = 0.5f * ((Length - 10) / 2);
            aralık = right - aralık;
            left = -2.5f + (aralık) + 0.25f;

            GameObject copyyenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
            copyyenikutucuk.transform.parent = Canvas.transform;
            copyyenikutucuk.transform.position = new Vector3(left, 1.5f, 0);
            copyyenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
            copyyenikutucuk.GetComponentInChildren<Text>().text = karakterler[10].ToString();
            Ballons.Add(copyyenikutucuk);
            for (int i = 11; i < karakterler.Length; i++)
            {

                GameObject yenikutucuk = Instantiate(copyyenikutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
                yenikutucuk.transform.parent = Canvas.transform;
                yenikutucuk.transform.position = new Vector3(copyyenikutucuk.transform.position.x + 0.5f, 1.5f, 0);
                yenikutucuk.transform.localScale = new Vector3(1.75f, 5, 1);
                yenikutucuk.GetComponentInChildren<Text>().text = karakterler[i].ToString();
                copyyenikutucuk = yenikutucuk;
                Ballons.Add(copyyenikutucuk);
            }
        }
        StartCoroutine("ItemsAnimation");
    }

    IEnumerator ItemsAnimation()
    {
        for(int i = 0;i<Ballons.Count;i++)
        {
            Ballons[i].transform.localScale = Vector3.zero;
        }
        for (int i = 0; i < Ballons.Count; i++)
        {
            Ballons[i].transform.DOScale(new Vector3(1.75f,5,1),1f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
