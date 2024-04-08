using UnityEngine;

public class KarTopuController : MonoBehaviour
{
    public float speed;
    public float scaleSpeed;
    public float maxScale = 5f; // Belirli bir b�y�kl�k s�n�r�

    private void Update()
    {
        // Kartopunun hareketi
        transform.position += Vector3.forward * -speed * Time.deltaTime;

        // Kartopunun b�y�kl��� artarken, maksimum b�y�kl��� a�mamas� i�in kontrol ekleniyor
        if (transform.localScale.x < maxScale && transform.localScale.y < maxScale && transform.localScale.z < maxScale)
        {
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }
}
