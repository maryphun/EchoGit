using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class controlUIcanvas : MonoBehaviour
{
    [SerializeField] private Canvas controlCheckCanvas;
    [SerializeField] private Canvas headphoneTestCanvas;
    [SerializeField] private AudioSource bgm;

    [SerializeField] private CanvasGroup transitionImage;

    public void OpenControlPanel()
    {
        StartCoroutine(Fade(true, controlCheckCanvas.gameObject, 0.35f));
    }

    public void CloseControlPanel()
    {
        StartCoroutine(Fade(false, controlCheckCanvas.gameObject, 0.35f));
    }
    
    public void OpenHeadphoneTestPanel()
    {
        StartCoroutine(Fade(true, headphoneTestCanvas.gameObject, 0.35f));
        bgm.DOFade(0.0f, 0.35f);
    }

    public void CloseHeadphoneTestPanel()
    {
        StartCoroutine(Fade(false, headphoneTestCanvas.gameObject, 0.35f));
        bgm.DOFade(1.0f, 0.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        transitionImage.DOFade(1.0f, 2.0f);
        bgm.DOFade(0.0f, 1.0f);
        StartCoroutine(ChangeSceneWithDelay(3f));
    }

    IEnumerator ChangeSceneWithDelay(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("ResonanceAudioDemo");
    }

    IEnumerator Fade(bool active, GameObject target, float time)
    {
        float targetalpha;
        if (active)
        {
            target.SetActive(active);
            targetalpha = 1.0f;
        }
        else
        {
            targetalpha = 0.0f;
        }

        CanvasGroup targetCanvas = target.GetComponent<CanvasGroup>();
        float originalalpha = targetCanvas.alpha;
        float lerptmp = 0.0f;

        while (targetCanvas.alpha != targetalpha)
        {
            lerptmp += 1.0f / time * Time.deltaTime;
            targetCanvas.alpha = Mathf.Lerp(originalalpha, targetalpha, lerptmp);
            yield return null;
        }
        
        targetCanvas.alpha = targetalpha;

        if (!active)
        {
            target.SetActive(active);
        }

        yield return 0; 
    }
}
