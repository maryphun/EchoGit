using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup frontimage;
    // Start is called before the first frame update
    void Start()
    {
        //frontimage.DOFade(0.0f, 3f);
    }

    void FadeIn(float time)
    {
        frontimage.DOFade(0.0f, time);
    }

    void FadeOut(float time)
    {
        frontimage.DOFade(1.0f, time);
    }
}
