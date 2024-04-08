using UnityEngine;
using Yavuz;

public class CivcivController : MonoBehaviour
{
    public Transform[] startingPoint; // Civcivlerin doðma noktasý
    public Transform[] target;        // Varýþ noktasý
    public float moveSpeed = 5.0f;  // Civcivlerin hareket hýzý

    bellekYonetimi bellekYonetimi = new bellekYonetimi();
    public PlayerController[] playerController;
    private bool isMoving = false;  // Civcivlerin hareket durumu
    public AudioSource civcivSes;
    void Start()
    {
        civcivSes.volume = PlayerPrefs.GetFloat("OyunSes");
        int aktifKarakterIndex = bellekYonetimi.VeriOku_i("AktifKarakter");
        PlayerController aktifKarakter = playerController[aktifKarakterIndex];
        // Civcivlerin doðma noktasýnda baþlamasý için baþlangýç pozisyonunu ayarlayýn
        transform.position = startingPoint[bellekYonetimi.VeriOku_i("AktifKarakter")].position;
        isMoving = true; // Civcivlerin hareketine baþlayýn
    }

    void Update()
    {
        if (isMoving)
        {
            Hareket();
        }
    }
    void Hareket()
    {
        moveSpeed = playerController[bellekYonetimi.VeriOku_i("AktifKarakter")].hareket +2;
        // Varýþ noktasýna doðru hareket etmek için yönlendirmeyi hesaplayýn
        Vector3 direction = (target[bellekYonetimi.VeriOku_i("AktifKarakter")].position - transform.position).normalized;

        // Civcivi varýþ noktasýna doðru hareket ettirin
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Eðer civciv varýþ noktasýna yaklaþýrsa hareketi durdurun
    }
}
