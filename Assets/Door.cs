using System.Collections;
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
    public void Open(bool ignoreLock)
    {
        if (!locked || ignoreLock)
        {
            transform.DOMoveY(transform.position.y + GetComponent<Collider>().bounds.extents.y, 1.0f, false);

            PlaySound(openDoor, 1.0f);

            if (GetComponent<PartSix>() != null)
            {
                GetComponent<PartSix>().Opened();
            }
            if (GetComponent<PartNine>() != null)
            {
                GetComponent<PartNine>().Opened();
            }
        }
        else
        {
            PlaySound(openLockedDoor, 0.4f);
        }
        
        if (GetComponent<PartOne>() != null)
        {
            GetComponent<PartOne>().DoorInteracted();
        }
    }

    public void Close(bool lockDoor)
    {
        transform.DOMoveY(transform.position.y - GetComponent<Collider>().bounds.extents.y, 1.0f, false);
        Debug.Log("close door");
        PlaySound(closeDoor, 1.0f);
        if (lockDoor) locked = true;
    }

    public void OpenAndClose(float delay, bool ignoreLock)
    {
        if (!locked || ignoreLock)
        {
            transform.DOMoveY(transform.position.y + GetComponent<Collider>().bounds.extents.y, 1.0f, false);

            PlaySound(openDoor, 1.0f);

            StartCoroutine(CloseAfterDelay(delay));
        }
        else
        {
            PlaySound(openLockedDoor, 0.4f);
        }
    }

    public void UnlockWithoutSE()
    {
        locked = false;
    }

    private IEnumerator CloseAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        transform.DOMoveY(transform.position.y - GetComponent<Collider>().bounds.extents.y, 1.0f, false);
        Debug.Log("close door");
        PlaySound(closeDoor, 1.0f);
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
