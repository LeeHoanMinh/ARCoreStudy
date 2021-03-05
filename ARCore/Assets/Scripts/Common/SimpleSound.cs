using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSound : MonoBehaviour
{
    public static SimpleSound instance;
    public AudioSource musicSource, soundSource, soundSource2;
    public AudioClip music, sound, button, enemyShoot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
        soundSource.clip = sound;
    }

    public void PlaySound()
    {
        if (!soundSource.isPlaying)
        {
            soundSource.clip = sound;
            soundSource.Play();
        }
    }

    public void PlayEnemyShoot()
    {
        if (!soundSource2.isPlaying)
        {
            soundSource2.clip = enemyShoot;
            soundSource2.Play();
        }
    }

    public void PlayButton()
    {
        soundSource.clip = button;
        soundSource.Play();
    }

}
