using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup frontimage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image icon;
    // Start is called before the first frame update
    void Start()
    {
        //frontimage.DOFade(0.0f, 3f);
    }

    public void Fade(float targetAlpha, float time)
    {
        frontimage.DOFade(targetAlpha, time);
    }
    
    public void Text(string newstring)
    {
        text.text = newstring;
    }

    public void IconFade(float targetAlpha, float time)
    {
        icon.DOFade(targetAlpha, time);
    }
    
    public void ChangeIcon(string iconName)
    {
        icon.GetComponent<Animator>().SetTrigger("iconName");
    }
}