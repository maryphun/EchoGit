using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOne : MonoBehaviour
{
    [SerializeField] private Timeline timeline;

    public void DoorInteracted()
    {
        timeline.PartDone(3);

        Destroy(GetComponent<PartOne>());
    }
}
