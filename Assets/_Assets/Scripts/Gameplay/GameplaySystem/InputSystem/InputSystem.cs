using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private const int LeftMouseIndex = 0;

    private void Update()
    {
        ClickLeftMouse();
    }

    private void ClickLeftMouse()
    {
        if (!Input.GetMouseButtonDown(LeftMouseIndex)) return;
        InvokeClickingLeftMouseEvent();
    }

    private void InvokeClickingLeftMouseEvent()
    {
        GameEventSystem.Invoke(EventName.ClickingLeftMouse);
    }
}