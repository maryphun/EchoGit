using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    /// Main camera.
    public Camera mainCamera;

    /// Cube controller.
    public Door[] doors;

    [SerializeField] private float minOpenDistance;

    void Update()
    {
        Ray ray = mainCamera.ViewportPointToRay(0.5f * Vector2.one);

        foreach (Door door in doors)
        {
            RaycastHit hit;
            bool cubeHit = Physics.Raycast(ray, out hit) && hit.transform == door.transform;
            if (cubeHit)
            {
                if ((Input.touchCount == 0 && Input.GetMouseButtonDown(0)) ||    // LMB for desktop.
                   (Input.touchCount > 0 && Input.GetTouch(0).tapCount > 1 &&   // Double-tap for mobile.
                    Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    // open this door
                    if (Vector3.Distance(hit.point, Camera.main.transform.position) < minOpenDistance)
                    {
                        door.Open();
                    }
                }
            }
        }
    }
}
