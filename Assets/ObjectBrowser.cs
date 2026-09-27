using UnityEngine;

/// <summary>
/// 鼠标环绕浏览脚本：把此脚本挂在主相机(Main Camera)上即可。
/// 左键拖拽 = 围绕模型旋转视角；滚轮 = 缩放；右键拖拽 = 平移。
/// </summary>
public class ObjectBrowser : MonoBehaviour
{
    [Tooltip("要观察的目标物体（把场景里的兔子拖进来）")]
    public Transform target;

    [Tooltip("旋转速度")]
    public float rotateSpeed = 5f;

    [Tooltip("缩放速度")]
    public float zoomSpeed = 500f;

    [Tooltip("平移速度")]
    public float panSpeed = 0.5f;

    private float distance = 2.5f;   // 相机到目标的距离
    private float yaw = 0f;          // 水平角
    private float pitch = 20f;       // 俯仰角

    void Start()
    {
        if (target != null) UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 左键拖拽：旋转
        if (Input.GetMouseButton(0))
        {
            yaw += Input.GetAxis("Mouse X") * rotateSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
            pitch = Mathf.Clamp(pitch, -80f, 80f);
        }

        // 滚轮：缩放
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, 0.3f, 10f);

        // 右键拖拽：平移目标点
        if (Input.GetMouseButton(1))
        {
            Vector3 right = transform.right;
            Vector3 up = transform.up;
            target.position += (-right * Input.GetAxis("Mouse X") - up * Input.GetAxis("Mouse Y")) * panSpeed * Time.deltaTime;
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = target.position - rot * Vector3.forward * distance;
        transform.rotation = rot;
    }
}
//（注：内容由AI生成）
