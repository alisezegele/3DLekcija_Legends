using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource sfxSource, ambienceSource;
    [SerializeField] private AudioClip levelUpSound, playerHitSound, enemyHitSound, gameOverSound, winSound, forestSound;

    private void Start()
    {
        PlayAmbience();
    }

    public void PlayAmbience()
    {
        if (ambienceSource != null && forestSound != null)
        {
            ambienceSource.clip = forestSound;
            ambienceSource.loop = true;
            ambienceSource.volume = 0.5f;
            ambienceSource.Play();
        } 
    }
    public void StopAmbience()
    {
        if (ambienceSource != null && ambienceSource.isPlaying)
        {
            ambienceSource.Stop();
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlayLevelUp()
    {
        sfxSource.PlayOneShot(levelUpSound);
    }

    public void PlayPlayerHit()
    {
        sfxSource.PlayOneShot(playerHitSound);
    }

    public void PlayEnemyHit()
    {
        sfxSource.PlayOneShot(enemyHitSound);
    }

    public void PlayGameOver(float volume = 0.5f)
    {
        sfxSource.PlayOneShot(gameOverSound, volume);
    }

    public void PlayWinSound(float volume = 0.5f)
    {
        sfxSource.PlayOneShot(winSound, volume);
    }
}
