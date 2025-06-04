using System.Collections.Generic;
using UnityEngine;

public class GameToolManager : TemporaryMonoSingleton<GameToolManager>
{
    private Dictionary<ToolType, GameTool> _gameToolList;

    protected override void Init()
    {
        InitSystem();
        ListenEvent();
    }

    private void OnDisable()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<ToolType>(EventName.UsingTool, UseTool);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<ToolType>(EventName.UsingTool, UseTool);
    }

    private void InitSystem()
    {
        _gameToolList = new Dictionary<ToolType, GameTool>();

        foreach (Transform obj in transform)
        {
            var gameTool = obj.GetComponent<GameTool>();
            if (gameTool == null) continue;

            var toolType = gameTool.GetToolType();
            _gameToolList.Add(toolType, gameTool);
        }
    }

    private GameTool GetGameTool(ToolType toolType)
    {
        return _gameToolList[toolType];
    }

    public Dictionary<ToolType, GameTool> GetGameToolList()
    {
        return _gameToolList;
    }

    public void UseTool(ToolType toolType)
    {
        var gameTool = GetGameTool(toolType);
        if (!gameTool.AnyToolLeft()) return;

        gameTool.Execute();
    }

    public int GetToolQuantity(ToolType toolType)
    {
        var gameTool = GetGameTool(toolType);
        return gameTool.GetToolQuantity();
    }
}