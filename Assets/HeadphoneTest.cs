using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeadphoneTest : MonoBehaviour
{
    [SerializeField] private RectTransform soundSourceUI;
    [SerializeField] private RectTransform youUI;

    [SerializeField] private GameObject audiosourceCube;
    [SerializeField] private GameObject youCube;

    float angle;
    float speed; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius;
    float slideRadius;

    private void Start()
    {
        angle = 0;
        speed = (2 * Mathf.PI) / 12;
        radius = 200;
        slideRadius = 200;
    }

    private void Update()
    {
        radius = Mathf.MoveTowards(radius, slideRadius, 0.5f);

        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        soundSourceUI.DOAnchorPosX((Mathf.Cos(angle) * radius) + youUI.anchoredPosition.x, 0.0f, false);
        soundSourceUI.DOAnchorPosY((Mathf.Sin(angle) * radius) + youUI.anchoredPosition.y, 0.0f, false);
        
        audiosourceCube.transform.DOMoveX((Mathf.Cos(angle) * radius / 10) + youCube.transform.position.x, 0.0f, false);
        audiosourceCube.transform.DOMoveZ((Mathf.Sin(angle) * radius / 10) + youCube.transform.position.z, 0.0f, false);
    }

    private void OnEnable()
    {
        audiosourceCube.GetComponent<AudioSource>().volume = 0.0f;
        audiosourceCube.GetComponent<AudioSource>().DOFade(1.0f, 0.5f);
        Debug.Log("play");
    }

    private void OnDisable()
    {
        audiosourceCube.GetComponent<AudioSource>().volume = 1.0f;
        audiosourceCube.GetComponent<AudioSource>().DOFade(0.0f, 0.5f);
    }

    public void OnSliderValueChanged(float distance)
    {
        slideRadius = 300 * distance;
    }
}
