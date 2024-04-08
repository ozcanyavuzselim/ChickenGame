using UnityEngine;

public class YokEdici : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        // Dokunan objenin etiketini alýn.
        string collidedObjectTag = collision.gameObject.tag;

        // Eðer dokunan objenin etiketi hedef etiketle eþleþiyorsa, objeyi yok et.
        if (collidedObjectTag == "Untagged")
        {
            Destroy(collision.gameObject);
        }
    }
}
