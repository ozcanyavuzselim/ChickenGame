using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletLevel : MonoBehaviour
{
    public GameObject level;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(level);
        }

    }
}
