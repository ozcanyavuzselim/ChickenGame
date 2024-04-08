using UnityEngine;
using Yavuz;

public class MenuSes : MonoBehaviour
{
    private static GameObject instance;
    bellekYonetimi bellek = new bellekYonetimi();
    public AudioSource Ses;


    void Start()
    {
        Ses.volume = PlayerPrefs.GetFloat("Muzik");
        DontDestroyOnLoad(gameObject);

        if (instance == null)

            instance = gameObject;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Ses.volume = PlayerPrefs.GetFloat("Muzik");
    }
}
