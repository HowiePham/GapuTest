using UnityEngine;

public class MultiTouchInputSystem : MonoBehaviour
{
    [SerializeField] private int minTouchingCount;
    [SerializeField] private bool multiTouching;

    private void Update()
    {
        CheckReleaseMultiTouching();
        CheckMultiTouching();
    }

    private void CheckReleaseMultiTouching()
    {
        if (!CanCheckReleaseMultiTouching()) return;

        SetMultiTouchingState(false);
        GameEventSystem.Invoke(EventName.ReleaseMultiTouching);
    }

    private void CheckMultiTouching()
    {
        if (!CanCheckMultiTouching()) return;

        SetMultiTouchingState(true);
        GameEventSystem.Invoke(EventName.MultiTouch);
    }

    private bool CanCheckMultiTouching()
    {
        return !multiTouching && IsMultiTouching();
    }

    private bool CanCheckReleaseMultiTouching()
    {
        return multiTouching && !IsMultiTouching();
    }
    
    private bool IsMultiTouching()
    {
        var touchingCount = Input.touchCount;

        return touchingCount >= minTouchingCount;
    }

    private void SetMultiTouchingState(bool state)
    {
        multiTouching = state;
    }

    public void ResetSystem()
    {
        SetMultiTouchingState(false);
        
    }
}