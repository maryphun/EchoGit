using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JScript : MonoBehaviour
{
    private bool isMoving;
    [SerializeField] private AudioSource walkingAudio;
    private float targetWalkingVolume;
    [SerializeField] private float walkingSoundValume = 0.8f;
    private Vector3 lastPosition;

    [SerializeField] private AudioClip[] clips;
    [SerializeField] GameObject audiosourcePrefab;

    private AudioSource audiosource;

    private Vector3 moveTarget, moveOrigin;
    private float moveLerp, moveTime, moveTimePass;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isMoving)
        {
            moveTimePass += Time.deltaTime;
            moveLerp = 1.0f * (moveTimePass / moveTime);

            transform.DOMove(Vector3.Lerp(moveOrigin, moveTarget, moveLerp), Time.deltaTime, false);
            if (moveTimePass >= moveTime)
            {
                transform.position = moveTarget;
                isMoving = false;
            }
        }

        if (isMoving)
        {
            if (targetWalkingVolume == 0.0f)
            {
                targetWalkingVolume = walkingSoundValume;
            }
        }
        else
        {
            if (targetWalkingVolume == walkingSoundValume)
            {
                targetWalkingVolume = 0.0f;
            }
        }
        lastPosition = transform.position;

        //lerp audio to its target volume
        walkingAudio.volume = Mathf.MoveTowards(walkingAudio.volume, targetWalkingVolume, 5f * Time.deltaTime);
    }

    public void Walk(Vector3 targetPos, float time)
    {
        moveLerp = 0.0f;
        moveTime = time;
        moveTimePass = 0.0f;
        moveOrigin = transform.position;
        moveTarget = targetPos;
        isMoving = true;
    }

    public void OpenDoor(bool closeLater)
    {
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            if (Vector2.Distance(new Vector2(door.transform.position.x, door.transform.position.z), new Vector2(transform.position.x, transform.position.z)) < 1.5f)
            {
                if (!closeLater)
                {
                    door.GetComponent<Door>().Open();
                }
                else
                {
                    door.GetComponent<Door>().OpenAndClose(2f);
                }
            }
        }
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

    public float EventOne()
    {
        //Open chest
        return PlaySound(clips[4], 1.0f, new Vector3(1f, 0.5f, 0f));
    }

    private float PlaySound(AudioClip clip, float volume, Vector3 target)
    {
        var source = Instantiate(audiosourcePrefab, target, Quaternion.identity);
        var component = source.GetComponent<AudioSource>();
        component.clip = clip;
        component.volume = volume;
        component.Play();

        Destroy(source, clip.length);
        return clip.length;
    }
}
