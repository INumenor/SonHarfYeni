using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Deneme : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Oyuncu;
    [SerializeField] GameObject Oyuncuyer;
    [SerializeField] TextMeshProUGUI OyuncuPuan;
    [SerializeField] GameObject OyuncuPuanyer;
    [SerializeField] GameObject Content;
    // Start is called before the first frame update
    void Start()
    {
        GameObject clone = Instantiate(Oyuncuyer, new Vector3(Oyuncuyer.transform.position.x, Oyuncuyer.transform.position.y-50, Oyuncuyer.transform.position.z), Oyuncuyer.transform.rotation);
        GameObject clonepuan = Instantiate(OyuncuPuanyer, new Vector3(OyuncuPuanyer.transform.position.x, OyuncuPuanyer.transform.position.y-50, OyuncuPuanyer.transform.position.z), OyuncuPuanyer.transform.rotation);
        clone.transform.parent = Content.transform;
        clonepuan.transform.parent = Content.transform;
        clone.transform.localScale = new Vector3(1,1,1);
        clone.GetComponent<TextMeshProUGUI>().text = "Yağmur";
        clonepuan.GetComponent<TextMeshProUGUI>().text = "220";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
