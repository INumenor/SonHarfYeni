using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testkutucuk : MonoBehaviour
{
    [SerializeField] GameObject Kutucuk;
    [SerializeField] List<GameObject> Kutucuklar;
    [SerializeField] Camera Camera;
    [SerializeField] GameObject Canvas;
    float ekrangenislik;
    float ekranyükseklik;
    string text = "araba";
    void Start()
    {
        ekrangenislik = Canvas.GetComponent<RectTransform>().rect.height;
        ekranyükseklik = Canvas.GetComponent<RectTransform>().rect.width;

        Debug.Log(ekrangenislik);
        Debug.Log(ekranyükseklik);

        char[] karakterler = text.ToCharArray();

        for(int i = 0; i < karakterler.Length; i++)
        {
            GameObject yenikutucuk = Instantiate(Kutucuk, new Vector3(-2.5f, 0, 0), Quaternion.identity);
            yenikutucuk.transform.parent = Canvas.transform;
            yenikutucuk.transform.position = new Vector3(-5,0,0);
            yenikutucuk.transform.localScale = new Vector3(1, 3, 1);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
