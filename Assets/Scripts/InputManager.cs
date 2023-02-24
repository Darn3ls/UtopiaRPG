using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utopia
{
    public class InputManager : MonoBehaviour
    {
        //new Input System reference script
        PlayerControls playerControls;

        [SerializeField]
        Vector2 movementInput;

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
    }
}

