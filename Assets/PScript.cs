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

    public IEnumerator PartOne()
    {
        audiosource.clip = clips[0];
        audiosource.Play();

        yield return new WaitForSeconds(clips[0].length + 1.0f);

        audiosource.clip = clips[1];
        audiosource.Play();

        yield return new WaitForSeconds(clips[1].length + 2.0f);

        audiosource.clip = clips[2];
        audiosource.Play();

        yield return new WaitForSeconds(clips[2].length + 4.0f);

        audiosource.clip = clips[3];
        audiosource.Play();
        
        yield return new WaitForSeconds(clips[3].length + 3.0f);

        audiosource.clip = clips[4];
        audiosource.Play();
        
        yield return new WaitForSeconds(clips[4].length + 5.0f);

        audiosource.clip = clips[5];
        audiosource.Play();
        
        yield return new WaitForSeconds(clips[5].length + 3.0f);

        audiosource.clip = clips[6];
        audiosource.Play();
        
        yield return new WaitForSeconds(clips[6].length + 3.0f);

        audiosource.clip = clips[7];
        audiosource.Play();
        
        yield return new WaitForSeconds(clips[7].length + 2.0f);

        audiosource.clip = clips[8];
        audiosource.Play();
        
        yield return new WaitForSeconds(clips[8].length + 2.0f);

        timeline.PartDone(1);
    }
}
