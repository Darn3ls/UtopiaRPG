using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utopia
{
    public class PlayerLocomotion : MonoBehaviour
    {
        InputManager inputManager;
        Transform cameraObject;
        Vector3 moveDirection; //handle the movement direction of the player
        Vector3 targetDirection; //handle the rotation of the player, facing always the movement direction (target)
        public float movementSpeed = 7f;
        public float rotationSpeed = 15f;
        Rigidbody playerRigidbody;
        

        private void Awake() 
        {
            inputManager = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody>();
            cameraObject = Camera.main.transform;
        }

        public void HandleAllMovement()
        {
            HandleMovement();
            HandleRotation();
        }

        //movement of the player based on InputManager
        private void HandleMovement()
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput; //move in the direction the camera is facing
            moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput; //movement left/right based on horizontal input
            moveDirection.Normalize(); 
            moveDirection.y = 0;
            moveDirection = moveDirection * movementSpeed;

            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;
        }

        //rotation of the player based on InputManager
        private void HandleRotation()
        {
            targetDirection = Vector3.zero;

            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if(targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }
    }
}
