using UnityEngine;
using Yavuz;

public class CivcivController : MonoBehaviour
{
    public Transform[] startingPoint; // Civcivlerin do�ma noktas�
    public Transform[] target;        // Var�� noktas�
    public float moveSpeed = 5.0f;  // Civcivlerin hareket h�z�

    bellekYonetimi bellekYonetimi = new bellekYonetimi();
    public PlayerController[] playerController;
    private bool isMoving = false;  // Civcivlerin hareket durumu
    public AudioSource civcivSes;
    void Start()
    {
        civcivSes.volume = PlayerPrefs.GetFloat("OyunSes");
        int aktifKarakterIndex = bellekYonetimi.VeriOku_i("AktifKarakter");
        PlayerController aktifKarakter = playerController[aktifKarakterIndex];
        // Civcivlerin do�ma noktas�nda ba�lamas� i�in ba�lang�� pozisyonunu ayarlay�n
        transform.position = startingPoint[bellekYonetimi.VeriOku_i("AktifKarakter")].position;
        isMoving = true; // Civcivlerin hareketine ba�lay�n
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
        // Var�� noktas�na do�ru hareket etmek i�in y�nlendirmeyi hesaplay�n
        Vector3 direction = (target[bellekYonetimi.VeriOku_i("AktifKarakter")].position - transform.position).normalized;

        // Civcivi var�� noktas�na do�ru hareket ettirin
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // E�er civciv var�� noktas�na yakla��rsa hareketi durdurun
    }
}
