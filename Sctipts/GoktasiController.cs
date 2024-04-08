using UnityEngine;

public class GoktasiController : MonoBehaviour
{
    public float takipMesafesi;
    public GameObject goktasi;

    public Transform player;
    private Rigidbody rb; // Rigidbody bile�eni
    public float dususHizi = 30f; // Yer�ekimi h�z�

    private void Start()
    {
        goktasi.SetActive(false);
 
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Rigidbody bile�enini al
        rb = goktasi.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = goktasi.AddComponent<Rigidbody>();
        }

        // Yer�ekimini etkinle�tir ve h�z�n� art�r
        rb.useGravity = true;
        rb.velocity = new Vector3(0, -dususHizi, 0);
    }

    void Update()
    {
        float mesafePlayer = Vector3.Distance(transform.position, player.position);

        if (mesafePlayer <= takipMesafesi)
        {
            goktasi.SetActive(true);
  
        }
    }

    private void OnDrawGizmosSelected()
    {
        // �izgi �ember olu�tur
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }
}
