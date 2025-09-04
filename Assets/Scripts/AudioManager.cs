using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAS;
    public AudioSource sfxAS;

    [Header("Music")]
    public AudioClip jungleMusic;
    public AudioClip combatMusic;

    [Header("SFX")]
    public AudioClip swordHit;
    public AudioClip brightSound;
    public AudioClip chestOpen;
    public AudioClip moneyBag;

    public void PlaySFX (AudioClip soundClip)
    {
        sfxAS.PlayOneShot(soundClip);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicAS.Stop();
        musicAS.clip = musicClip;
        musicAS.Play();
    }
}
