using UnityEngine;

public abstract class GameTool : MonoBehaviour
{
    [SerializeField] protected GameToolInfo gameToolInfo;
    protected const int consumptionValue = 1;

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
}