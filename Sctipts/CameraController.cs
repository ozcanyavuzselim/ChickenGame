using UnityEngine;
using Cinemachine;
using Yavuz;

public class CameraController : MonoBehaviour
{
    public GameObject[] karakterler;
    bellekYonetimi bellek = new bellekYonetimi();

    Animator animator;
    public CinemachineVirtualCamera VirtualCamera;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("ikinciKamera");
       
    }

    private void LateUpdate()
    {
        VirtualCamera.Follow = karakterler[bellek.VeriOku_i("AktifKarakter")].transform;
    }
    public void cameradegis()
    {
        animator.Play("anaKamera");

     
    }
}
