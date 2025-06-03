using UnityEngine;

public class MagnifyingGlassTool : GameTool
{
    [SerializeField] private Camera mainCamera;

    private MapManager MapManager => SingletonManager.MapManager;
    private CameraManager CameraManager => SingletonManager.CameraManager;

    public override void Execute()
    {
        FindHiddenItem();
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
        var mainCameraTransform = mainCamera.transform;
        var oldCamPos = mainCameraTransform.position;
        mainCameraTransform.position = new Vector3(itemPos.x, itemPos.y, oldCamPos.z);
    }

    private void ZoomCamera()
    {
        CameraManager.ZoomToMinValue();
    }
}