using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnter : MonoBehaviour
{
    public Transform[] ayýSpawner;
    public Transform[] yanayýSpawner;
    public GameObject ayý;
    public GameObject yanayý;
    public bool Spawn;
    private void Awake()
    {
        Spawn = false;
    }
    void Start()
    {
        // Spawn
        if (!Spawn)
        {
            // Kartal Spawn
            for (int i = 0; i < ayýSpawner.Length; i++)
            {
                Instantiate(ayý, ayýSpawner[i].position, Quaternion.identity);

            }
            for (int i = 0; i < ayýSpawner.Length; i++)
            {

                Instantiate(yanayý, yanayýSpawner[i].position, Quaternion.identity);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Spawn = false;
        if (other.CompareTag("Player"))
        {
            Spawn = true;
        }
        
    }
}
