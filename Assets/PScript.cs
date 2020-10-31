using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PScript : MonoBehaviour
{
    private bool isMoving;
    [SerializeField] private AudioSource walkingAudio;
    private float targetWalkingVolume;
    [SerializeField] private float walkingSoundValume = 0.8f;
    private Vector3 lastPosition;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private Timeline timeline;
    [SerializeField] GameObject audiosourcePrefab;
    private Vector3 moveTarget, moveOrigin;
    private float moveLerp, moveTime, moveTimePass;

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

    public void OpenDoor(bool closeLater)
    {
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            if (Vector2.Distance(new Vector2(door.transform.position.x, door.transform.position.z), new Vector2(transform.position.x, transform.position.z)) < 1.5f)
            {
                if (!closeLater)
                {
                    door.GetComponent<Door>().Open(true);
                }
                else
                {
                    door.GetComponent<Door>().OpenAndClose(2f, true);
                }
            }
        }
    }

    public void CloseDoor(bool lockTheDoor)
    {
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            if (Vector2.Distance(new Vector2(door.transform.position.x, door.transform.position.z), new Vector2(transform.position.x, transform.position.z)) < 3.5f)
            {
                 door.GetComponent<Door>().Close(lockTheDoor);
            }
        }
    }
}