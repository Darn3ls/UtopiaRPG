using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCameraController : NetworkBehaviour
{
    public CinemachineVirtualCamera cam; // Drag camera into here
 
    void Start()
    {
        // IF I'M THE PLAYER, STOP HERE (DON'T TURN MY OWN CAMERA OFF)
        if (IsLocalPlayer) return;
 
        // DISABLE CAMERA AND CONTROLS HERE (BECAUSE THEY ARE NOT ME)
        cam.enabled = false;
        //GetComponent<PlayerControls>().enabled = false;
        //GetComponent<PlayerMovement>().enabled = false;
    }
}
