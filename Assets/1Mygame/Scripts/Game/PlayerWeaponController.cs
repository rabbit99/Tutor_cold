using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bullet1;
    [SerializeField] private Transform gun1;

    private PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        _pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pv && _pv.IsMine)
        {
            //INPUT
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _pv.RPC("fire", RpcTarget.AllViaServer);
            }
        }
    }

    [PunRPC]
    public void fire()
    {
        Instantiate(bullet1, gun1.position, gun1.rotation);
    }
}
