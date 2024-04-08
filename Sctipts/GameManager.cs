using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using Yavuz;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameovercanva;
    public bool isPlayerDead;
    private float delayTime = 3f; // 3 saniye




    public GameObject[] dogmaNoktasý;
    public List<GameObject> Civcivler;

    public GameObject pausePanel; // Pause panelini referanslayýn



    public float scoreValue;
    public float delay = 2f;

    public Text sureText;
    public Text sureText2;
    public Text sureText3;
    public Text coinTxt;
    public ui ui;
    public GameData data;
    public Text scoretext;

    bellekYonetimi bellek = new bellekYonetimi();

    private BinaryFormatter formatter;
    private string filePath;

    

    public GameObject[] Karakterler;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        ItemleriKontrolEt();
        Destroy(GameObject.FindWithTag("MenuSes"));
       
        if (instance == null)
        {
            instance = this;
        }

    }
    void Start()
    {

        data.coin = bellek.VeriOku_i("Coin");
        CollectCoin();
        UpdateScoreText();
        isPlayerDead = false;
        UpdateHearths();
        LoadGame();
    }

    void Update()
    {
        sureText2.text = data.gecenSure.ToString("F2");
        sureText3.text = data.gecenSure.ToString("F2");
        Score();
        UpdateHearths();
        UpdateScoreText();
        bellek.VeriKAydet_int("Coin", data.coin);

    }


    //Öldüðümüzde çaðrýlacak fonksiyon
    public void PlayerDied()
    {
        if (!isPlayerDead)
        {
            data.lives = 0;
            UpdateHearths();
            SaveData();
            isPlayerDead = true;
            Invoke("DeactivateHedef", delayTime);

        }

    }

    private void DeactivateHedef()
    {
        Time.timeScale = 0;
        Karakterler[bellek.VeriOku_i("AktifKarakter")].SetActive(false);
        gameovercanva.SetActive(true);
    }

    //Leveli tekrardan baþlatýr
    public void RestartLevel()
    {

        data.lives = 5;
        Karakterler[bellek.VeriOku_i("AktifKarakter")].SetActive(true);
        gameovercanva.SetActive(false);

    }
    private void Score()
    {
        if (!isPlayerDead)
        {
            data.gecenSure += Time.deltaTime;
            sureText.text = data.gecenSure.ToString("F2");
        }

    }
    public void SaveData()
    {
        CollectCoin();
        if (data.gecenSure > scoreValue)
        {
            bellek.VeriKAydetfloat("Sure", data.gecenSure);
        }

    }
    private void UpdateScoreText()
    {
        // "scoretext" bileþenine "score" deðerini yazdýr
        scoreValue = bellek.VeriOku_f("Sure");
        string formattedScore = scoreValue.ToString("F2"); // "F2" formatý virgülden sonra iki basamak gösterir
        scoretext.text = formattedScore;

    }
    private void UpdateCoinText()
    {
        coinTxt.text = data.coin.ToString("F0"); // Ekrandaki coin sayýsýný güncelle
    }


    public void CollectCoin()
    {

        bellek.VeriKAydet_int("Coin", data.coin); // Güncellenmiþ coin sayýsýný kaydet
        UpdateCoinText(); // Ekrandaki coin sayýsýný güncelle
    }
    public void LoadGame()
    {
        data.lives = 0;
        data.gecenSure = 0;
        data.firstLoading = false;
    }
    public void CanAzalt()
    {
        data.lives -= 1; // Oyuncunun canýný 1 azalt
        UpdateHearths();
    }

    public void PlayerHealtUp()
    {
        if (data.lives < 4)
        {
            data.lives += 1;

        }
        else
            data.lives = 4;

        UpdateHearths();

    }

    // civcivlerin can sayýsý kadar gözükmesi
    private void UpdateHearths()
    {
        if (data.lives == 4)
        {
            ui.hearth1.sprite = ui.FullHearth;
            ui.hearth2.sprite = ui.FullHearth;
            ui.hearth3.sprite = ui.FullHearth;
            ui.hearth4.sprite = ui.FullHearth;
        }
        else if (data.lives == 3)
        {
            ui.hearth1.sprite = ui.FullHearth;
            ui.hearth2.sprite = ui.FullHearth;
            ui.hearth3.sprite = ui.FullHearth;
            ui.hearth4.sprite = ui.emptyHearth;
        }
        else if (data.lives == 2)
        {
            ui.hearth1.sprite = ui.FullHearth;
            ui.hearth2.sprite = ui.FullHearth;
            ui.hearth3.sprite = ui.emptyHearth;
            ui.hearth4.sprite = ui.emptyHearth;
        }
        else if (data.lives == 1)
        {
            ui.hearth1.sprite = ui.FullHearth;
            ui.hearth2.sprite = ui.emptyHearth;
            ui.hearth3.sprite = ui.emptyHearth;
            ui.hearth4.sprite = ui.emptyHearth;
        }
        else
        {
            ui.hearth1.sprite = ui.emptyHearth;
            ui.hearth2.sprite = ui.emptyHearth;
            ui.hearth3.sprite = ui.emptyHearth;
            ui.hearth4.sprite = ui.emptyHearth;
        }
    }



    //civcivler toplandýðý zaman onu takip etmesi
    public void CivcivDogma()
    {

        foreach (var item in Civcivler)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.position = dogmaNoktasý[bellek.VeriOku_i("AktifKarakter")].transform.position;
                item.transform.rotation = dogmaNoktasý[bellek.VeriOku_i("AktifKarakter")].transform.rotation;
                item.SetActive(true);
                break;
            }
        }

    }

    //civcivlerin yanýnca yok olmasý
    public void CivcivOlme()
    {

        foreach (var item in Civcivler)
        {
            if (item.activeInHierarchy)
            {
                item.transform.position = dogmaNoktasý[bellek.VeriOku_i("AktifKarakter")].transform.position;
                item.SetActive(false);
                break;
            }
        }

    }

    public void ItemleriKontrolEt()
    {
        Karakterler[bellek.VeriOku_i("AktifKarakter")].SetActive(true);
    }



}

[System.Serializable]
public class GameData
{

    public float gecenSure;
    public int coin;
    public int lives;
    public bool firstLoading;
}

[System.Serializable]
public class ui
{
    public Image hearth1;
    public Image hearth2;
    public Image hearth3;
    public Image hearth4;

    public Sprite emptyHearth;
    public Sprite FullHearth;
}