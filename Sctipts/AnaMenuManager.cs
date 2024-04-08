using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yavuz;


public class AnaMenuManager : MonoBehaviour
{
    public GameManager manager;
    public PlayerController[] playerController;
    public AudioSource buttonSes;
    public GameObject oyuniçi;
    public List<DilVerilerAnaObj> VarsayýlanDilVerilerAnaObj = new List<DilVerilerAnaObj>();
    public List<ItemBilgileri> VarsayýlanItemBilgileri = new List<ItemBilgileri>();
    public Text[] textObj;

    private bellekYonetimi bellekYonetimi = new bellekYonetimi();
    private VeriYonetimi veriYonetimi = new VeriYonetimi();
    private List<DilVerilerAnaObj> DilVerilerAnaObj = new List<DilVerilerAnaObj>();
    private List<DilVerilerAnaObj> DilOkulmaVerileri = new List<DilVerilerAnaObj>();

    private float delayTime = 3f;
    int karakterIndex;
    // Start is called before the first frame update
    void Start()
    {
        karakterIndex = bellekYonetimi.VeriOku_i("AktifKarakter");
        Debug.Log(Application.persistentDataPath + "/DilVerileri.gd");
        
        bellekYonetimi.KontrolEtVeTanimla();
        veriYonetimi.ilkKurulumDosyaOlusturma(VarsayýlanDilVerilerAnaObj, VarsayýlanItemBilgileri);


        //bellekYonetimi.VeriKAydet_string("Dil", "TR");

        veriYonetimi.DilLoad();
        DilOkulmaVerileri = veriYonetimi.DilVErileriListeAktar();
        DilVerilerAnaObj.Add(DilOkulmaVerileri[0]);

        DilTercihi();


        PlayerController aktifKarakter = playerController[karakterIndex];

        buttonSes.volume = PlayerPrefs.GetFloat("OyunSes");

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

    public void SahneYukle(int index)
    {
        buttonSes.Play();
        SceneManager.LoadScene(index);

    }
    public void Basla()
    {
        playerController[karakterIndex].BaslaKosmaya();
    }
    public void Devam()
    {
        oyuniçi.SetActive(true);
        buttonSes.Play();
        manager.RestartLevel();
        playerController[karakterIndex].IleriHareket();

        Invoke("Tekrar", delayTime);
        playerController[karakterIndex].hareket = playerController[karakterIndex].originalHareket;
        Time.timeScale = 1;
        manager.isPlayerDead = false;

    }
    private void Tekrar()
    {

        buttonSes.Play();
        playerController[karakterIndex].AktifEtme();

    }

    public void Exit()
    {

        buttonSes.Play();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}