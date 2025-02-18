using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clip-----")]

    public AudioClip backround;
    public AudioClip deadth;
    public AudioClip fireballhitEnemy;
    public AudioClip fireballFire;
    public AudioClip enemyDestroy;
    public AudioClip shipwhenHit;



    private void Start()
    {
       musicSource.clip = backround;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }    
}
