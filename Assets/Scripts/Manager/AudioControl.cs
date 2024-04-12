using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : SingletonMono<AudioControl>
{
    public AudioClip laserShoot1;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        laserShoot1.
    }
    public void LaserShoot()
    {
        audioSource.clip = laserShoot1;
        audioSource.Play();
    }
}
