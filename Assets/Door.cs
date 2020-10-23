﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip openLockedDoor;
    [SerializeField] private AudioClip closeDoor;
    [SerializeField] private AudioClip unlockDoor;
    [SerializeField] private GameObject audiosourceprefab;
    [SerializeField] private bool locked = false;

    // Start
    public void Open()
    {
        if (!locked)
        {
            transform.DOMoveY(transform.position.y + GetComponent<Collider>().bounds.extents.y, 1.0f, false);

            PlaySound(openDoor, 1.0f);
        }
        else
        {
            PlaySound(openLockedDoor, 0.4f);
        }
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        var source = Instantiate(audiosourceprefab, transform);
        var component = source.GetComponent<AudioSource>();
        component.clip = clip;
        component.volume = volume;
        component.Play();

        Destroy(source, clip.length);
    }
}
