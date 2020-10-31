using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    [Header("Part Five Reference")]
    [SerializeField] private Door unlockingDoor;
    [SerializeField] private Key pickup;

    [Header("Part Seven Reference")]
    [SerializeField] private Door closeDoor;
    [SerializeField] private Door finalJUnlockDoor;
    [SerializeField] private Door finalDoorUnlock;

    [Header("Game End")]
    [SerializeField] private AudioClip paku;

    private AudioSource timelineAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerScript.SetEnableMovement(enableMovement);
        playerScript.SetEnableTarget(enableTarget);
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
                gameCanvas.EnableCompass(true);
                break;
            case 2:
                enableMovement = true;
                enableTarget = true;
                break;
            case 3:
                target.position = new Vector3(-2.59f, 1.5f, 0f);
                gameCanvas.Text("- 回去原位 -");
                playerScript.RequestMoveToTarget(4);
                break;
            case 4:
                enableMovement = false;
                enableTarget = false;
                StartCoroutine(PartFour());
                break;
            case 5:
                enableMovement = true;
                enableTarget = true;
                target.position = new Vector3(0f, 1.5f, 6.61f);
                StartCoroutine(PartFive());
                break;
            case 6:
                target.position = new Vector3(20.82f, 2.5f, -19.62f);
                StartCoroutine(PartSeven());
                break;
            case 7:
                StartCoroutine(PartNine());
                break;
            default:
                enableMovement = true;
                enableTarget = true;
                gameCanvas.Fade(0.0f, 0.2f);
                break;
        }

        playerScript.SetEnableMovement(enableMovement);
        playerScript.SetEnableTarget(enableTarget);
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

        yield return new WaitForSeconds(waitTime + 0.5f);


        gameCanvas.Text("- [F]键 使用麦克风回应 -");
        while (!Input.GetKeyDown(KeyCode.F))
        {
            yield return null;
        }

        yield return new WaitForSeconds(waitTime + 1.5f);
        gameCanvas.Text("- No vision -");

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

        gameCanvas.Text("- [空格键]获取前进方向 -");

        PartDone(2);
    }

    private IEnumerator PartFour()
    {
        gameCanvas.Text("- No vision -");

        float waitTime = 4.0f;
        J.Walk(new Vector3(5.94f, 1f, -0.02f), waitTime);

        yield return new WaitForSeconds(waitTime + 1.0f);

        J.OpenDoor(true);

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
        
        yield return new WaitForSeconds(waitTime - 2.5f);
        
        waitTime = P.PlayClip(11, 2f);

        yield return new WaitForSeconds(waitTime + 0.5f);

        waitTime = J.PlayClip(4);
        
        yield return new WaitForSeconds(waitTime - 3f);

        waitTime = 4.5f;
        J.Walk(new Vector3(-0.44f, 1f, 1.84f), waitTime);

        yield return new WaitForSeconds(waitTime - 0.5f);

        waitTime = J.OpenChestSound();

        yield return new WaitForSeconds(waitTime - 1.0f);

        waitTime = P.PlayClip(12, 2f);
        
        yield return new WaitForSeconds(waitTime * 0.75f);

        waitTime = P.PlaySoundEffect(36, 1.0f, 2f, 1f);

        yield return new WaitForSeconds(waitTime + 1.5f);

        //laugh
        J.PlayClip(8, 0.5f, 1f);

        waitTime = 2.0f;
        J.Walk(new Vector3(3.23f, 1f, 1.84f), waitTime);

        yield return new WaitForSeconds(1.2f);

        J.PlayClip(6);

        yield return new WaitForSeconds(waitTime - 1.2f);

        waitTime = 1.0f;
        J.Walk(new Vector3(4f, 1f, 0f), waitTime);
        yield return new WaitForSeconds(waitTime);

        waitTime = 1.0f;
        J.OpenDoor(true);
        yield return new WaitForSeconds(waitTime);
        
        waitTime = 4.0f;
        J.Walk(new Vector3(13f, 1f, 0f), waitTime);
        yield return new WaitForSeconds(waitTime);

        //laugh
        waitTime = J.PlayClip(7);
        yield return new WaitForSeconds(waitTime+0.2f);

        waitTime = P.PlayClip(13, 1f);
        yield return new WaitForSeconds(waitTime+ 1f);

        waitTime = P.PlayClip(14, 1f);

        gameCanvas.Text("- Find the way out -");

        PartDone(5);
    }

    private IEnumerator PartFive()
    {
        unlockingDoor.UnlockWithoutSE();
        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.90f)
        {
            yield return null;
        }
        
        float waitTime = P.PlayClip(15, 1f);

        yield return new WaitForSeconds(waitTime);
        
        target.position = new Vector3(0f, 1.5f, 13f);
        
        gameCanvas.Text("- No vision -");
        
        waitTime = P.PlayClip(17);

        yield return new WaitForSeconds(waitTime);

        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.60f)
        {
            yield return null;
        }

        P.PlayClip(18);

        gameCanvas.ChangeIcon("Key");
        gameCanvas.Text("- Find the key -");

        target.position = new Vector3(pickup.transform.position.x, 1.5f, pickup.transform.position.z);

        while (!pickup.pickedUp)
        {
            yield return null;
        }

        waitTime = 3f;

        gameCanvas.ChangeIcon("Eye");
        gameCanvas.Text("- Return -");
        target.position = new Vector3(P.transform.position.x, 1.5f, P.transform.position.z);

        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(19);
        float timecnt = 0.0f;

        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.60f)
        {
            timecnt += Time.deltaTime;
            if (timecnt >= 8.0f)
            {
                timecnt = 0.0f;
                P.PlayClip(Random.Range(19, 20));
            }
            yield return null;
        }
        
        waitTime = P.PlayClip(37);

        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(21);
        
        yield return new WaitForSeconds(waitTime);

        gameCanvas.Text("- Follow -");
        // P start to walk
        waitTime = 4f;
        P.Walk(new Vector3(-2.2f, 1.0f, 4.22f), waitTime);
        target.SetParent(P.transform, true);
        yield return new WaitForSeconds(waitTime);

        waitTime = 1.5f;
        P.Walk(new Vector3(0.49f, 1.0f, 4.22f), waitTime);
        yield return new WaitForSeconds(waitTime);

        waitTime = 1.5f;
        P.Walk(new Vector3(0.49f, 1.0f, 12.81f), waitTime);
        yield return new WaitForSeconds(waitTime);

        timecnt = 0.0f;
        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.60f)
        {
            timecnt += Time.deltaTime;
            if (timecnt >= 8.0f)
            {
                timecnt = 0.0f;
                P.PlayClip(22);
            }
            yield return null;
        }

        waitTime = P.PlayClip(23);
        yield return new WaitForSeconds(waitTime);

        waitTime = 3f;
        P.Walk(new Vector3(-3.58f, 1.0f, 7.9f), waitTime);
        yield return new WaitForSeconds(waitTime);

        // sound for news paper, play 2 times in row.
        waitTime = P.PlayClip(38);
        yield return new WaitForSeconds(waitTime);
        waitTime = P.PlayClip(38);
        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(24);
        yield return new WaitForSeconds(waitTime + 1.5f);

        waitTime = 1.5f;
        P.Walk(new Vector3(-4.37f, 1.0f, 12.91f), waitTime);
        yield return new WaitForSeconds(waitTime);

        P.OpenDoor(false);
        yield return new WaitForSeconds(1f);
        
        waitTime = 1.5f;
        P.Walk(new Vector3(-10.35f, 1.0f, 12.91f), waitTime);
        yield return new WaitForSeconds(waitTime/2);

        waitTime = J.PlayClip(10);
        yield return new WaitForSeconds(waitTime);
        waitTime = J.PlayClip(11);
        yield return new WaitForSeconds(waitTime);
        waitTime = J.PlayClip(12);
        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(25);
        yield return new WaitForSeconds(waitTime);

        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.60f)
        {
            yield return null;
        }
        
        waitTime = 1.5f;
        P.Walk(new Vector3(-14.5f, 1.0f, 13f), waitTime);
        yield return new WaitForSeconds(waitTime);

        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.60f)
        {
            yield return null;
        }

        waitTime = 6f;
        P.Walk(new Vector3(-14.5f, 1.0f, -7f), waitTime);
        yield return new WaitForSeconds(waitTime);
        
        waitTime = 5.5f;
        P.Walk(new Vector3(-25f, 1.0f, -7f), waitTime);
        yield return new WaitForSeconds(waitTime);

        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.60f)
        {
            yield return null;
        }
        
        waitTime = 2.5f;
        P.Walk(new Vector3(-25f, 1.0f, -13.05f), waitTime);
        yield return new WaitForSeconds(waitTime);

        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.60f)
        {
            yield return null;
        }

        P.OpenDoor(false);
        yield return new WaitForSeconds(1f);

        waitTime = 2.5f;
        P.Walk(new Vector3(-25f, 1.0f, -17.05f), waitTime);
        yield return new WaitForSeconds(waitTime);

        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.60f)
        {
            yield return null;
        }

        waitTime = P.PlayClip(26);
        yield return new WaitForSeconds(waitTime + 2f);
        
        waitTime = P.PlayClip(27);
        yield return new WaitForSeconds(waitTime + 1f);

        waitTime = 3f;
        P.Walk(new Vector3(-25f, 1.0f, -14.5f), waitTime);
        yield return new WaitForSeconds(waitTime);

        waitTime = 2f;
        J.Walk(new Vector3(-15f, 1f, -7f), waitTime);

        P.CloseDoor(true);
        
        yield return new WaitForSeconds(waitTime);
        J.Walk(new Vector3(-25f, 1f, -12.61f), waitTime);

        waitTime = J.PlayClip(13);
        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(29);
        target.SetParent(null, true);
        yield return new WaitForSeconds(waitTime / 2);

        J.OpenDoor(false);

        yield return new WaitForSeconds(waitTime / 2);
        
        J.OpenDoor(false);

        waitTime = P.PlayClip(30);
        yield return new WaitForSeconds(waitTime / 2);

        J.OpenDoor(false);
        yield return new WaitForSeconds(waitTime / 2);
        
        waitTime = P.PlayClip(31);

        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(32);
        target.position = new Vector3(23.2f, 2.5f, -19.62f);

        gameCanvas.Text("- Hide -");
        
        waitTime = P.PlayClip(32);
    }

    private IEnumerator PartSeven()
    {
        while (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(playerScript.transform.position.x, playerScript.transform.position.z)) > 1.40f)
        {
            yield return null;
        }

        gameCanvas.Text("- No vision -");

        P.Walk(new Vector3(-25f, 1, -23f), 1.5f);

        finalJUnlockDoor.UnlockWithoutSE();

        J.OpenDoor(false);

        J.Walk(new Vector3(-25f, 1, -19.88f), 1.5f);

        closeDoor.Close(false);

        yield return new WaitForSeconds(3f);

        float waitTime = J.PlayClip(14);

        yield return new WaitForSeconds(waitTime - 2f);

        waitTime = P.PlayClip(33);

        yield return new WaitForSeconds(waitTime + 2f);

        waitTime = P.PlayClip(34);
        
        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(35);
        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(40);

        yield return new WaitForSeconds(waitTime + 1.5f);
        
        waitTime = P.PlayClip(40);
        
        yield return new WaitForSeconds(waitTime);

        waitTime = P.PlayClip(40);
        
        yield return new WaitForSeconds(waitTime- 1.5f);

        waitTime = P.PlayClip(40);

        yield return new WaitForSeconds(waitTime + 1.5f);

        waitTime = J.PlayClip(15);
        yield return new WaitForSeconds(waitTime);
        waitTime = J.PlayClip(16);

        target.position = new Vector3(-39.2f, 2.5f, -49.33f);
        
        J.Walk(new Vector3(-25f, 1, -14.43f), 2f);
        yield return new WaitForSeconds(2f);
        J.OpenDoor(true);
        yield return new WaitForSeconds(1f);
        J.Walk(new Vector3(-25f, 1, 4.16f), 10f);
        
        gameCanvas.Text("- Escape -");
        gameCanvas.ChangeIcon("RedEye");
        finalDoorUnlock.UnlockWithoutSE();
    }
    
    private IEnumerator PartNine()
    {
        bgmplayer.clip = paku;
        bgmplayer.DOFade(0.75f, 5f);
        bgmplayer.Play();

        gameCanvas.ChangeIcon("Smile");
        gameCanvas.Text("- Thanks for playing! -");
        yield return new WaitForSeconds(paku.length);

        SceneManager.LoadScene("SampleScene");
    }

    private void DebugMode()
    {
        PartDone(9999);
    }
} 