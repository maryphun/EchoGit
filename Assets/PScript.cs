using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public float PlayClip(int index, float inTime)
    {
        audiosource.volume = 0.0f;
        audiosource.DOFade(1.0f, inTime);

        audiosource.clip = clips[index];
        audiosource.Play();
        
        return audiosource.clip.length;
    }

    public void FadeAudioSource(float targetValue, float time)
    {
        audiosource.DOFade(targetValue, time);
    }


    public void FadeAudioSource(float originValue, float targetValue, float time)
    {
        audiosource.volume = originValue;
        audiosource.DOFade(targetValue, time);
    }
}