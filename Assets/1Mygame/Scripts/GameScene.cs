using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameScene : MonoBehaviour
{
    public Transform BlueSpawnPos;
    public Transform RedSpawnPos;
    public ThirdPersonCam ThirdPersonCamController;
    public GameUI GameUI;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            InitGame();
        }
    }
    public void InitGame()
    {
        float spawnPointX = Random.Range(500, 800);
        float spawnPointY = 100;
        float spawnPointZ = 200;
        Vector3 spwanPos = new Vector3();
        Hashtable myHash = PhotonNetwork.LocalPlayer.CustomProperties;
        if ((string)myHash["camp"] == "blue")
        {
            spwanPos = BlueSpawnPos.localPosition;
        }
        else
        {
            spwanPos = RedSpawnPos.localPosition;
        }
        GameObject player = PhotonNetwork.Instantiate("Player", spwanPos, Quaternion.identity);
        PlayerCamController playerCamController = player.GetComponent<PlayerCamController>();
        playerCamController.SetThirdPersonCam(ThirdPersonCamController);
        GameUI.OnGameEnd = () => { ThirdPersonCamController.TurnOffAllCam(); };
    }


    // Update is called once per frame
    void Update()
    {

    }
}
