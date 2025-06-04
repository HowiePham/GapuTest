using System.Collections.Generic;
using UnityEngine;

public abstract class GameTool : MonoBehaviour
{
    [SerializeField] protected GameToolInfo gameToolInfo;
    protected const int consumptionValue = 1;
    protected MapManager MapManager => SingletonManager.MapManager;

    public abstract void Execute();

    public ToolType GetToolType()
    {
        return gameToolInfo.ToolType;
    }

    public bool AnyToolLeft()
    {
        return gameToolInfo.AnyToolLeft();
    }

    public int GetToolQuantity()
    {
        return gameToolInfo.toolQuantity;
    }

    public void ConsumeTool()
    {
        gameToolInfo.ConsumeTool(consumptionValue);
    }

    protected void InvokeUsingToolCompleted()
    {
        GameEventSystem.Invoke(EventName.UsingToolCompleted);
    }

    protected List<HiddenItem> GetAllHiddenItemInMap()
    {
        var currentMapProgress = MapManager.GetCurrentMapProgress();
        var currentMap = MapManager.GetMapController(currentMapProgress);
        var hiddenItemList = currentMap.GetAllHiddenItemInMap();
        return hiddenItemList;
    }
}