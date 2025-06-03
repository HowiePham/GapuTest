using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : TemporaryMonoSingleton<MapManager>
{
    [SerializeField] private MapProgress mapProgress;
    [SerializeField] private List<MapController> mapList = new List<MapController>();

    protected override void Init()
    {
        ListenEvent();
        InitSystem();
    }

    private void InitSystem()
    {
        InitMapList();
        mapProgress.Init(mapList);
    }

    private void OnDisable()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<int>(EventName.HiddenItemFound, IncreaseTotalFoundItem);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.HiddenItemFound, IncreaseTotalFoundItem);
    }

    private void IncreaseTotalFoundItem(int itemID)
    {
        mapProgress.IncreaseTotalFoundItem(itemID);
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

    public int GetTotalFoundItem()
    {
        return mapProgress.GetTotalFoundItem();
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