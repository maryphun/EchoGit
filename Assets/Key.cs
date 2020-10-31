using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool pickedUp = false;
    [SerializeField] private Transform player;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] GameObject audiosourcePrefab;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector2.Distance(new Vector2(player.position.x, player.position.z), new Vector2(transform.position.x, transform.position.z)) < 1.5f)
            {
                //get the key
                this.pickedUp = true;
                PlaySound(pickupSound, 1.0f);
            }
        }
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
