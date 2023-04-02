using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class Attack : MonoBehaviour
    {
        private StarterAssetsInputs starterAssetsInput;
        public Animator animator;

        private void Awake() {
            starterAssetsInput = GetComponent<StarterAssetsInputs>();
        }
        
        private void Update() {
            if(starterAssetsInput.attack)
            {
                Debug.Log("Attack");
                animator.PlayInFixedTime("Unarmed-Attack-R2");
                starterAssetsInput.attack = false;
            }
        }
    }
}

