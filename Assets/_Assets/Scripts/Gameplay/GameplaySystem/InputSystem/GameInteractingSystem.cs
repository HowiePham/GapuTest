using UnityEngine;

public class GameInteractingSystem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private GameEffectManager GameEffectManager => SingletonManager.GameEffectManager;

    private void Awake()
    {
        InitSystem();
        ListenEvent();
    }

    private void OnDestroy()
    {
        StopListeningEvent();
    }

    private void InitSystem()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe(EventName.ClickingLeftMouse, CheckInteractingPoint);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe(EventName.ClickingLeftMouse, CheckInteractingPoint);
    }

    private void CheckInteractingPoint()
    {
        var mouseWorldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var rayCastHit2D = Physics2D.Raycast(mouseWorldPoint, Vector2.zero);

        var hitCollider = rayCastHit2D.collider;
        if (hitCollider == null)
        {
            // Debug.Log($"--- (INPUT) Not interact with anything.");
            HandleRedMarkEffect(mouseWorldPoint);
            return;
        }

        var selectableObject = hitCollider.GetComponent<ISelectable>();

        if (selectableObject == null)
        {
            // Debug.Log($"--- (INPUT) Not interact with anything.");
            HandleRedMarkEffect(mouseWorldPoint);
            return;
        }

        selectableObject.Select();
    }

    private void HandleRedMarkEffect(Vector2 position)
    {
        GameEffectManager.CreateEffectAt(GameEffectType.RedMark, position);
    }
}