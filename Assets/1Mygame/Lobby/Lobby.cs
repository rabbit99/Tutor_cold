using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using System.Text;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    InputField inputRoomName;
    [SerializeField]
    Text textRoomList;

    private StringBuilder sb = new StringBuilder();
    private List<RoomInfo> _roomList = new List<RoomInfo>();
    void Start()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            SceneManager.LoadScene("LoginScene");
        }
        else
        {
            //PhotonNetwork.JoinLobby();
        }
        print("進入大廳");
    }

    //public override void OnJoinedLobby()
    //{
    //    print("進入大廳");
    //}
    public string GetRoomName()
    {
        string roomname = inputRoomName.text;
        return roomname.Trim();
    }
    public void OnClickCreateRoom()
    {
        string roomname = GetRoomName();
        if (roomname.Length > 0)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            PhotonNetwork.CreateRoom(roomname, roomOptions);
            //PhotonNetwork.LocalPlayer.NickName = playername;
        }
        else
        {
            print("無法使用此房名或暱稱");
        }
    }
    public void OnclickJoinRoom()
    {
        string roomname = GetRoomName();
        if (roomname.Length > 0)
        {
            PhotonNetwork.JoinRoom(roomname);
            //PhotonNetwork.LocalPlayer.NickName = playername;
        }
        else
        {
            print("無法加入房間");
        }
    }
    public override void OnJoinedRoom()
    {
        print("已加入房間");
        SceneManager.LoadScene("RoomScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("加入房間失敗 = " + returnCode + " =" + message);
    }
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("創建房間成功");
        //SceneManager.LoadScene("RoomScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }

    //舊的 OnRoomListUpdate
    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    print("update");
    //    StringBuilder sb = new StringBuilder();
    //    foreach (RoomInfo roomInfo in roomList)
    //    {
    //        if (roomInfo.PlayerCount > 0)
    //        {
    //            sb.AppendLine(roomInfo.Name +"  " +  roomInfo.PlayerCount + "人");
    //        }
    //    }
    //    textRoomList.text = sb.ToString();
    //}


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("OnRoomListUpdate  ");

        var oldRoomUpdateList = roomList.Where(i => _roomList.Contains(i)).ToList();
        foreach (RoomInfo room in oldRoomUpdateList)
        {
            //_roomList.Add(room);
            Debug.Log(room);
            if (room.PlayerCount == 0)
            {
                int index = _roomList.FindIndex(x => x.Equals(room));
                _roomList.RemoveAt(index);
                roomList.Remove(room);
            }
            if (!room.IsOpen)
            {
                //int index = _roomList.FindIndex(x => x.Equals(room));
                //_roomList.RemoveAt(index);
                //roomList.Remove(room);
            }
        }
        var newRoomList = roomList.Where(i => !_roomList.Contains(i)).ToList();
        foreach (RoomInfo room in newRoomList)
        {
            if (room.PlayerCount != 0)
                _roomList.Add(room);
        }

        sb.Clear();
        foreach (RoomInfo roomInfo in _roomList)
        {
            Hashtable cp = roomInfo.CustomProperties;
            //sb.AppendLine(">> " + room.Name + "  map = " + cp["map"]);
            if (!roomInfo.IsOpen)
            {
                sb.AppendLine(roomInfo.Name + "  " + roomInfo.PlayerCount + "人 -遊戲進行中");
            }
            else
            {
                sb.AppendLine(roomInfo.Name + "  " + roomInfo.PlayerCount + "人");
            }
        }
        textRoomList.text = sb.ToString();
    }

    public void RefreshRoomList()
    {
        PhotonNetwork.GetCustomRoomList(PhotonNetwork.CurrentLobby, null);
    }
}

