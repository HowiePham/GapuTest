using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : TemporaryMonoSingleton<MapManager>
{
    [SerializeField] private MapProgress mapProgress;
    [SerializeField] private List<MapController> mapList = new List<MapController>();

    private void Reset()
    {
        InitMapList();
    }

    protected override void Init()
    {
        ListenEvent();
    }

    private void Start()
    {
        InitSystem();
    }

    private void InitSystem()
    {
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
        mapProgress.IncreaseTotalFoundItem();
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

    public int GetTotalFoundItem()
    {
        return mapProgress.GetTotalFoundItem();
    }

    public int GetTotalItem()
    {
        return mapProgress.GetTotalItem();
    }
}