using UnityEngine;

public class EndLevelPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        ActiveEndLevelPanel(false);
        ListenEvent();
    }

    private void OnDisable()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe(EventName.EndLevel, ActiveEndLevelPanel);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe(EventName.EndLevel, ActiveEndLevelPanel);
    }

    private void ActiveEndLevelPanel()
    {
        ActiveEndLevelPanel(true);
    }

    private void ActiveEndLevelPanel(bool state)
    {
        panel.SetActive(state);
    }
}