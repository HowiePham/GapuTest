using UnityEngine;

public class CameraDragHandler : MonoBehaviour
{
    [SerializeField] private CameraLimitHandler cameraLimitHandler;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private bool isDragging;
    [SerializeField] private Vector3 dragStartPosition;

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
        GameEventSystem.Subscribe(EventName.FirstTouch, GetFirstTouchPosition);
        GameEventSystem.Subscribe(EventName.HoldingLeftMouse, EnableDraggingCamera);
        GameEventSystem.Subscribe(EventName.ReleaseLeftMouse, DisableDraggingCamera);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe(EventName.FirstTouch, GetFirstTouchPosition);
        GameEventSystem.Unsubscribe(EventName.HoldingLeftMouse, EnableDraggingCamera);
        GameEventSystem.Unsubscribe(EventName.ReleaseLeftMouse, DisableDraggingCamera);
    }

    private void Update()
    {
        HandleDraggingCamera();
    }

    private void HandleDraggingCamera()
    {
        if (!isDragging) return;

        var currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var difference = dragStartPosition - currentMousePosition;
        mainCamera.transform.position += difference;

        LimitCameraPosition();
    }

    private void LimitCameraPosition()
    {
        cameraLimitHandler.LimitCameraPosition();
    }

    private void GetFirstTouchPosition()
    {
        dragStartPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void EnableDraggingCamera()
    {
        isDragging = true;
    }

    private void DisableDraggingCamera()
    {
        isDragging = false;
    }
}