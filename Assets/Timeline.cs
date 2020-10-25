using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    [Header("Default configuration")]
    [SerializeField] private DemoPlayerController playerScript;
    [SerializeField] private Transform target;
    [SerializeField] private PScript P;

    [Header("Variable")]
    [SerializeField] private bool enableMovement;
    [SerializeField] private bool enableTarget;

    [SerializeField] private float clock;
    [SerializeField] private int part;

    // Start is called before the first frame update
    void Start()
    {
        playerScript.SetEnableMovement(enableMovement);
        playerScript.SetEnableTarget(enableTarget);
        target.GetComponent<AudioSource>().volume = 0.0f;
        target.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        clock += Time.deltaTime;

        if (clock > 2.0f && part == 0)
        {
            StartCoroutine(P.PartOne());
            part++;
        }
    }

    public void PartDone(int part)
    {
        switch (part)
        {
            case 1:
                enableMovement = true;
                enableTarget = true;
                playerScript.SetEnableMovement(enableMovement);
                playerScript.SetEnableTarget(enableTarget);
                break;
            default:
                break;
        }
    }
}
