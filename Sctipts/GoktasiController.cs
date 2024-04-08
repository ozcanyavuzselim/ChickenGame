using UnityEngine;

public class GoktasiController : MonoBehaviour
{
    public float takipMesafesi;
    public GameObject goktasi;

    public Transform player;
    private Rigidbody rb; // Rigidbody bileþeni
    public float dususHizi = 30f; // Yerçekimi hýzý

    private void Start()
    {
        goktasi.SetActive(false);
 
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Rigidbody bileþenini al
        rb = goktasi.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = goktasi.AddComponent<Rigidbody>();
        }

        // Yerçekimini etkinleþtir ve hýzýný artýr
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
        // Çizgi çember oluþtur
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }
}
