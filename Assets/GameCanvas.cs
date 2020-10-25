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
        frontimage.DOFade(0.0f, 1.5f);
    }
    
}
