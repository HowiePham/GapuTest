using UnityEngine;

public class MapUnlockPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private CameraManager CameraManager => SingletonManager.CameraManager;

    private void Start()
    {
        ListenEvent();
    }

    private void OnDisable()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe(EventName.UnlockNewMap, EnablePanel);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe(EventName.UnlockNewMap, EnablePanel);
    }

    public void GoToCurrentMap()
    {
        CameraManager.DragCameraToCurrentMap();
    }

    private void EnablePanel()
    {
        panel.SetActive(true);
    }
}