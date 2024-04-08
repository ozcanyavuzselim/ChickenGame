using UnityEngine;
using Yavuz;

public class KartalController : MonoBehaviour
{
    public float takipesafesi = 2f;
    public float kartalh�z� = 2f;
    private Transform karakter;
    public AudioSource audio;

    bellekYonetimi bellek = new bellekYonetimi();
    Rigidbody rb;
    SphereCollider sphereCollider;
    public Animator animator;
    public bool atakyapt�;

    public float atakyapmasuresi;
    float atakyapmasayac;
    float mesafe;
    Vector3 hareketyonu;

    private bool isAttacking = false;
    private Vector3 initialPosition;

    private void Awake()
    {

        karakter = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        animator.SetBool("AtakYapt�", atakyapt�);
        if (!isAttacking)
        {
            Atackt();
        }
        else
        {
            Uzaklas();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audio.Play();
            isAttacking = true;
            atakyapt� = true;
        }
    }

    public void Atackt()
    {
        float mesafe = Vector3.Distance(transform.position, karakter.position);

        if (mesafe < takipesafesi)
        {
            hareketyonu = karakter.position - transform.position;
            rb.velocity = hareketyonu.normalized * kartalh�z�;

        }
    }

    public void Uzaklas()
    {
        Vector3 direction = (initialPosition - transform.position).normalized;
        Vector3 desiredVelocity = direction * kartalh�z�;

        if (desiredVelocity.sqrMagnitude > rb.velocity.sqrMagnitude)
        {
            rb.velocity = desiredVelocity;
        }

        if (Vector3.Distance(transform.position, initialPosition) <= 0.1f)
        {
            rb.velocity = Vector3.zero;
            isAttacking = false;
            atakyapt� = false;
        }

        Invoke("InaktifYap", 3f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, takipesafesi);
    }

    public void InaktifYap()
    {
        gameObject.SetActive(false);
    }
}
