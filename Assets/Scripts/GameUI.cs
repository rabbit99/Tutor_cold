using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameUI : MonoBehaviourPunCallbacks
{
    public GameObject game_over_ui;
    public GameObject game_win_ui;
    public GameObject back_room_btn;
    // Start is called before the first frame update
    void Start()
    {
        back_room_btn.SetActive(false);
        back_room_btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("RoomScene");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (PhotonNetwork.IsMasterClient)
        {
            back_room_btn.SetActive(true);
        }
        Debug.Log("OnPlayerPropertiesUpdate life ");
        Hashtable hash = targetPlayer.CustomProperties;
        if (hash.ContainsKey("life"))
        {
            Debug.Log("OnPlayerPropertiesUpdate ContainsKey life ");
            if (targetPlayer.NickName == PhotonNetwork.LocalPlayer.NickName )
            {
                game_over_ui.SetActive(!(bool)hash["life"]);
                game_win_ui.SetActive((bool)hash["life"]);
            }
            else
            {
                game_over_ui.SetActive((bool)hash["life"]);
                game_win_ui.SetActive(!(bool)hash["life"]);
            }
        
        }
    }
}
