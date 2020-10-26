// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;

/// First-person player controller for Resonance Audio demo scenes.
[RequireComponent(typeof(CharacterController))]
public class DemoPlayerController : MonoBehaviour {
      /// Camera.
      public Camera mainCamera;

      // Character controller.
      private CharacterController characterController = null;

      // Player movement speed.
      [SerializeField] private float movementSpeed = 5.0f;

      [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private float walkingSoundValume = 0.8f;
    [SerializeField] private AudioSource targetSoundEffect;

      // Target camera rotation in degrees.
      private float rotationX = 0.0f;
      private float rotationY = 0.0f;

      // Maximum allowed vertical rotation angle in degrees.
      private const float clampAngleDegrees = 80.0f;

      // Camera rotation sensitivity.
      private const float sensitivity = 2.0f;

      private bool isMoving;
    private float targetWalkingVolume;
    private bool enableMovement, enableTarget;

    private Vector3 lastPosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Vector3 rotation = mainCamera.transform.rotation.eulerAngles;
        rotationX = rotation.x;
        rotationY = rotation.y;
        isMoving = false;
    }

    void LateUpdate()
    {
        // Update the rotation.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Note that multi-touch control is not supported on mobile devices.
            mouseX = 0.0f;
            mouseY = 0.0f;
        }
        //rotationX += sensitivity * mouseY;
        rotationY += sensitivity * mouseX;
        rotationX = Mathf.Clamp(rotationX, -clampAngleDegrees, clampAngleDegrees);
        mainCamera.transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        // Update the position.
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(movementX, 0.0f, movementY);

        if (!enableMovement)
        {
            movementDirection = Vector3.zero;
        }

        movementDirection = mainCamera.transform.rotation * movementDirection;
        movementDirection.y = 0.0f;

        if (movementDirection.magnitude != 0.0f)
        {
            Move(movementDirection);
        }


        // determine if the player is moving
        isMoving = false;
        if (lastPosition != transform.position)
        {
            isMoving = true;
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

        if (Input.GetKeyDown(KeyCode.Space) && enableTarget)
        {
            ListenTarget();
        }
    }


    private void Move(Vector3 directionVector)
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), directionVector);
        RaycastHit hit;
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), directionVector, Color.red, 1.0f);
        bool moveable = !Physics.Raycast(ray, out hit, movementSpeed * Time.deltaTime);

        if (moveable)
        {
            characterController.SimpleMove(movementSpeed * directionVector);
            return;
        }
    }

    private void ListenTarget()
    {
        targetSoundEffect.volume = 1.0f;
        targetSoundEffect.Play();
    }

    public void SetEnableMovement(bool boolean)
    {
        enableMovement = boolean;
    }
    public void SetEnableTarget(bool boolean)
    {
        enableTarget = boolean;
    }
}
