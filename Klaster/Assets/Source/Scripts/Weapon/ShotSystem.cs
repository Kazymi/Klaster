using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSystem : MonoBehaviour
{
    public event Action Shooted;

    public void Shot()
    {
        Shooted?.Invoke();
    }
}

[RequireComponent(typeof(ShotSystem))]
public class WeaponSoundSystem : MonoBehaviour
{
    [SerializeField] private AudioClip shotClip;
    [SerializeField] private AudioSource audioSource;

    private ShotSystem _system;

    private void Awake()
    {
        _system = GetComponent<ShotSystem>();
        _system.Shooted += PlayShot;
    }

    private void PlayShot()
    {
        audioSource.PlayOneShot(shotClip);
    }
}