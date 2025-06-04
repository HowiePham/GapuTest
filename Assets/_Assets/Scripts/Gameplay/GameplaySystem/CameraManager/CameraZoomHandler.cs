using System.Collections;
using UnityEngine;

public class CameraZoomHandler : MonoBehaviour
{
    [SerializeField] private CameraLimitHandler cameraLimitHandler;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float zoomToMinTime;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private bool isZooming;
    private const int FirstTouchIndex = 0;
    private const int SecondTouchIndex = 1;

    private void Start()
    {
        ListenEvent();
    }

    private void OnDestroy()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe(EventName.MultiTouch, EnableZoomState);
        GameEventSystem.Subscribe(EventName.ReleaseMultiTouching, DisableZoomState);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe(EventName.MultiTouch, EnableZoomState);
        GameEventSystem.Unsubscribe(EventName.ReleaseMultiTouching, DisableZoomState);
    }

    private void Update()
    {
        ZoomCamera();
    }

    private void ZoomCamera()
    {
        if (!isZooming) return;

        var deltaMagnitudeDiff = CalculateMultiTouchDiff();

        mainCamera.orthographicSize += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;
        LimitCamera();
    }

    private void LimitCamera()
    {
        cameraLimitHandler.LimitCameraZoom();
        cameraLimitHandler.LimitCameraPosition();
    }

    private float CalculateMultiTouchDiff()
    {
        var touchZero = Input.GetTouch(FirstTouchIndex);
        var touchOne = Input.GetTouch(SecondTouchIndex);

        var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
        var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
        return deltaMagnitudeDiff;
    }

    public void ZoomToMinValue()
    {
        var minZoomValue = cameraLimitHandler.GetMinZoomValue();

        StartCoroutine(RunZoomEffect(minZoomValue));
    }

    private IEnumerator RunZoomEffect(float zoomValue)
    {
        var currentZoomValue = mainCamera.orthographicSize;
        float elapsedTime = 0;

        while (elapsedTime < zoomToMinTime)
        {
            elapsedTime += Time.deltaTime;

            mainCamera.orthographicSize = Mathf.Lerp(currentZoomValue, zoomValue, elapsedTime / zoomToMinTime);

            yield return null;
        }
    }

    private void EnableZoomState()
    {
        isZooming = true;
    }

    private void DisableZoomState()
    {
        isZooming = false;
    }
}