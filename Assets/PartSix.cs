using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSix : MonoBehaviour
{
    [SerializeField] private Timeline timeline;

    public void Opened()
    {
        timeline.PartDone(6);
    }
}

