using UnityEngine;

public class Countdown2 : MonoBehaviour
{
    private SpriteRenderer gameobject;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SlowTimeTimer()
    {
        gameobject = GetComponent<SpriteRenderer>();
        gameobject.enabled = true;
        Invoke("ExpireTimer", 10f);
        anim.SetBool("SlowTimeTimer", true);

    }

    private void ExpireTimer()
    {
        gameobject.enabled = false;
        anim.SetBool("SlowTimeTimer", false);
    }
}