using UnityEngine;

public class KutukManager : MonoBehaviour
{

    public float takipMesafesi = 5.0f; // Karaktere olan mesafe
    public float zEkseniHizi = 1.0f; // Z ekseni boyunca hareket hýzý

    private Transform karakter;
    private bool karakterYakin = false;
    public AudioSource yuvarlanma;
    void Start()
    {
        yuvarlanma.volume = PlayerPrefs.GetFloat("OyunSes");
        karakter = GameObject.FindGameObjectWithTag("Player").transform;

        if (karakter == null)
        {
            Debug.LogError("Karakter bulunamadý!");
        }
    }

    void Update()
    {
        // Karakter ile nesne arasýndaki mesafeyi hesapla
        float mesafe = Vector3.Distance(transform.position, karakter.position);

        // Mesafe takip mesafesinden küçükse Z ekseni boyunca hareket et
        if (mesafe < takipMesafesi)
        {
            Hareket();
        }
        else
        {
            karakterYakin = false;
        }
    }
    void Hareket()
    {
        karakterYakin = true;
        Vector3 yeniPozisyon = transform.position;
        yeniPozisyon.z += zEkseniHizi * Time.deltaTime * -1;
        transform.position = yeniPozisyon;
    }
    private void OnDrawGizmosSelected()
    {
        // Takip mesafesi için çizgi çember oluþtur
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }
}
