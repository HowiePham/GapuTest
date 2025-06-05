using System.Collections;
using UnityEngine;

public class CameraDragHandler : MonoBehaviour
{
    [SerializeField] private CameraLimitHandler cameraLimitHandler;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private bool isDragging;
    [SerializeField] private Vector3 dragStartPosition;
    [SerializeField] private float draggingEffectTime;
    private MapManager MapManager => SingletonManager.MapManager;

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
        GameEventSystem.Subscribe(EventName.HoldingSingleTouch, EnableDraggingCamera);
        GameEventSystem.Subscribe(EventName.ReleaseSingleTouch, DisableDraggingCamera);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe(EventName.FirstTouch, GetFirstTouchPosition);
        GameEventSystem.Unsubscribe(EventName.HoldingSingleTouch, EnableDraggingCamera);
        GameEventSystem.Unsubscribe(EventName.ReleaseSingleTouch, DisableDraggingCamera);
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
        var mainCameraTransform = mainCamera.transform;
        mainCameraTransform.position += difference;

        LimitCameraPosition();
    }

    public void DragCameraTo(Vector2 newPos)
    {
        StartCoroutine(RunDraggingEffect(newPos));
    }
    
    public void DragCameraToCurrentMap()
    {
        var currentMap = MapManager.GetCurrentMap();
        var mapTransform = currentMap.transform;
        var mapPos = mapTransform.position;
        DragCameraTo(mapPos);
    }

    private IEnumerator RunDraggingEffect(Vector2 newPos)
    {
        var mainCameraTransform = mainCamera.transform;
        var oldCamPos = mainCameraTransform.position;

        var newPosVector3 = new Vector3(newPos.x, newPos.y, oldCamPos.z);

        float elapsedTime = 0;
        while (elapsedTime < draggingEffectTime)
        {
            elapsedTime += Time.deltaTime;

            mainCameraTransform.position = Vector3.Lerp(oldCamPos, newPosVector3, elapsedTime / draggingEffectTime);

            yield return null;
        }
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
        SetDraggingState(true);
    }

    private void DisableDraggingCamera()
    {
        SetDraggingState(false);
    }

    private void SetDraggingState(bool state)
    {
        isDragging = state;
    }
}