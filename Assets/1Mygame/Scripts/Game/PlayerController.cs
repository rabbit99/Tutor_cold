using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using Cinemachine;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class PlayerController : MonoBehaviour, IPunObservable
{
    Vector3 movement; //抓出三維度座標系統
    Animator anim; //抓出動畫內容
    float speed; //定義出有Speed這個東西
    float rotateSpeed = 0.1f; //定義旋轉的速度為20
    float dashspeed = 50f; //定義衝刺速度為50f
    float normalspeed = 20f;//定義一般速度為20f
    [SerializeField] private float yVelocity = 0.0f;
    Quaternion rotation; //旋轉
    Rigidbody rb;//套入剛體系統
    private PhotonView _pv;

    [SerializeField] public Camera CM;
    [SerializeField] public CinemachineVirtualCamera CinemachineVirtualCamera;

    [SerializeField] private GameObject bullet1;
    [SerializeField] private Transform gun1;

    private float horizontalInputValue;
    private float verticalInputValue;
    private Coroutine _MoveToPositionCoroutine;
    [SerializeField]
    public int playerHealth;
    public HPBar hPBar;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _pv = this.gameObject.GetComponent<PhotonView>();
        if (_pv && !_pv.IsMine)
        {
            //_pv.ObservedComponents.Add(this);
            CM.enabled = false;          // 如果視野是自己的，使新增的相機無效
            CinemachineVirtualCamera.enabled = false;
            CM.gameObject.SetActive(false);
            CinemachineVirtualCamera.gameObject.SetActive(false);
        }
    }
    public void SmoothRotationY(float iTargetAngle)
    {
        transform.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, iTargetAngle, ref yVelocity, rotateSpeed), 0);
    }

    void FixedUpdate() //從Update改成fixedUpdate是為了避免與Unity內建的物理系統互衝

    {
        if (_pv && _pv.IsMine)
        {
            //定義相機的視角
            Vector3 vecForward = Camera.main.transform.forward;
            vecForward.y = 0.0f;
            vecForward.Normalize();
            Vector3 vecRight = Camera.main.transform.right;
            vecRight.y = 0.0f;
            vecRight.Normalize();



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

            anim.SetBool("Flying", Flying); //意思為在Animator中若為Flying狀態，則表現出Flying的動畫
            GetComponent<Animator>().SetFloat("Speed", speed);//意思為將Animator中的速度套入這邊的速度值

            //所以就可以定義動作為 : 當呈現飛行狀態時，進入animator中的movement的動作，而movement區分衝刺與一般速度的選項由speed決定
            /*
            movement.Set(horizontalInputValue, 0f, verticalInputValue); //將三位元座標(xx,yy,zz)放入上方水平輸入的值
            movement.Normalize();
            Vector3 rotateDirection = Vector3.RotateTowards(transform.forward, movement, rotateSpeed * Time.deltaTime, 0f);//意思為旋轉方向為目標前方，其速度為定義的旋轉速度*FPS
            rotation = Quaternion.LookRotation(rotateDirection);//套用旋轉 (?)
            */



            //以下定義方向鍵以及速度與轉動

            if (horizontalInputValue > 0) //如果水平輸入值大於0，就代表有值正在輸入
            {
                //transform.position += new Vector3(speed * Time.deltaTime, 0, 0); //所以新的座標X軸就會+20(自己輸入的值)*FPS ，以下相同
                SmoothRotationY(Camera.main.transform.eulerAngles.y + 90);
                //transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y+90, 0);
            }
            else if (horizontalInputValue < 0)
            {
                //rb.velocity = vecRight * speed * Time.deltaTime * horizontalInputValue * anim.deltaPosition.magnitude;
                //transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                SmoothRotationY(Camera.main.transform.eulerAngles.y - 90);
            }
            if (verticalInputValue > 0)
            {
                //rb.velocity = vecForward * speed * Time.deltaTime * verticalInputValue * anim.deltaPosition.magnitude;
                //transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                SmoothRotationY(Camera.main.transform.eulerAngles.y);
            }
            else if (verticalInputValue < 0)
            {
                //rb.velocity = vecForward * speed * Time.deltaTime * verticalInputValue * anim.deltaPosition.magnitude;
                //transform.position += new Vector3(0, 0, -speed * Time.deltaTime);   
                SmoothRotationY(Camera.main.transform.eulerAngles.y + 180);
            }

            if (horizontalInputValue == 0 && verticalInputValue == 0) return;
            rb.velocity = new Vector3(horizontalInputValue * speed, 0, verticalInputValue * speed);

            //movement.Normalize();
            /* Vector3 rotateDirection = Vector3.RotateTowards(transform.forward, movement, rotateSpeed * Time.deltaTime, 0f);//意思為旋轉方向為目標前方，其速度為定義的旋轉速度*FPS
             rotation = Quaternion.LookRotation(rotateDirection);//套用旋轉 (?)*/
            //rb.MovePosition(rb.position + movement * anim.deltaPosition.magnitude);
            //this.transform.Translate(movement * anim.deltaPosition.magnitude);
            //rb.velocity = movement;
        }

    }
    private void Update()
    {
        if (_pv && _pv.IsMine)
        {
            //INPUT
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Instantiate(bullet1, gun1.position, gun1.rotation);
            }
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 targetPosition = hit.point;
                    if (_MoveToPositionCoroutine != null) StopCoroutine(_MoveToPositionCoroutine);
                    _MoveToPositionCoroutine = StartCoroutine(MoveToPosition(transform, targetPosition, speed));
                }
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Hurt();
            }
        }
        hPBar.UpdateHp(playerHealth);
    }

    private IEnumerator MoveToPosition(Transform transform, Vector3 position, float speed)
    {
        while (Vector3.Distance(transform.position, position) > 0.1f)
        {
            Vector3 movePos = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            transform.position = movePos;
            anim.SetBool("Flying", true);

            Vector3 direction = (position - transform.position).normalized;
            Debug.Log("direction = " + direction);
            if (direction.x > 0) //如果水平輸入值大於0，就代表有值正在輸入
            {
                SmoothRotationY(Camera.main.transform.eulerAngles.y + 90);
            }
            else if (direction.x < 0)
            {
                SmoothRotationY(Camera.main.transform.eulerAngles.y - 90);
            }
            if (direction.z > 0)
            {
                SmoothRotationY(Camera.main.transform.eulerAngles.y);
            }
            else if (direction.z < 0)
            {
                SmoothRotationY(Camera.main.transform.eulerAngles.y + 180);
            }
            yield return null;
        }
        _MoveToPositionCoroutine = null;
    }

    public void Hurt()
    {
        if (_pv && !_pv.IsMine)
            return;
        playerHealth -= 10;
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

