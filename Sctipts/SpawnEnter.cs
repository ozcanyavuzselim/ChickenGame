using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnter : MonoBehaviour
{
    public Transform[] ay�Spawner;
    public Transform[] yanay�Spawner;
    public GameObject ay�;
    public GameObject yanay�;
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
            for (int i = 0; i < ay�Spawner.Length; i++)
            {
                Instantiate(ay�, ay�Spawner[i].position, Quaternion.identity);

            }
            for (int i = 0; i < ay�Spawner.Length; i++)
            {

                Instantiate(yanay�, yanay�Spawner[i].position, Quaternion.identity);
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
