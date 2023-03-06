using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utopia
{
    public class InputManager : MonoBehaviour
    {
        //new Input System reference script
        PlayerControls playerControls;

        AnimatorManager animatorManager;

        [SerializeField]
        Vector2 movementInput;
        [SerializeField]
        Vector2 cameraInput;
        private float moveAmount;
        public float verticalInput;
        public float horizontalInput;

        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        private void Awake() 
        {
            animatorManager = GetComponent<AnimatorManager>();
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
                //when WASD or Joystick left call Movement Vector2
                playerControls.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                //when mouse or joystick right
                playerControls.PlayerMovement.Camera.performed += inputActions => cameraInput = inputActions.ReadValue<Vector2>();
            }
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }
    
        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            cameraHorizontalInput = cameraInput.x;
            cameraVerticalInput = cameraInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateAnimatorValues(0, moveAmount);
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
            //HandleJumpingInput();
            //HandleActionInput();
        }
    
    }
}

