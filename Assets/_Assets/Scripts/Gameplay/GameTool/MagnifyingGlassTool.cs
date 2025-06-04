using UnityEngine;

public class MagnifyingGlassTool : GameTool
{
    private MapManager MapManager => SingletonManager.MapManager;
    private CameraManager CameraManager => SingletonManager.CameraManager;

    public override void Execute()
    {
        FindHiddenItem();
        ConsumeTool();
    }

    private void FindHiddenItem()
    {
        var currentMapProgress = MapManager.GetCurrentMapProgress();
        var currentMap = MapManager.GetMapController(currentMapProgress);
        var hiddenItemList = currentMap.GetAllHiddenItemInMap();

        foreach (var hiddenItem in hiddenItemList)
        {
            if (hiddenItem.IsFound) continue;

            var itemPos = hiddenItem.GetPos();
            MoveCameraTo(itemPos);
            ZoomCamera();

            return;
        }
    }

    private void MoveCameraTo(Vector2 itemPos)
    {
        CameraManager.DragCameraTo(itemPos);
    }

    private void ZoomCamera()
    {
        CameraManager.ZoomToMinValue();
    }
}