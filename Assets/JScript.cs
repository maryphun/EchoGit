using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JScript : MonoBehaviour
{
    private bool isMoving;
    [SerializeField] private AudioSource walkingAudio;
    private float targetWalkingVolume;
    [SerializeField] private float walkingSoundValume = 0.8f;
    private Vector3 lastPosition;

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
}
