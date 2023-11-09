using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public Transform startPoint;  // 射线起点
    public float rayLength = 10f; // 射线长度
    public LineRenderer lineRenderer; // LineRenderer组件

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        // 获取LineRenderer组件
        lineRenderer = GetComponent<LineRenderer>();

        // 设置LineRenderer的起点和终点
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint.position);
    }

    void Update()
    {
        // 创建一条射线
        ray = new Ray(startPoint.position, startPoint.forward);

        // 检测射线碰撞
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // 如果射线击中物体，设置LineRenderer的终点为击中点
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // 如果射线没有击中物体，设置LineRenderer的终点为射线的最大长度
            lineRenderer.SetPosition(1, startPoint.position + startPoint.forward * rayLength);
        }
    }
}
