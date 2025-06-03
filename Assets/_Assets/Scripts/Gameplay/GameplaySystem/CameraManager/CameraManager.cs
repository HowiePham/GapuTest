using UnityEngine;

public class CameraManager : TemporaryMonoSingleton<CameraManager>
{
    [SerializeField] private CameraZoomHandler cameraZoomHandler;

    public void ZoomToMinValue()
    {
        cameraZoomHandler.ZoomToMinValue();
    }
}