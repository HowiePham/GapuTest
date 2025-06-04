using System;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private ObjectVisual objectVisual;
    [SerializeField] private HiddenItem currentTarget;

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

    public void SetNewTarget(HiddenItem target)
    {
        currentTarget = target;
    }

    private void FollowCamera()
    {
        var mainCameraPosition = mainCamera.position;
        var oldPos = transform.position;
        transform.position = new Vector3(mainCameraPosition.x, mainCameraPosition.y, oldPos.z);
    }

    private void PointToTarget()
    {
        if (currentTarget == null) return;

        var rotation = GetRotationToTarget();
        transform.rotation = rotation * transform.rotation;
    }

    private Quaternion GetRotationToTarget()
    {
        var targetTransform = currentTarget.transform;
        var targetPos = targetTransform.position;
        var compassPos = transform.position;
        var dirToTarget = (targetPos - compassPos).normalized;
        var rotation = Quaternion.FromToRotation(transform.up, dirToTarget);
        return rotation;
    }

    public bool TargetAvailable()
    {
        return currentTarget != null && !currentTarget.IsFound;
    }
}