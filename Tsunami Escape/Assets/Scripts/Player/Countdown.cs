using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private SpriteRenderer gameobject;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void AntiGravTimer()
    {
        gameobject = GetComponent<SpriteRenderer>();
        gameobject.enabled = true;
        Invoke("ExpireTimer", 10f);
        anim.SetBool("AntiGravTimer", true);

    }

    private void ExpireTimer()
    {
        gameobject.enabled = false;
        anim.SetBool("AntiGravTimer", false);
    }
}
