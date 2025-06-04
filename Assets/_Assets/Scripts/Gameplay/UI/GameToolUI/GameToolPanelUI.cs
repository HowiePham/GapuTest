using UnityEngine;

public class GameToolPanelUI : MonoBehaviour
{
    [SerializeField] private GameToolUI gameToolUIPrefab;
    private GameToolManager GameToolManager => SingletonManager.GameToolManager;

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        var gameToolList = GameToolManager.GetGameToolList();
        foreach (var tool in gameToolList)
        {
            var toolType = tool.Key;
            CreateNewGameToolUI(toolType);
        }
    }

    private void CreateNewGameToolUI(ToolType toolType)
    {
        var newGameToolUI = Instantiate(gameToolUIPrefab, transform);
        newGameToolUI.InitUI(toolType);
    }
}