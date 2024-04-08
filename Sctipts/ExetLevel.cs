using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExetLevel : MonoBehaviour
{

    public GameObject Level;
    public GameObject[] coins;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateGameObjects();
        }

    }
    private void ActivateGameObjects()
    {
        // Add code here to set the GameObject instances active
        // For example:
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }
    }
}
