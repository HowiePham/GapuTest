using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimitHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float minZoomValue;
    [SerializeField] private float maxZoomValue;
    [SerializeField] private List<MapController> mapList = new List<MapController>();
    private Bounds _cameraBounds;

    private void Awake()
    {
        foreach (var map in mapList)
        {
            var mapBounds = map.GetMapBounds();
            _cameraBounds.Encapsulate(mapBounds);
        }
    }

    private void Update()
    {
        LimitCameraPosition();
    }

    private void LimitCameraPosition()
    {
        var cameraTransform = mainCamera.transform;
        var cameraBoundsMin = _cameraBounds.min;
        var cameraBoundsMax = _cameraBounds.max;
        var verticalExtent = mainCamera.orthographicSize;
        var horizontalExtent = verticalExtent * mainCamera.aspect;

        var cameraPosition = cameraTransform.position;
        var minX = cameraBoundsMin.x + horizontalExtent;
        var maxX = cameraBoundsMax.x - horizontalExtent;
        var minY = cameraBoundsMin.y + verticalExtent;
        var maxY = cameraBoundsMax.y - verticalExtent;

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minX, maxX);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minY, maxY);

        cameraTransform.position = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z);
    }
}