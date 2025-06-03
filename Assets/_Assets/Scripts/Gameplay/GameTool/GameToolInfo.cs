using System;

[Serializable]
public struct GameToolInfo
{
    public ToolType ToolType;
    public int toolQuantity;
    private const int EmptyValue = 0;

    public void ConsumeTool(int value)
    {
        toolQuantity -= value;
    }

    public bool AnyToolLeft()
    {
        return toolQuantity > EmptyValue;
    }
}