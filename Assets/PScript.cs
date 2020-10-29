using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private Timeline timeline;
    [SerializeField] GameObject audiosourcePrefab;
    
    public float PlayClip(int index)
    {
        //audiosource.clip = clips[index];
        //audiosource.Play();

        PlaySound(clips[index], 1f);

        return clips[index].length;
    }

    public float PlayClip(int index, float inTime)
    {
        var component = PlaySound(clips[index], 0f);
        component.DOFade(1.0f, inTime);
        
        return clips[index].length;
    }

    public void FadeAudioSource(float targetValue, float time)
    {
        var components = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource component in components)
        {
            if (component.gameObject.tag != "Foot")
            {
                component.DOFade(targetValue, time);
            }
        }
    }

    public void FadeAudioSource(float originValue, float targetValue, float time)
    {
        var components = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource component in components)
        {
            if (component.gameObject.tag != "Foot")
            {
                component.volume = originValue;
                component.DOFade(targetValue, time);
            }
        }
    }

    public float PlaySoundEffect(int clip, float volume, float end, float fadeoutTime)
    {
        //Open chest
        return PlaySound(clips[clip], volume, transform.position, end, fadeoutTime);
    }

    private float PlaySound(AudioClip clip, float volume, Vector3 target, float end, float fadeoutTime)
    {
        var source = Instantiate(audiosourcePrefab, target, Quaternion.identity);
        var component = source.GetComponent<AudioSource>();
        component.clip = clip;
        component.volume = volume;
        component.Play();

        StartCoroutine(FadeAfterTime(end, fadeoutTime, component));
        return clip.length;
    }

    private IEnumerator FadeAfterTime(float end, float time, AudioSource target)
    {
        yield return new WaitForSeconds(end);

        target.DOFade(0.0f, time);

        Destroy(target.gameObject, time);
    }

    private AudioSource PlaySound(AudioClip clip, float volume)
    {
        var source = Instantiate(audiosourcePrefab, transform);
        var component = source.GetComponent<AudioSource>();
        component.clip = clip;
        component.volume = volume;
        component.Play();

        Destroy(source, clip.length);
        return component;
    }
}