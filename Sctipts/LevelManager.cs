using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yavuz;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public GameManager manager;
    public Button[] levelButonlarý;
    public Image[] levelResimleri;
    public Sprite[] levelResimleraktif;

    public AudioSource butonSes;
    public float scoreValue;

    private bellekYonetimi bellek = new bellekYonetimi();
    private VeriYonetimi veriYonetimi = new VeriYonetimi();
    public List<DilVerilerAnaObj> DilVerilerAnaObj = new List<DilVerilerAnaObj>();
    private List<DilVerilerAnaObj> DilOkulmaVerileri = new List<DilVerilerAnaObj>();
    public Text[] textObj;


    void Start()
    {
        //bellek.VeriKAydetfloat("Sure", 2000);


        veriYonetimi.DilLoad();
        DilOkulmaVerileri = veriYonetimi.DilVErileriListeAktar();
        DilVerilerAnaObj.Add(DilOkulmaVerileri[2]);
        DilTercihi();

        LeveliAktifet();


        manager = FindObjectOfType<GameManager>();
        butonSes.volume = PlayerPrefs.GetFloat("OyunSes");
    }

    public void DilTercihi()
    {
        if (bellek.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < textObj.Length; i++)
            {
                textObj[i].text = DilVerilerAnaObj[0].DilVerileri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < textObj.Length; i++)
            {
                textObj[i].text = DilVerilerAnaObj[0].DilVerileri_EN[i].Metin;
            }

        }
    }
    public void LeveliAktifet()
    {
        scoreValue = bellek.VeriOku_f("Sure");
        if (scoreValue > -1)
        {
            levelButonlarý[0].interactable = true;
            levelResimleri[0].sprite = levelResimleraktif[0];
        }
        if (scoreValue > -1f)
        {
            levelButonlarý[1].interactable = true;
            levelResimleri[1].sprite = levelResimleraktif[1];
        }

    }
    public void SahneYukle(int index)
    {
        SceneManager.LoadScene(index);
        butonSes.Play();
    }
    public void GeriDon()
    {
        butonSes.Play();
        SceneManager.LoadScene(0);
    }
}
