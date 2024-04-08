using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yavuz;

public class AyarlarManager : MonoBehaviour
{
    private bellekYonetimi bellekYonetimi = new bellekYonetimi();
    public AudioSource ButtonSes;
    public Slider Muzik;
    public Slider oyunSes;

    private VeriYonetimi veriYonetimi = new VeriYonetimi();

    public List<DilVerilerAnaObj> DilVerilerAnaObj = new List<DilVerilerAnaObj>();
    private List<DilVerilerAnaObj> DilOkulmaVerileri = new List<DilVerilerAnaObj>();
    public Text[] textObj;
    public Text DilTxt;
    public Button[] dilButonlarý;

    private int aktifdilIndex;

    void Start()
    {
        veriYonetimi.DilLoad();
        DilOkulmaVerileri = veriYonetimi.DilVErileriListeAktar();
        DilVerilerAnaObj.Add(DilOkulmaVerileri[3]);
        DilTercihi();
        DilDurumunuKontrolEt();

        Muzik.value = bellekYonetimi.VeriOku_f("Muzik");
        oyunSes.value = bellekYonetimi.VeriOku_f("OyunSes");
        ButtonSes.volume = PlayerPrefs.GetFloat("OyunSes");
    }

    public void SesAyar(string HangiSes)
    {
        switch (HangiSes)
        {
            case "muzik":
                bellekYonetimi.VeriKAydetfloat("Muzik", Muzik.value);
                break;


            case "oyunses":
                bellekYonetimi.VeriKAydetfloat("OyunSes", oyunSes.value);
                break;
        }
    }

    public void DilTercihi()
    {
        if (bellekYonetimi.VeriOku_s("Dil") == "TR")
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

    void DilDurumunuKontrolEt()
    {
        if (bellekYonetimi.VeriOku_s("Dil") == "TR")
        {
            aktifdilIndex = 0;
            DilTxt.text = "Türkçe";
            dilButonlarý[0].interactable = false;
        }
        else
        {
            aktifdilIndex = 1;
            DilTxt.text = "English";
            dilButonlarý[1].interactable = false;
        }
    }


    public void DilDegistir(string yon)
    {
        if (yon == "ileri")
        {
            Debug.Log("Ýleri basýldý");
            aktifdilIndex = 1;
            DilTxt.text = "English";
            dilButonlarý[1].interactable = false;
            dilButonlarý[0].interactable = true;
            bellekYonetimi.VeriKAydet_string("Dil", "EN");
            DilTercihi();
        }
        else
        {
            aktifdilIndex = 0;
            DilTxt.text = "Türkçe";
            dilButonlarý[0].interactable = false;
            dilButonlarý[1].interactable = true;
            bellekYonetimi.VeriKAydet_string("Dil", "TR");
            DilTercihi();
        }

        ButtonSes.Play();
    }


    public void GeriDon()
    {
        ButtonSes.Play();
        SceneManager.LoadScene(0);
    }
}
