using UnityEngine;

public class CameraZoomHandler : MonoBehaviour
{
    [SerializeField] private CameraLimitHandler cameraLimitHandler;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float zoomToMinTime;

    public void ZoomToMinValue()
    {
        var minZoomValue = cameraLimitHandler.GetMinZoomValue();
        var currentZoomValue = mainCamera.orthographicSize;
        float elapsedTime = 0;

        while (elapsedTime < zoomToMinTime)
        {
            elapsedTime += Time.deltaTime;

            mainCamera.orthographicSize = Mathf.Lerp(currentZoomValue, minZoomValue, elapsedTime);
        }
    }
}