using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Timeline : MonoBehaviour
{
    [Header("Default configuration")]
    [SerializeField] private GameCanvas gameCanvas;
    [SerializeField] private DemoPlayerController playerScript;
    [SerializeField] private Transform target;
    [SerializeField] private PScript P;
    [SerializeField] private JScript J;

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
            StartCoroutine(PartThree());
            part++;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DebugMode();
        }
    }

    public void PartDone(int part)
    {
        switch (part)
        {
            case 1:
                StartCoroutine(PartTwo());
                break;
            case 2:
                enableMovement = true;
                enableTarget = true;
                playerScript.SetEnableMovement(enableMovement);
                playerScript.SetEnableTarget(enableTarget);
                break;
            default:
                enableMovement = true;
                enableTarget = true;
                playerScript.SetEnableMovement(enableMovement);
                playerScript.SetEnableTarget(enableTarget);
                gameCanvas.Fade(0.0f, 0.2f);
                break;
        }
    }

    public IEnumerator PartOne()
    {
        bgmplayer.clip = bgm;
        bgmplayer.Play();

        //Graphic
        gameCanvas.Fade(0.0f, 2.0f);
        gameCanvas.Text("- Prologue -");
        //gameCanvas.IconFade(0.0f, 0f);
        gameCanvas.ChangeIcon("Book");
        //

        yield return new WaitForSeconds(3.0f);

        bgmplayer.DOFade(0.25f, 0.25f);

        timelineAudioSource.clip = voice;
        timelineAudioSource.dopplerLevel = 2.0f;
        timelineAudioSource.Play();


        yield return new WaitForSeconds(voice.length);

        bgmplayer.DOFade(1f, 0.25f);

        yield return new WaitForSeconds(56.8f - voice.length);

        bgmplayer.DOFade(0f, 0.2f);

        yield return new WaitForSeconds(0.2f);

        bgmplayer.Stop();
        bgmplayer.clip = null;

        //Graphic
        gameCanvas.Fade(1.0f, 1.5f);
        
        yield return new WaitForSeconds(2f);

        gameCanvas.Text("- No vision -");
        gameCanvas.ChangeIcon("Eye");
        gameCanvas.Fade(0.0f, 1.5f);
        playerScript.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);

        PartDone(1);
    }

    private IEnumerator PartTwo()
    {
        float waitTime = 0.0f;
        waitTime = P.PlayClip(0);
        P.FadeAudioSource(0.0f, 1.0f, 10f);

        yield return new WaitForSeconds(waitTime + 1.0f);

        waitTime = P.PlayClip(1);

        yield return new WaitForSeconds(waitTime + 2.0f);

        waitTime = P.PlayClip(2, 1);

        yield return new WaitForSeconds(waitTime + 4.0f);

        waitTime = P.PlayClip(3);

        yield return new WaitForSeconds(waitTime + 3.0f);

        waitTime = P.PlayClip(4);

        yield return new WaitForSeconds(waitTime + 5.0f);

        waitTime = P.PlayClip(5);

        yield return new WaitForSeconds(waitTime + 3.0f);

        waitTime = P.PlayClip(6);

        yield return new WaitForSeconds(waitTime + 3.0f);

        waitTime = P.PlayClip(7);

        yield return new WaitForSeconds(waitTime + 2.0f);

        waitTime = P.PlayClip(8);

        yield return new WaitForSeconds(waitTime + 2.0f);

        PartDone(2);
    }

    private IEnumerator PartThree()
    {
        float waitTime = 4.0f;
        J.Walk(new Vector3(5.94f, 1f, -0.02f), waitTime);

        yield return new WaitForSeconds(waitTime + 1.0f);

        J.OpenDoor();

        yield return new WaitForSeconds(2.0f);

        waitTime = 1.5f;
        J.Walk(new Vector3(2.82f, 1f, -0.02f), waitTime);
        
        yield return new WaitForSeconds(waitTime + 0.5f);

        waitTime = 3.5f;
        J.Walk(new Vector3(2.82f, 1f, 3.72f), waitTime);
        
        yield return new WaitForSeconds(waitTime / 2f);

        J.PlayClip(0);

        yield return new WaitForSeconds(waitTime / 2f);

        waitTime = 10f;
        J.Walk(new Vector3(-3.38f, 1f, 0.61f), waitTime);
        
        yield return new WaitForSeconds(waitTime + 1.5f);

        waitTime = 2f;
        J.Walk(new Vector3(-2.31f, 1f, -0.86f), waitTime);
        
        yield return new WaitForSeconds(5.5f);

        waitTime = P.PlayClip(9, 2f);

        yield return new WaitForSeconds(waitTime + 0.5f);

        waitTime = J.PlayClip(1);

        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(10, 2f);

        yield return new WaitForSeconds(waitTime);

        waitTime = J.PlayClip(2, 2f);
        
        yield return new WaitForSeconds(waitTime + 2f);
        
        waitTime = P.PlayClip(11, 2f);

        yield return new WaitForSeconds(waitTime + 1.5f);

        J.Walk(new Vector3(-0.44f, 1f, 1.84f), 3f);

        yield return new WaitForSeconds(1.5f);

        waitTime = J.PlayClip(3);
        
        yield return new WaitForSeconds(waitTime - 1.5f);

        J.Walk(new Vector3(-0.44f, 1f, 1.84f), 3f);

        PartDone(3);
    }

    private void DebugMode()
    {
        PartDone(9999);
    }
} 