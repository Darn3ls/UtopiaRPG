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
        private float moveAmount;
        public float verticalInput;
        public float horizontalInput;

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

