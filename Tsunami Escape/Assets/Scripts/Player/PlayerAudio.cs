using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip footstep1;
public void footsteps()
    {
        audiosource.PlayOneShot(footstep1);
    }
}
