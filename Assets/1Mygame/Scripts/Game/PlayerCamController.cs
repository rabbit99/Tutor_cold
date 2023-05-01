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
        setCombatCam(ThirdPersonCam.combatCam.GetComponent<CinemachineFreeLook>());
        setThirdPersonCam(ThirdPersonCam.thirdPersonCam.GetComponent<CinemachineFreeLook>());
        setTopDownCam(ThirdPersonCam.topDownCam.GetComponent<CinemachineFreeLook>());
    }

    private void setCombatCam(CinemachineFreeLook cam)
    {
        cam.Follow = player;
        cam.LookAt = combatLookAt;
    }

    private void setThirdPersonCam(CinemachineFreeLook cam)
    {
        cam.Follow = player;
        cam.LookAt = player;
    }

    private void setTopDownCam(CinemachineFreeLook cam)
    {
        cam.Follow = player;
        cam.LookAt = player;
    }
}

