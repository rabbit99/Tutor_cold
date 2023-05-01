using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerHpController : MonoBehaviour, IPunObservable
{
    public int playerHealth;
    public HPBar hPBar;

    private PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        _pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        hPBar.UpdateHp(playerHealth);
    }

    public void Hurt()
    {
        if (_pv && !_pv.IsMine)
            return;
        playerHealth -= 10;
        hPBar.UpdateHp(playerHealth);
        if (playerHealth <= 0)
        {
            Hashtable myHash = PhotonNetwork.LocalPlayer.CustomProperties;
            if (myHash.ContainsKey("life"))
            {
                Debug.Log("ContainsKey life ");
                myHash["life"] = false;
                this.enabled = false;
            }
            else
            {
                Debug.Log("no ContainsKey life ");
            }
            PhotonNetwork.LocalPlayer.SetCustomProperties(myHash);
        }
    }

    // 實現IPunObservable接口
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 將生命值寫入流中
            stream.SendNext(playerHealth);
        }
        else
        {
            // 從流中讀取生命值
            //playerHealth = (int)stream.ReceiveNext();
            playerHealth = (int)stream.ReceiveNext();


        }
    }
}
