using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssignCorrectInput : NetworkBehaviour
{
    // Get "Player Input" component which sould be attached to player prefab
    [SerializeField] private PlayerInput playerInput;
 
   // On spawn
   public override void OnNetworkSpawn()
   {
        base.OnNetworkSpawn();
        // Make sure this belongs to us
        if (!IsOwner) { return; }
        // Enable the input player for client
        playerInput.enabled = true;
   }
}
