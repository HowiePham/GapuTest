using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : TemporaryMonoSingleton<MapManager>
{
    [SerializeField] private MapProgress mapProgress;
    [SerializeField] private List<MapController> mapList = new List<MapController>();

    protected override void Init()
    {
    }

    private void Start()
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
        GameEventSystem.Subscribe<int>(EventName.MapProgressChanged, UnlockNewMap);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.HiddenItemFound, IncreaseTotalFoundItem);
        GameEventSystem.Unsubscribe<int>(EventName.MapProgressChanged, UnlockNewMap);
    }

    private void UnlockNewMap(int mapID)
    {
        if (mapID >= mapList.Count) return;

        var newMapUnlocked = mapList[mapID];
        EnableMap(newMapUnlocked, true);

        mapProgress.UpdateNewMapProgress(newMapUnlocked);
    }

    private void EnableMap(MapController map, bool unlock)
    {
        map.UnlockMap(unlock);
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

    public int GetCurrentMapProgress()
    {
        return mapProgress.GetCurrentMapProgress();
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