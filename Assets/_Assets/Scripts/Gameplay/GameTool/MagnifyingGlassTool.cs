using UnityEngine;

public class MagnifyingGlassTool : GameTool
{
    private CameraManager CameraManager => SingletonManager.CameraManager;

    public override void Execute()
    {
        FindHiddenItem();
        ConsumeTool();
        InvokeUsingToolCompleted();
    }

    private void FindHiddenItem()
    {
        var hiddenItemList = GetAllHiddenItemInMap();

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