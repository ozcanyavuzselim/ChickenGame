using UnityEngine;

public class LevelController : MonoBehaviour
{
    public bool playerEnter, playerExit;

    // Kartal Spawn
    public bool Spawn;
    public float pozisyonx = 7.9f;

    // Level Spawn
    public GameObject[] levels;
 

    private void Awake()
    {
        Spawn = false;
        playerEnter = false;


    }

    private void Update()
    {
        // Spawn
        if (!Spawn)
        {
            EnterLevel();
        }
    }

    void EnterLevel()
    {
        if (playerEnter)
        {
            
            if (levels.Length > 0)
            {
                ActivateRandomLevel();
                
                Spawn = true;
            }
            else
            {
                Debug.LogWarning("No levels available to spawn.");
            }
        }
    }

    private void ActivateRandomLevel()
    {
        
        if (levels.Length > 0)
        {
            // Rastgele bir seviye se�in
            int randomIndex = Random.Range(0, levels.Length);

            // Son etkinle�tirilen seviyenin pozisyonunu kullanarak yeni seviyeyi yerle�tirin
            Vector3 newPosition = new Vector3(pozisyonx, transform.position.y, transform.position.z + 199f);

            // Seviyeyi etkinle�tirin ve pozisyonunu ayarlay�n
            levels[randomIndex].SetActive(true);
            levels[randomIndex].transform.position = newPosition;

        }
    }
   
}