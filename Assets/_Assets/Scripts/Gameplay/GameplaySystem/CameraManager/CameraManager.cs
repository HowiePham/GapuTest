using UnityEngine;

public class CameraManager : TemporaryMonoSingleton<CameraManager>
{
    [SerializeField] private CameraZoomHandler cameraZoomHandler;
    [SerializeField] private CameraDragHandler cameraDragHandler;

    public void ZoomToMinValue()
    {
        cameraZoomHandler.ZoomToMinValue();
    }

    public void DragCameraTo(Vector2 newPos)
    {
        cameraDragHandler.DragCameraTo(newPos);
    }

    public void DragCameraToCurrentMap()
    {
        cameraDragHandler.DragCameraToCurrentMap();
    }
}