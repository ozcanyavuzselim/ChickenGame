using UnityEngine;
using Yavuz;

public class KartalController : MonoBehaviour
{
    public float takipesafesi = 2f;
    public float kartalhızı = 2f;
    private Transform karakter;
    public AudioSource audio;

    bellekYonetimi bellek = new bellekYonetimi();
    Rigidbody rb;
    SphereCollider sphereCollider;
    public Animator animator;
    public bool atakyaptı;

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
        animator.SetBool("AtakYaptı", atakyaptı);
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
            atakyaptı = true;
        }
    }

    public void Atackt()
    {
        float mesafe = Vector3.Distance(transform.position, karakter.position);

        if (mesafe < takipesafesi)
        {
            hareketyonu = karakter.position - transform.position;
            rb.velocity = hareketyonu.normalized * kartalhızı;

        }
    }

    public void Uzaklas()
    {
        Vector3 direction = (initialPosition - transform.position).normalized;
        Vector3 desiredVelocity = direction * kartalhızı;

        if (desiredVelocity.sqrMagnitude > rb.velocity.sqrMagnitude)
        {
            rb.velocity = desiredVelocity;
        }

        if (Vector3.Distance(transform.position, initialPosition) <= 0.1f)
        {
            rb.velocity = Vector3.zero;
            isAttacking = false;
            atakyaptı = false;
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
