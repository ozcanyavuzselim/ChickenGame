using UnityEngine;

public class EnterLevel : MonoBehaviour
{
    public LevelController lc;
    public bool enter;

    private void Awake()
    {

        enter = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enter)
            {
                lc.playerEnter = true;
            }
            else
            {
                lc.playerExit = false;

            }
        }

    }
}
