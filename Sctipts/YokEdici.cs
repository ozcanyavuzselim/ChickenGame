using UnityEngine;

public class YokEdici : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        // Dokunan objenin etiketini al�n.
        string collidedObjectTag = collision.gameObject.tag;

        // E�er dokunan objenin etiketi hedef etiketle e�le�iyorsa, objeyi yok et.
        if (collidedObjectTag == "Untagged")
        {
            Destroy(collision.gameObject);
        }
    }
}
