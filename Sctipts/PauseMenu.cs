using UnityEngine;
using Yavuz;

public class PauseMenu : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject pauseMenu;
    public AudioSource buttonSes;
    public GameObject oyuni�i;
    bellekYonetimi bellek = new bellekYonetimi();
    private void Start()
    {
        buttonSes.volume = PlayerPrefs.GetFloat("OyunSes");
    }

    public void Pause()
    {

        oyuni�i.SetActive(false);
        buttonSes.Play();
        isGamePaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void Resume()
    {
        oyuni�i.SetActive(true);
        buttonSes.Play();
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isGamePaused = false;
    }

}
