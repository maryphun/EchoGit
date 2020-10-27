using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private Timeline timeline;

    private AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public float PlayClip(int index)
    {
        audiosource.clip = clips[index];
        audiosource.Play();

        return audiosource.clip.length;
    }
}
