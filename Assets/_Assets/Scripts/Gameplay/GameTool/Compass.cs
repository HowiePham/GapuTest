using System;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private ObjectVisual objectVisual;
    [SerializeField] private Transform currentTarget;

    public void Init(ToolType toolType)
    {
        var toolID = (int)toolType;
        objectVisual.InitVisual(toolID);
    }

    private void Update()
    {
        FollowCamera();
        PointToTarget();
    }

    private void FollowCamera()
    {
        var mainCameraPosition = mainCamera.position;
        var oldPos = transform.position;
        transform.position = new Vector3(mainCameraPosition.x, mainCameraPosition.y, oldPos.z);
    }

    private void PointToTarget()
    {
        var targetPos = currentTarget.position;
        var rotation = GetRotationToTarget(targetPos);

        transform.rotation = rotation * transform.rotation;
    }

    private Quaternion GetRotationToTarget(Vector3 targetPos)
    {
        var compassPos = transform.position;
        var dirToTarget = (targetPos - compassPos).normalized;
        var rotation = Quaternion.FromToRotation(transform.up, dirToTarget);
        return rotation;
    }
}