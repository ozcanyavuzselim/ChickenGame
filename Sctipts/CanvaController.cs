using UnityEngine;

public class CanvaController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController playercontroller;

    public void Basla()
    {
        playercontroller.hareket = 5;
    }
}
