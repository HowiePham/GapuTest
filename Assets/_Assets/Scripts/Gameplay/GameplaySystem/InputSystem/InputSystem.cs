using UnityEngine;
using UnityEngine.EventSystems;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private float interactingThreshold;
    [SerializeField] private bool isTouching;
    [SerializeField] private bool isHolding;
    private float _interactingTimer;
    private const int LeftMouseIndex = 0;

    private void Start()
    {
        ResetSystem();
    }

    private void Update()
    {
        ReleaseLeftMouse();
        RunInteractingTimer();
        CheckHoldingEvent();
        FirstTouch();
    }

    private void RunInteractingTimer()
    {
        if (!isTouching) return;

        _interactingTimer += Time.deltaTime;
    }

    private void ResetSystem()
    {
        _interactingTimer = 0;
        SetTouchState(false);
        SetHoldingState(false);
    }

    private void FirstTouch()
    {
        if (!Input.GetMouseButton(LeftMouseIndex) || IsMouseOverUI() || isTouching) return;

        SetTouchState(true);
        GameEventSystem.Invoke(EventName.FirstTouch);
    }

    private void ReleaseLeftMouse()
    {
        if (!Input.GetMouseButtonUp(LeftMouseIndex)) return;

        CheckClickingEvent();
        ResetSystem();
        GameEventSystem.Invoke(EventName.ReleaseLeftMouse);
    }

    private void CheckClickingEvent()
    {
        if (EnoughHoldingThreshold()) return;

        GameEventSystem.Invoke(EventName.ClickingLeftMouse);
    }

    private void CheckHoldingEvent()
    {
        if (isHolding || !isTouching) return;
        if (!EnoughHoldingThreshold()) return;

        SetHoldingState(true);
        GameEventSystem.Invoke(EventName.HoldingLeftMouse);
    }

    private bool IsMouseOverUI()
    {
        var eventSystem = EventSystem.current;
        return eventSystem.IsPointerOverGameObject();
    }

    private void SetTouchState(bool state)
    {
        isTouching = state;
    }

    private void SetHoldingState(bool state)
    {
        isHolding = state;
    }

    private bool EnoughHoldingThreshold()
    {
        return _interactingTimer >= interactingThreshold;
    }
}