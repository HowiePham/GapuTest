using UnityEngine;
using UnityEngine.UI;

public class GameToolUI : MonoBehaviour
{
    [SerializeField] private ToolType toolType;
    [SerializeField] private Image toolImage;
    [SerializeField] private Text quantityText;

    public void OnClickToolButton()
    {
        GameEventSystem.Invoke(EventName.UsingTool, toolType);
    }
}