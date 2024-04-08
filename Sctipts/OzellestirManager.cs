using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yavuz;
using UnityEngine.UI;


public class OzellestirManager : MonoBehaviour
{
    public AudioSource ButtonSes;
    bellekYonetimi bellek = new bellekYonetimi();


    public Text coinTxt;
    public Text KarakterTxt;
    public GameObject[] Karakterler;

    int KarakterIndex = 0;
    public Button[] KarakterYonBtn;
    public Button[] islemButonlarý;


    public Animator kaydetAnim;

    VeriYonetimi veriYonetimi = new VeriYonetimi();

    public List<DilVerilerAnaObj> DilVerilerAnaObj = new List<DilVerilerAnaObj>();
    List<DilVerilerAnaObj> DilOkulmaVerileri = new List<DilVerilerAnaObj>();
    public Text[] textObj;
    string SatýnAlmaTxt;

    public List<ItemBilgileri> ItemBilgileri = new List<ItemBilgileri>();
    void Start()
    {
        // bellek.VeriKAydet_int("Coin", 1000);

        veriYonetimi.DilLoad();
        DilOkulmaVerileri = veriYonetimi.DilVErileriListeAktar();
        DilVerilerAnaObj.Add(DilOkulmaVerileri[1]);
        DilTercihi();

        ButtonSes.volume = PlayerPrefs.GetFloat("OyunSes");

        coinTxt.text = bellek.VeriOku_i("Coin").ToString();

        Debug.Log(bellek.VeriOku_i("AktifKarakter"));
        DurumKontrolEt();
        veriYonetimi.Load();
        ItemBilgileri = veriYonetimi.ListeAktar();

        //Debug.Log(Application.persistentDataPath + "/DilVerileri.gd");
    }
    public void DilTercihi()
    {
        if (bellek.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < textObj.Length; i++)
            {
                textObj[i].text = DilVerilerAnaObj[0].DilVerileri_TR[i].Metin;
            }
            SatýnAlmaTxt = DilVerilerAnaObj[0].DilVerileri_TR[1].Metin;
        }
        else
        {
            for (int i = 0; i < textObj.Length; i++)
            {
                textObj[i].text = DilVerilerAnaObj[0].DilVerileri_EN[i].Metin;
            }
            SatýnAlmaTxt = DilVerilerAnaObj[0].DilVerileri_EN[1].Metin;
        }
    }


    public void Karakter_YonBtnlar(string islem)
    {
        ButtonSes.Play();
        if (islem == "ileri")
        {
            if (KarakterIndex == 0)
            {
                KarakterTxt.text = ItemBilgileri[KarakterIndex].ItemAd;
                Karakterler[KarakterIndex].SetActive(false);
                KarakterIndex = 1;
                Karakterler[KarakterIndex].SetActive(true);
                KarakterTxt.text = ItemBilgileri[KarakterIndex].ItemAd;

                if (!ItemBilgileri[KarakterIndex].SatýnalmaDurumu)
                {
                    textObj[1].text = ItemBilgileri[KarakterIndex].Coin + SatýnAlmaTxt;
                    islemButonlarý[1].interactable = false;
                    if (bellek.VeriOku_i("Coin") < ItemBilgileri[KarakterIndex].Coin)
                        islemButonlarý[0].interactable = false;
                    else
                        islemButonlarý[0].interactable = true;


                }
                else
                {
                    textObj[1].text = SatýnAlmaTxt;
                    islemButonlarý[0].interactable = false;
                    islemButonlarý[1].interactable = true;
                }
            }
            else
            {
                Karakterler[KarakterIndex].SetActive(false);
                KarakterIndex++;
                Karakterler[KarakterIndex].SetActive(true);
                KarakterTxt.text = ItemBilgileri[KarakterIndex].ItemAd;


                if (!ItemBilgileri[KarakterIndex].SatýnalmaDurumu)
                {
                    textObj[1].text = ItemBilgileri[KarakterIndex].Coin + SatýnAlmaTxt;
                    islemButonlarý[1].interactable = false;

                    if (bellek.VeriOku_i("Coin") < ItemBilgileri[KarakterIndex].Coin)
                        islemButonlarý[0].interactable = false;
                    else
                        islemButonlarý[0].interactable = true;

                }
                else
                {
                    textObj[1].text = SatýnAlmaTxt;
                    islemButonlarý[0].interactable = false;
                    islemButonlarý[1].interactable = true;
                }
            }

            //-------------------------
            if (KarakterIndex == Karakterler.Length - 1)
                KarakterYonBtn[1].interactable = false;
            else
                KarakterYonBtn[1].interactable = true;
            if (KarakterIndex != 0)
                KarakterYonBtn[0].interactable = true;
        }
        else
        {
            if (KarakterIndex != 0)
            {
                KarakterTxt.text = ItemBilgileri[KarakterIndex].ItemAd;
                Karakterler[KarakterIndex].SetActive(false);
                KarakterIndex--;
                if (KarakterIndex != 0)
                {

                    Karakterler[KarakterIndex].SetActive(true);
                    KarakterYonBtn[0].interactable = true;
                    KarakterTxt.text = ItemBilgileri[KarakterIndex].ItemAd;
                    if (!ItemBilgileri[KarakterIndex].SatýnalmaDurumu)
                    {
                        textObj[1].text = ItemBilgileri[KarakterIndex].Coin + SatýnAlmaTxt;
                        islemButonlarý[1].interactable = false;

                        if (bellek.VeriOku_i("Coin") < ItemBilgileri[KarakterIndex].Coin)
                            islemButonlarý[0].interactable = false;
                        else
                            islemButonlarý[0].interactable = true;
                    }
                    else
                    {
                        textObj[1].text = SatýnAlmaTxt;
                        islemButonlarý[0].interactable = false;
                        islemButonlarý[1].interactable = true;
                    }

                }
                else
                {
                    //geri dönüþler (0. karakter için burya ekleme yapýlacak)
                    KarakterTxt.text = ItemBilgileri[KarakterIndex].ItemAd;
                    KarakterYonBtn[0].interactable = false;
                    Karakterler[0].SetActive(true);
                    if (!ItemBilgileri[KarakterIndex].SatýnalmaDurumu)
                    {
                        textObj[1].text = ItemBilgileri[KarakterIndex].Coin + SatýnAlmaTxt;

                        islemButonlarý[0].interactable = false;
                        islemButonlarý[1].interactable = true;

                    }
                    else
                    {
                        textObj[1].text = SatýnAlmaTxt;
                        islemButonlarý[0].interactable = false;
                        islemButonlarý[1].interactable = true;
                    }
                }
            }
            else
            {

                KarakterYonBtn[0].interactable = false;

            }
            if (KarakterIndex != Karakterler.Length - 1)
                KarakterYonBtn[1].interactable = true;
        }
    }
    void DurumKontrolEt()
    {
        if (bellek.VeriOku_i("AktifKarakter") == 0)
        {
            foreach (var item in Karakterler)
            {
                item.SetActive(false);
            }

            Karakterler[0].SetActive(true);
            KarakterIndex = 0;
            KarakterTxt.text = "Minnos";
            textObj[1].text = SatýnAlmaTxt;
            islemButonlarý[0].interactable = false;
            islemButonlarý[1].interactable = true;


        }
        else
        {
            foreach (var item in Karakterler)
            {
                item.SetActive(false);
            }

            KarakterIndex = bellek.VeriOku_i("AktifKarakter");
            Karakterler[KarakterIndex].SetActive(true);
            textObj[1].text = SatýnAlmaTxt;
            KarakterTxt.text = ItemBilgileri[KarakterIndex].ItemAd;
            islemButonlarý[0].interactable = false;
            islemButonlarý[1].interactable = true;
            coinTxt.text = bellek.VeriOku_i("Coin").ToString();
        }
    }

    public void SatýnAl()
    {
        ButtonSes.Play();
        ItemBilgileri[KarakterIndex].SatýnalmaDurumu = true;
        bellek.VeriKAydet_int("Coin", bellek.VeriOku_i("Coin") - ItemBilgileri[KarakterIndex].Coin);

        textObj[1].text = SatýnAlmaTxt;
        islemButonlarý[0].interactable = false;
        islemButonlarý[1].interactable = true;
        coinTxt.text = bellek.VeriOku_i("Coin").ToString();
    }
    public void Kaydet()
    {
        bellek.VeriKAydet_int("AktifKarakter", KarakterIndex);
        islemButonlarý[1].interactable = false;
        if (!kaydetAnim.GetBool("ok"))
        {
            kaydetAnim.SetBool("ok", true);
        }

        ButtonSes.Play();

    }
    public void GeriDon()
    {
        DurumKontrolEt();
        veriYonetimi.Save(ItemBilgileri);
        ButtonSes.Play();
        SceneManager.LoadScene(0);
    }
}
