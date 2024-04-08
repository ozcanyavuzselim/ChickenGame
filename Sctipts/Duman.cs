using UnityEngine;

public class Duman : MonoBehaviour
{
    public float hareket = 1f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // dumanýn ileri yönde sabit hareketi
        hareket += 0.01f * Time.deltaTime;
        float forwardMovement = hareket * Time.deltaTime;
        rb.MovePosition(transform.position + Vector3.forward * forwardMovement);
    }

}
