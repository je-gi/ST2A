using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip soundClip; 
    private AudioSource audioSource; 
    private bool hasPlayed = false; 

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.clip = soundClip; 
    }

    public void PlaySound()
    {
        if (!hasPlayed && soundClip != null) 
        {
            audioSource.Play(); 
            hasPlayed = true; 
        }
    }

    public void ResetSound()
    {
        hasPlayed = false; 
    }
}
