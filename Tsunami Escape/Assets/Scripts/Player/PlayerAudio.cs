using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip footstep1;
    public AudioClip[] jumpClips;
public void Footsteps()
    {
        audiosource.PlayOneShot(footstep1);
    }

public void Jump()
    {
        AudioClip randomJump = jumpClips[Random.Range(0, jumpClips.Length)];
        audiosource.PlayOneShot(randomJump);
    }
}
