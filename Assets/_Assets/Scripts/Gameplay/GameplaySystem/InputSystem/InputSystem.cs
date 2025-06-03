using UnityEngine;
using UnityEngine.EventSystems;

public class InputSystem : MonoBehaviour
{
    private const int LeftMouseIndex = 0;

    private void Update()
    {
        ClickLeftMouse();
    }

    private void ClickLeftMouse()
    {
        if (!Input.GetMouseButtonDown(LeftMouseIndex) || IsMouseOverUI()) return;
        InvokeClickingLeftMouseEvent();
    }

    private void InvokeClickingLeftMouseEvent()
    {
        GameEventSystem.Invoke(EventName.ClickingLeftMouse);
    }

    private bool IsMouseOverUI()
    {
        var eventSystem = EventSystem.current;
        return eventSystem.IsPointerOverGameObject();
    }
}