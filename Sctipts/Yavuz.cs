using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Yavuz
{

    public class bellekYonetimi
    {
        public void VeriKAydet_string(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        public void VeriKAydetfloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        public void VeriKAydet_int(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }


        public string VeriOku_s(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public float VeriOku_f(string key) // Float deðeri döndürmelidir
        {
            return PlayerPrefs.GetFloat(key);
        }

        public int VeriOku_i(string key) // Ýnt deðeri döndürmelidir
        {
            return PlayerPrefs.GetInt(key);
        }
        public void KontrolEtVeTanimla()
        {
            if (!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level", 0);
                PlayerPrefs.SetInt("Sure", 0);
                PlayerPrefs.SetInt("Coin", 0);
                PlayerPrefs.SetInt("AktifKarakter", 0);
                PlayerPrefs.SetFloat("Muzik", 1);
                PlayerPrefs.SetFloat("OyunSes", 1);
                PlayerPrefs.SetString("Dil", "TR");
            }


        }




    }

    public class VeriYonetimi
    {


        public void Save(List<ItemBilgileri> ýtemBilgileri)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, ýtemBilgileri);
            file.Close();
        }


        List<ItemBilgileri> itemlistesi;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                itemlistesi = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();


            }
        }
        public List<ItemBilgileri> ListeAktar()
        {
            return itemlistesi;
        }
        public void ilkKurulumDosyaOlusturma(List<DilVerilerAnaObj> dilVerileri, List<ItemBilgileri> ýtemBilgileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, ýtemBilgileri);
                file.Close();
            }
            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                bf.Serialize(file, dilVerileri);
                file.Close();
            }
        }

        //--------------------------
        List<DilVerilerAnaObj> dilverilerIcListe;
        public void DilLoad()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                dilverilerIcListe = (List<DilVerilerAnaObj>)bf.Deserialize(file);
                file.Close();


            }
        }
        public List<DilVerilerAnaObj> DilVErileriListeAktar()
        {
            return dilverilerIcListe;
        }
    }

    [Serializable]
    public class ItemBilgileri
    {
        public int GrupIndex;
        public int ItemIndex;
        public string ItemAd;
        public int Coin;
        public bool SatýnalmaDurumu;


    }

    // -----------Dil Yönetimi ---------

    [Serializable]
    public class DilVerilerAnaObj
    {
        public List<DilVerileri_TR> DilVerileri_TR = new List<DilVerileri_TR>();
        public List<DilVerileri_TR> DilVerileri_EN = new List<DilVerileri_TR>();
    }
    [Serializable]
    public class DilVerileri_TR
    {
        public string Metin;
    }



}
