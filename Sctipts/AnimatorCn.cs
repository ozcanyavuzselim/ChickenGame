using UnityEngine;

public class AnimatorCn : MonoBehaviour
{
    public Animator kaydetAnim;

    public void KendiniPasiflestir()
    {
        kaydetAnim.SetBool("ok", false);
    }
}
