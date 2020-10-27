using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameSubMenu : MonoBehaviour
{
    private CanvasGroup cnavasGroup;
    private bool menuEnabled;
    [SerializeField] private CanvasGroup frontimage;

    private void Start()
    {
        cnavasGroup = GetComponent<CanvasGroup>();
        cnavasGroup.interactable = false;
        menuEnabled = false;
        SetCursorLock(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!menuEnabled)
            {
                SetCursorLock(true);

                cnavasGroup.DOFade(0.0f, 0.5f);
                cnavasGroup.interactable = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuEnabled)
            {
                SetCursorLock(true);

                cnavasGroup.DOFade(0.0f, 0.5f);
                cnavasGroup.interactable = false;

                cnavasGroup.GetComponent<RectTransform>().DOAnchorPosX(370f, 0.5f, false);
            }
            else
            {
                SetCursorLock(false);

                cnavasGroup.DOFade(1.0f, 0.5f);
                cnavasGroup.interactable = true;

                cnavasGroup.GetComponent<RectTransform>().DOAnchorPosX(960f, 0.5f, false);
            }
            menuEnabled = !menuEnabled;
        }
    }
    
    // Sets the cursor lock for first-person control.
    private void SetCursorLock(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void BackToMenu()
    {
        frontimage.DOFade(1.0f, 2.0f);
        StartCoroutine(ChangeSceneWithDelay(3f));
    }
    
    IEnumerator ChangeSceneWithDelay(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
