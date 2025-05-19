using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SFXAudio;

    public AudioClip Jump;
    public AudioClip Attack;
    public AudioClip Fall;
    public AudioClip batDeath;
    public AudioClip batImpact;
    public AudioClip footsteps;
    public AudioClip damaged;

    public void PlaySFX(AudioClip clip, float minPitch = 1f, float maxPitch = 1f)
    {
        SFXAudio.pitch = Random.Range(minPitch, maxPitch);
        SFXAudio.PlayOneShot(clip);
        SFXAudio.pitch = 1f; // Reinicia el pitch a su valor original por seguridad
    }

}
