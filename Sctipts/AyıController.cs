using UnityEngine;

public class AyıController : MonoBehaviour
{
    public bool girdi = false;
    public string yon;
    public Transform[] pozısyonlar;
    public float Hız;
    public float beklemeSure;
    private bool karakterYakin = false;
    int kacıncıpos;
    private Transform karakter;
    Animator animator;
    public float takipMesafesi;
    void Start()
    {
        girdi = false;
        karakter = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        foreach (Transform pos in pozısyonlar)
        {
            pos.parent = null;
        }
    }


    void Update()
    {
        
        float mesafe = Vector3.Distance(transform.position, karakter.position);
        if (mesafe < takipMesafesi)
        {
            girdi = true;
            if (beklemeSure > 0)
            {

                bekleme();
            }
            else
            {
                HareketEtme();
            }
        }
        else
        {

            karakterYakin = false;
        }
        if (girdi == true)
        {
            if(mesafe > takipMesafesi)
            {
                Destroy(gameObject);
            }
        }

    }
    void bekleme()
    {
        beklemeSure -= Time.deltaTime;
        animator.SetBool("HareketEtsinmi", false);
    }
    void HareketEtme()
    {
        karakterYakin = true;
        animator.SetBool("HareketEtsinmi", true);

        if (yon == "duz")
        {
            if (transform.position.z > pozısyonlar[kacıncıpos].position.z)
            {
                transform.localScale = new Vector3(1, 1, -1);
            }
            else if (transform.position.z < pozısyonlar[kacıncıpos].position.z)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
        }
        else if (yon == "yan")
        {
            if (transform.position.x > pozısyonlar[kacıncıpos].position.x)
            {
                transform.localScale = new Vector3(1, 1, -1);
            }
            else if (transform.position.x < pozısyonlar[kacıncıpos].position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
        }


        transform.position = Vector3.MoveTowards(transform.position, pozısyonlar[kacıncıpos].position, Hız * Time.deltaTime);
        if (Vector3.Distance(transform.position, pozısyonlar[kacıncıpos].position) < 0.1f)
        {
            beklemeSure = 3;
            PosDegistir();
        }
    }
    void PosDegistir()
    {
        kacıncıpos++;
        if (kacıncıpos >= pozısyonlar.Length)
        {
            kacıncıpos = 0;

        }
    }

    void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            // Ayıyı oyuncuya doğru çevirme
            Vector3 directionToPlayer = (karakter.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0f, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Oyuncuyla çarpıştığında çalışacak kodlar
            animator.SetTrigger("Player");
            Hız = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Takip mesafesi için çizgi çember oluştur
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }
}
