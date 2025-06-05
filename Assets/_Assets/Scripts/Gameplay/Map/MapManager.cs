using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : TemporaryMonoSingleton<MapManager>
{
    [SerializeField] private MapProgress mapProgress;
    [SerializeField] private List<MapController> mapList = new List<MapController>();
    private CameraManager CameraManager => SingletonManager.CameraManager;

    private void Start()
    {
        InitSystem();
    }

    private void InitSystem()
    {
        InitMapList();
        mapProgress.Init(mapList);
    }

    private void OnDisable()
    {
        mapProgress.Destruct();
    }

    private void InitMapList()
    {
        mapList.Clear();
        foreach (Transform obj in transform)
        {
            var mapController = obj.GetComponent<MapController>();
            if (mapController == null) return;

            mapList.Add(mapController);
        }
    }

    public void GoToCurrentMap()
    {
        var currentMap = GetCurrentMap();
        var mapTransform = currentMap.transform;
        var mapPos = mapTransform.position;
        CameraManager.DragCameraTo(mapPos);
    }

    public MapController GetMapController(int mapID)
    {
        return mapList[mapID];
    }

    public Dictionary<int, List<HiddenItem>> GetAllItemInMap()
    {
        return mapProgress.GetAllItemInMap();
    }

    public Dictionary<int, int> GetItemList()
    {
        return mapProgress.GetItemList();
    }

    public List<MapController> GetMapList()
    {
        return mapList;
    }

    public int GetCurrentMapProgress()
    {
        return mapProgress.GetCurrentMapProgress();
    }

    public int GetTotalFoundItem()
    {
        return mapProgress.GetTotalFoundItem();
    }

    public MapController GetCurrentMap()
    {
        var currentProgress = GetCurrentMapProgress();
        return mapList[currentProgress];
    }

    public int GetTotalItem()
    {
        return mapProgress.GetTotalItem();
    }

    public int GetNumberOfFoundItem(int itemID)
    {
        return mapProgress.GetNumberOfFoundItem(itemID);
    }

    public int GetNumberOfItem(int itemID)
    {
        return mapProgress.GetNumberOfItem(itemID);
    }
}