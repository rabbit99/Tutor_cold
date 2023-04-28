using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimatorAdvanced : MonoBehaviour
{
    [SerializeField]
    float dashspeed = 50f; //定義衝刺速度為50f
    [SerializeField]
    float normalspeed = 20f;//定義一般速度為20f
    private float speed; //定義出有Speed這個東西
    private Animator _anim; //抓出動畫內容
    [SerializeField]
    private PhotonView _pv;
    private float horizontalInputValue;
    private float verticalInputValue;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        if (_pv == null) _pv = this.gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (_pv && _pv.IsMine)
        {
            horizontalInputValue = Input.GetAxis("Horizontal"); //定義水平輸入的值=電腦接收到垂直指令
            verticalInputValue = Input.GetAxis("Vertical"); //定義垂直輸入的值=電腦接收到水平指令
            if (Input.GetKey(KeyCode.LeftShift))//如果有按住左邊的Shift的話，則速度套入衝刺速度，反之則套入一般速度
            {
                speed = dashspeed;
            }
            else
            {
                speed = normalspeed;
            }
            bool hasHorizontalInput = !Mathf.Approximately(horizontalInputValue, 0f); //bool=有或無，指有水平輸入=電腦真的有偵測到水平輸入
            bool hasVerticalInput = !Mathf.Approximately(verticalInputValue, 0f);//垂直輸入=電腦真的有偵測到垂直輸入
            bool Flying = hasHorizontalInput || hasVerticalInput; //定義Flying的意思=有水平輸入或垂直輸入

            _anim.SetBool("Flying", Flying); //意思為在Animator中若為Flying狀態，則表現出Flying的動畫
            GetComponent<Animator>().SetFloat("Speed", speed);//意思為將Animator中的速度套入這邊的速度值
        }
    }
}
