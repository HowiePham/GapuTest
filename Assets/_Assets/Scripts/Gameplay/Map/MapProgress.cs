using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapProgress
{
    [SerializeField] private int totalFoundItem;
    [SerializeField] private int totalItem;
    [SerializeField] private int currentMapProgress;
    private Dictionary<int, List<HiddenItem>> _allItemInMap = new Dictionary<int, List<HiddenItem>>();
    private Dictionary<int, int> _numberOfItem = new Dictionary<int, int>();
    private Dictionary<int, int> _numberOfFoundItem = new Dictionary<int, int>();

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
            var hiddenItemList = _allItemInMap[i];
            totalItem += hiddenItemList.Count;
        }
    }

    private void InitAllHiddenItem(List<MapController> mapList)
    {
        foreach (var map in mapList)
        {
            var hiddenItemInMap = map.GetAllHiddenItemInMap();
            var mapID = map.MapID;

            _allItemInMap[mapID] = hiddenItemInMap;

            InitNumberOfHiddenItemInMap(hiddenItemInMap);
        }
    }

    private void InitNumberOfHiddenItemInMap(List<HiddenItem> hiddenItemInMap)
    {
        foreach (var hiddenItem in hiddenItemInMap)
        {
            var itemCount = 0;
            var itemID = hiddenItem.HiddenItemID;
            if (_numberOfItem.ContainsKey(itemID)) itemCount = _numberOfItem[itemID];
            itemCount++;
            _numberOfItem[itemID] = itemCount;
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

    public void IncreaseTotalFoundItem(int itemID)
    {
        totalFoundItem++;

        IncreaseFoundItem(itemID);
        CheckMapProgress();
    }

    private void IncreaseFoundItem(int itemID)
    {
        var itemCount = 0;
        if (_numberOfFoundItem.ContainsKey(itemID)) itemCount = _numberOfItem[itemID];
        itemCount++;
        _numberOfFoundItem[itemID] = itemCount;
    }

    public Dictionary<int, List<HiddenItem>> GetAllItemInMap()
    {
        return _allItemInMap;
    }

    public Dictionary<int, int> GetItemList()
    {
        return _numberOfItem;
    }

    public int GetNumberOfFoundItem(int itemID)
    {
        var firstValue = 0;
        return _numberOfFoundItem.ContainsKey(itemID) ? _numberOfFoundItem[itemID] : firstValue;
    }

    public int GetNumberOfItem(int itemID)
    {
        return _numberOfItem[itemID];
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