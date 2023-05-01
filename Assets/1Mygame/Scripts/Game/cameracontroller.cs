using UnityEngine;
using System.Collections;
using System;
using Cinemachine;
// 将脚本挂载到摄像机上  
public class cameracontroller : MonoBehaviour
{
    public float minPitch = -80.0f;
    public float maxPitch = 80.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 20.0f;
    public float moveSpeed = 500; // 设置相机移动速度
    public float smoothSpeed = 10.0f;
    public float zoomSpeed = 10.0f;
    private float rotX = 0.0f;
    private float rotY = 0.0f;
    [SerializeField] private float yVelocity = 0.0f;
    private CinemachineVirtualCamera vCam;

    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {


        // 当按住鼠标右键的时候  
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt))
        {
            // 获取鼠标的x和y的值，乘以速度和Time.deltaTime是因为这个可以是运动起来更平滑  
            float h = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
            float v = Input.GetAxis("Mouse Y") * -moveSpeed * Time.deltaTime;
            // 设置当前摄像机移动，y轴并不改变  
            // 需要摄像机按照世界坐标移动，而不是按照它自身的坐标移动，所以加上Spance.World

            // this.transform.Rotate(v, h, 0);

            rotY += Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
            rotX = Mathf.Clamp(rotX - Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime, minPitch, maxPitch);

            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(rotX, 0, 0.0f), Time.deltaTime * smoothSpeed);

            if (transform.localEulerAngles.z != 0)
            {
                // rotX = transform.localEulerAngles.x;
                // rotY = transform.localEulerAngles.y;
                // rotX = Mathf.Clamp(rotX - transform.localEulerAngles.x, minPitch, maxPitch);
                // transform.localEulerAngles = new Vector3(rotX, rotY, 0);
                //  transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(rotX, rotY, 0.0f), Time.deltaTime * moveSpeed);
            }
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        vCam.m_Lens.FieldOfView += scroll * zoomSpeed;
        vCam.m_Lens.FieldOfView = Mathf.Clamp(vCam.m_Lens.FieldOfView, minZoom, maxZoom);

    }
}