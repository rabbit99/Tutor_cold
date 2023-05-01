using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    [Header("Player References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public Transform combatLookAt;

    public void SetThirdPersonCam(ThirdPersonCam ThirdPersonCam)
    {
        ThirdPersonCam.orientation = orientation;
        ThirdPersonCam.player = player;
        ThirdPersonCam.playerObj = playerObj;
        ThirdPersonCam.rb = rb;
        ThirdPersonCam.combatLookAt = combatLookAt;
        ThirdPersonCam.Init();
    }

    public void SetCombatCam(CinemachineFreeLook cam)
    {
        cam.Follow = player;
        cam.LookAt = combatLookAt;
    }

    public void SetThirdPersonCam(CinemachineFreeLook cam)
    {
        cam.Follow = player;
        cam.LookAt = player;
    }

    public void SetTopDownCam(CinemachineFreeLook cam)
    {
        cam.Follow = player;
        cam.LookAt = player;
    }
}

