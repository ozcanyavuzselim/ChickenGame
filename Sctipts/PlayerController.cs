using System.Collections;
using UnityEngine;
using Yavuz;

public class PlayerController : MonoBehaviour
{


    // Sesler 
    public AudioSource camurses;
    public AudioSource zararGormeSesi;
    public AudioSource tavukSes;
    public AudioSource baslangýctavukSes;

    bellekYonetimi bellek = new bellekYonetimi();
    public Animator KapýAnim;
    //public GameObject duman;
    public GameObject playcanva;

    public bool zemindemi;
    public Animator[] animator;
    private bool engelvar;

    public GameManager manager;
    public CameraController camera;
    public AnaMenuManager anaManu;

    public bool LevelSpawn;
    private bool start = false;



    [Header("player control")]
    private Vector2 dragStartPosition;
    private bool isTouching = false;
    public float dragSensitivity = 2.0f;

    public float originalHareket = 5f;
    public float hareket = 0f;

    public GameObject oyuniçi;

    public bool isGrounded;

    private BoxCollider boxCollider;
    private Rigidbody rb;

    private void Awake()
    {
        tavukSes.volume = PlayerPrefs.GetFloat("OyunSes");
        baslangýctavukSes.volume = PlayerPrefs.GetFloat("OyunSes");
        zararGormeSesi.volume = PlayerPrefs.GetFloat("OyunSes");
        camurses.volume = PlayerPrefs.GetFloat("OyunSes");

        engelvar = false;
        originalHareket = 5f;
        //duman.SetActive(false);
        start = false;
        KapýAnim.SetBool("KapýAcik", false);
    }

    private void Start()
    {



        originalHareket = hareket;

        anaManu = FindObjectOfType<AnaMenuManager>();
        camera = FindObjectOfType<CameraController>();
        manager = FindObjectOfType<GameManager>();

        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();



    }

    private void FixedUpdate()
    {


        if (start)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTouching = true;
                rb.velocity = new Vector3(dragSensitivity, 0f, 0f);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isTouching = false;
                rb.velocity = Vector3.zero;
            }

            if (isTouching)
            {
                if (Input.mousePosition.x > Screen.width / 2)
                {

                    MoveCharacter(1.0f);
                }
                else
                {
                    MoveCharacter(-1.0f);
                }
            }
            originalHareket = Mathf.Min(originalHareket + 0.01f * Time.deltaTime);
            IleriHareket();
        }
    }

    private void MoveCharacter(float xDirection)
    {
        Vector3 movement = new Vector3(xDirection * dragSensitivity / 5, 0f, 0f);
        rb.AddForce(movement, ForceMode.VelocityChange);
    }

    public void BaslaKosmaya()
    {
        start = true;
        baslangýctavukSes.Stop();
        tavukSes.Play();
        Debug.Log(start);
        Time.timeScale = 1;
        oyuniçi.SetActive(true);
        playcanva.SetActive(false);
        manager.gameObject.SetActive(true);
        KapýAnim.SetBool("KapýAcik", true);

        camera.cameradegis();


        originalHareket = 5f;
        hareket = 5f; // Hýzý baþlangýç deðerine sýfýrla

        // duman.SetActive(true);
    }

    public void IleriHareket()
    {

        animator[bellek.VeriOku_i("AktifKarakter")].SetFloat("hareket", Mathf.Abs(hareket));
        float forwardMovement = hareket * Time.deltaTime;
        transform.Translate(Vector3.forward * forwardMovement);
        if (!engelvar)
        {
            hareket = Mathf.Min(hareket + 0.01f * Time.deltaTime);
        }
    }

    public void DevredDisiBirakma()
    {
        oyuniçi.SetActive(false);
        if (rb != null)
            rb.isKinematic = true; // Rigidbody'ýn fiziksel etkileþime izin verilmediðinden emin olun

        if (boxCollider != null)
            boxCollider.enabled = false;
    }

    public void AktifEtme()
    {
        if (rb != null)
            rb.isKinematic = false; // Rigidbody'ýn fiziksel etkileþime izin verilmediðinden emin olun

        if (boxCollider != null)
            boxCollider.enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        animator[bellek.VeriOku_i("AktifKarakter")].SetBool("zemindemi", true);




        if (collision.gameObject.CompareTag("Kartal"))
        {
            manager.CanAzalt();
            manager.CivcivOlme();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            DevredDisiBirakma();
            manager.PlayerDied();
            manager.data.lives = 0;
            animator[bellek.VeriOku_i("AktifKarakter")].SetTrigger("Oldu");
            manager.CivcivOlme();
            hareket = 0;
            zararGormeSesi.Play();
        }



        // Zeminle temas kontrolü
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Engel"))
        {

            engelvar = true;
            animator[bellek.VeriOku_i("AktifKarakter")].SetFloat("hareket", Mathf.Abs(hareket));
            hareket = 0;


        }
    }
    private void OnCollisionExit(Collision collision)
    {


        if (collision.gameObject.CompareTag("Engel"))
        {
            hareket = originalHareket;
            engelvar = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            ResetIsGrounded();

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Can"))
        {

            manager.CivcivDogma();

            manager.PlayerHealtUp();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            manager.data.coin++; // Coin toplandýðýnda coin sayýsýný artýr
            manager.CollectCoin();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Misir"))
        {
            originalHareket += 0.5f;
            hareket += 0.5f;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Camur"))
        {
            animator[bellek.VeriOku_i("AktifKarakter")].SetFloat("hareket", Mathf.Abs(hareket));
            hareket = 4;
            isGrounded = false;
            camurses.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Camur"))
        {
            camurses.Stop();
            hareket = originalHareket;
            isGrounded = true;
        }
    }
    IEnumerator ResetIsGrounded()
    {
        yield return new WaitForSeconds(0.3f); // Biraz gecikme ekleyebilirsiniz, ihtiyaca göre deðiþtirebilirsiniz.

        if (!Physics.Raycast(transform.position, -Vector3.up, 0.1f))
        {
            isGrounded = false;

        }
    }
}
