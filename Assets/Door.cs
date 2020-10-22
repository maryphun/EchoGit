using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    // Start
    public void Open()
    {
        transform.DOMoveY(transform.position.y + GetComponent<Collider>().bounds.extents.y, 1.0f, false);
    }
}
