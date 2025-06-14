﻿using UnityEngine;
using UnityEngine.EventSystems;

public class SingleTouchInputSystem : MonoBehaviour
{
    [SerializeField] private float interactingThreshold;
    [SerializeField] private bool isTouching;
    [SerializeField] private bool isHolding;
    [SerializeField] private bool usingMouse;
    private float _interactingTimer;
    private const int LeftMouseIndex = 0;

    private void Start()
    {
#if UNITY_EDITOR
        HandleUsingMouse();
#endif
        ListenEvent();
    }

    private void OnDestroy()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe(EventName.MultiTouch, ResetSystem);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe(EventName.MultiTouch, ResetSystem);
    }

    private void Update()
    {
        if (!IsSingleTouch()) return;
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

        GameEventSystem.Invoke(EventName.ReleaseSingleTouch);
    }

    private void FirstTouch()
    {
        if (!Input.GetMouseButton(LeftMouseIndex) || IsMouseOverUI() || isTouching) return;

        SetTouchState(true);
        GameEventSystem.Invoke(EventName.FirstTouch);
    }

    private void ReleaseLeftMouse()
    {
        if (!Input.GetMouseButtonUp(LeftMouseIndex) || !isTouching) return;

        CheckClickingEvent();
        ResetSystem();
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
        GameEventSystem.Invoke(EventName.HoldingSingleTouch);
    }

    private bool IsMouseOverUI()
    {
        var eventSystem = EventSystem.current;
#if UNITY_EDITOR
        return eventSystem.IsPointerOverGameObject();
#else
        var touch = Input.GetTouch(LeftMouseIndex);
        return eventSystem.IsPointerOverGameObject(touch.fingerId);
#endif
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

    private bool IsSingleTouch()
    {
        var touchingCount = Input.touchCount;
        return touchingCount == 1 || usingMouse;
    }

    private void HandleUsingMouse()
    {
        usingMouse = true;
    }
}