using UnityEngine;

public class MapUnlockPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private MapManager MapManager => SingletonManager.MapManager;

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
        MapManager.GoToCurrentMap();
    }

    private void EnablePanel()
    {
        panel.SetActive(true);
    }
}