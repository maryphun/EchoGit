using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Timeline : MonoBehaviour
{
    [Header("Default configuration")]
    [SerializeField] private DemoPlayerController playerScript;
    [SerializeField] private Transform target;
    [SerializeField] private PScript P;
    //[SerializeField] private JScript J;

    [Header("Variable")]
    [SerializeField] private bool enableMovement;
    [SerializeField] private bool enableTarget;

    [SerializeField] private float clock;
    [SerializeField] private int part;

    [Header("Part one audio clips")]
    [SerializeField] private AudioSource bgmplayer;
    [SerializeField] private AudioClip bgm, voice;

    private AudioSource timelineAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerScript.SetEnableMovement(enableMovement);
        playerScript.SetEnableTarget(enableTarget);
        target.GetComponent<AudioSource>().volume = 0.0f;
        target.GetComponent<AudioSource>().Play();
        timelineAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        clock += Time.deltaTime;

        if (clock > 1.0f && part == 0)
        {
            StartCoroutine(PartOne());
            part++;
        }
    }

    public void PartDone(int part)
    {
        switch (part)
        {
            case 1:
                enableMovement = true;
                enableTarget = true;
                playerScript.SetEnableMovement(enableMovement);
                playerScript.SetEnableTarget(enableTarget);
                break;
            default:
                break;
        }
    }

    public IEnumerator PartOne()
    {
        bgmplayer.clip = bgm;
        bgmplayer.Play();

        yield return new WaitForSeconds(3.0f);

        bgmplayer.DOFade(0.25f, 0.25f);

        timelineAudioSource.clip = voice;
        timelineAudioSource.dopplerLevel = 2.0f;
        timelineAudioSource.Play();


        yield return new WaitForSeconds(voice.length);

        bgmplayer.DOFade(1f, 0.25f);

        yield return new WaitForSeconds(56.0f - voice.length);

        bgmplayer.DOFade(0f, 0.85f);

        yield return new WaitForSeconds(1f);

        bgmplayer.Stop();
        bgmplayer.clip = null;
    }
} 