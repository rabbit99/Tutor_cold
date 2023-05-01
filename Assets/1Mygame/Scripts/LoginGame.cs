using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginGame : MonoBehaviourPunCallbacks
{
    public InputField inputPlayerName;
    public void OnClickStar()
    {
        string playerName = inputPlayerName.text;
        if (!playerName.Equals(""))
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
            print("ClickStart");
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }
    public override void OnConnectedToMaster()
    {
        print("Connect!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("OnJoinedLobby");
        SceneManager.LoadScene("LobbyScene");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("OnRoomListUpdate  ");
    }
}


