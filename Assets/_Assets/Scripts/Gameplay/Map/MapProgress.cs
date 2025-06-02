using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapProgress
{
    [SerializeField] private int totalFoundItem;
    [SerializeField] private int totalItem;
    [SerializeField] private int currentMapProgress;
    private Dictionary<int, List<HiddenItem>> _allHiddenItemInMap = new Dictionary<int, List<HiddenItem>>();

    public void Init(List<MapController> mapList)
    {
        InitAllHiddenItem(mapList);
        GetNewTotalItem();
    }

    private void GetNewTotalItem()
    {
        totalItem = 0;
        for (var i = 0; i <= currentMapProgress; i++)
        {
            var hiddenItemList = _allHiddenItemInMap[i];
            totalItem += hiddenItemList.Count;
        }
    }

    private void InitAllHiddenItem(List<MapController> mapList)
    {
        foreach (var map in mapList)
        {
            var mapHiddenItem = map.GetAllHiddenItemInMap();
            var mapID = map.MapID;

            _allHiddenItemInMap[mapID] = mapHiddenItem;
        }
    }

    private void CheckMapProgress()
    {
        if (totalFoundItem < totalItem) return;

        IncreaseMapProgress();
    }

    private void IncreaseMapProgress()
    {
        currentMapProgress++;
        InvokeMapProgressChanged();
    }

    private void InvokeMapProgressChanged()
    {
        GameEventSystem.Invoke(EventName.MapProgressChanged);
    }

    public void IncreaseTotalFoundItem()
    {
        totalFoundItem++;
    }

    public int GetTotalFoundItem()
    {
        return totalFoundItem;
    }

    public int GetCurrentMapProgress()
    {
        return currentMapProgress;
    }

    public int GetTotalItem()
    {
        return totalItem;
    }
}