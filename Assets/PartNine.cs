using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartNine : MonoBehaviour
{
    [SerializeField] private Timeline timeline;

    public void Opened()
    {
        timeline.PartDone(7);
    }
}
