using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using System.Text;
using Unity.VisualScripting;
using Photon.Realtime;
using Photon.Pun.Demo.Asteroids;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class Room : MonoBehaviourPunCallbacks
{
    public string GameSceneName = "GameScene";
    [SerializeField]
    Text textroomname;
    [SerializeField]
    Text textYourName;
    [SerializeField]
    Text blueTextPlayerList;
    [SerializeField]
    Text redTextPlayerList;
    [SerializeField]
    Button buttonStartGame;
    [SerializeField]
    Button buttonReadyGame;

    private StringBuilder blueSB = new StringBuilder();
    private StringBuilder redSB = new StringBuilder();
    private bool isReady = true;

    void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            textroomname.text = "房名:" + PhotonNetwork.CurrentRoom.Name;
            textYourName.text = "你的名字:" + PhotonNetwork.LocalPlayer.NickName;
            UpdatePlayerCamp("blue");
            SwitchReady();
            SetLife();
        }
        setBtnState();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        _updatePlayerlist();
        if (PhotonNetwork.IsMasterClient)
        {
            bool allReady = true;
            foreach (var kvp in PhotonNetwork.CurrentRoom.Players)
            {
                Hashtable hash = kvp.Value.CustomProperties;
                if (!hash.ContainsKey("ready")) continue;
                if (!(bool)hash["ready"])
                {
                    allReady = false;
                    break;
                }
            }
            buttonStartGame.interactable = allReady;
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        setBtnState();
    }

    private void setBtnState()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            isReady = true;
            Hashtable myHash = PhotonNetwork.LocalPlayer.CustomProperties;
            myHash["ready"] = isReady;
            PhotonNetwork.LocalPlayer.SetCustomProperties(myHash);
        }
        buttonStartGame.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        buttonReadyGame.gameObject.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public void SwitchReady()
    {
        isReady = !isReady;
        Hashtable myHash = PhotonNetwork.LocalPlayer.CustomProperties;
        if (myHash.ContainsKey("ready"))
        {
            myHash["ready"] = isReady;
          
        }
        else
        {
            myHash.Add("ready", isReady);
        }
        if (isReady)
        {
            buttonReadyGame.GetComponentInChildren<TextMeshProUGUI>().text = "Ready!";
        }
        else
        {
            buttonReadyGame.GetComponentInChildren<TextMeshProUGUI>().text = "Not Ready";
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(myHash);
        Debug.Log(PhotonNetwork.LocalPlayer.ToStringFull());
    }

    public void SetLife()
    {
        Hashtable myHash = PhotonNetwork.LocalPlayer.CustomProperties;
        if (myHash.ContainsKey("life"))
        {
            myHash["life"] = true;

        }
        else
        {
            myHash.Add("life", true);
        }
    }

    public void SwitchCamp()
    {
        Hashtable myHash = PhotonNetwork.LocalPlayer.CustomProperties;
        if ((string)myHash["camp"] == "blue")
        {
            UpdatePlayerCamp("red");
        }
        else
        {
            UpdatePlayerCamp("blue");
        }
    }
    public void UpdatePlayerCamp(string camp)
    {
        Hashtable myHash = PhotonNetwork.LocalPlayer.CustomProperties;
        if (myHash.ContainsKey("camp"))
        {
            myHash["camp"] = camp;
        }
        else
        {
            myHash.Add("camp", camp);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(myHash);
        Debug.Log(PhotonNetwork.LocalPlayer.ToStringFull());
        _updatePlayerlist();
    }

    private void _updatePlayerlist()
    {
        blueSB.Clear();
        redSB.Clear();
        foreach (var kvp in PhotonNetwork.CurrentRoom.Players)
        {
            Hashtable hash = kvp.Value.CustomProperties;
            if (!hash.ContainsKey("camp")) continue;
            if ((string)hash["camp"] == "blue")
            {
                blueSB.AppendLine(kvp.Value.NickName);
            }
            else
            {
                redSB.AppendLine(kvp.Value.NickName);
            }
        }
        blueTextPlayerList.text = blueSB.ToString();
        redTextPlayerList.text = redSB.ToString();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _updatePlayerlist();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _updatePlayerlist();
    }
    public void OnclickStartGame()
    {
        Photon.Realtime.Room room = PhotonNetwork.CurrentRoom;
        if (room == null)
        {
            return;
        }
        //設置 room 的 IsOpen 旗標，讓外部可以藉由此旗標判斷使房間現在不開放
        room.IsOpen = false;
        SceneManager.LoadScene(GameSceneName);
    }
    public void OnclickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
