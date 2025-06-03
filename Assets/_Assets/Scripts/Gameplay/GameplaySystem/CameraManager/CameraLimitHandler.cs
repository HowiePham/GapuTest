using System.Collections.Generic;
using UnityEngine;

public class CameraLimitHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float minZoomValue;
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
        LimitCameraZoom();
        LimitCameraPosition();
    }

    private void LimitCameraZoom()
    {
        var maxZoomValue = CalculateMaxZoomValue();
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoomValue, maxZoomValue);
    }

    private float CalculateMaxZoomValue()
    {
        var aspectRatio = (float)Screen.width / Screen.height;

        var cameraBoundsSize = _cameraBounds.size;
        var mapWidth = cameraBoundsSize.x;
        var mapHeight = cameraBoundsSize.y;

        var halfMapHeight = mapHeight / 2;
        var halfMapWidthBasedOnAspect = mapWidth / (2 * aspectRatio);
        var maxZoomValue = Mathf.Min(halfMapHeight, halfMapWidthBasedOnAspect);
        
        return maxZoomValue;
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