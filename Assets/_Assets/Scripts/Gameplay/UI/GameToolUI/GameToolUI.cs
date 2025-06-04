using UnityEngine;
using UnityEngine.UI;

public class GameToolUI : MonoBehaviour
{
    [SerializeField] private ToolType toolType;
    [SerializeField] private Image toolImage;
    [SerializeField] private Text quantityText;

    private GameToolManager GameToolManager => SingletonManager.GameToolManager;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;

    public void InitUI(ToolType newToolType)
    {
        toolType = newToolType;
        HandleQuantityText();
        HandleToolImage();
    }

    private void HandleToolImage()
    {
        var toolID = (int)toolType;
        var image = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.GameTool, toolID);

        toolImage.sprite = image;
    }

    private void HandleQuantityText()
    {
        var toolQuantity = GameToolManager.GetToolQuantity(toolType);
        quantityText.text = toolQuantity.ToString();
    }

    public void OnClickToolButton()
    {
        GameEventSystem.Invoke(EventName.UsingTool, toolType);
    }
}