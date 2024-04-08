using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yavuz;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    bellekYonetimi bellek = new bellekYonetimi();
    public AudioSource ButtonSes;


    void Start()
    {
        ButtonSes.volume = PlayerPrefs.GetFloat("OyunSes");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GeriDon()
    {
        ButtonSes.Play();
        SceneManager.LoadScene(0);
    }
}
