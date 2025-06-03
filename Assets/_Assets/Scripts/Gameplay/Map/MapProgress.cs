using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapProgress
{
    [SerializeField] private int totalFoundItem;
    [SerializeField] private int totalItem;
    [SerializeField] private int currentMapProgress;
    [SerializeField] private ItemProgress itemProgress;

    public void Init(List<MapController> mapList)
    {
        for (var i = 0; i <= currentMapProgress; i++)
        {
            var map = mapList[i];
            UpdateNewMapProgress(map);
        }
    }

    public void UpdateNewMapProgress(MapController map)
    {
        itemProgress.UpdateAllHiddenItemInMap(map);
        UpdateNewTotalItemValue(map);
        InvokeMapProgressUpdated();
    }

    private void UpdateNewTotalItemValue(MapController map)
    {
        var hiddenItemList = map.GetAllHiddenItemInMap();
        totalItem += hiddenItemList.Count;
    }

    public void IncreaseTotalFoundItem(int itemID)
    {
        totalFoundItem++;
        itemProgress.IncreaseFoundItem(itemID);

        CheckMapProgress();
    }

    private void CheckMapProgress()
    {
        if (CanIncreaseMapProgress()) return;

        IncreaseMapProgress();
    }

    private bool CanIncreaseMapProgress()
    {
        return totalFoundItem < totalItem;
    }

    private void IncreaseMapProgress()
    {
        currentMapProgress++;
        InvokeMapProgressChanged();
    }

    private void InvokeMapProgressChanged()
    {
        GameEventSystem.Invoke(EventName.MapProgressChanged, currentMapProgress);
    }

    private void InvokeMapProgressUpdated()
    {
        GameEventSystem.Invoke(EventName.MapProgressUpdated);
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

    public Dictionary<int, List<HiddenItem>> GetAllItemInMap()
    {
        return itemProgress.GetAllItemInMap();
    }

    public Dictionary<int, int> GetItemList()
    {
        return itemProgress.GetItemList();
    }

    public int GetNumberOfFoundItem(int itemID)
    {
        return itemProgress.GetNumberOfFoundItem(itemID);
    }

    public int GetNumberOfItem(int itemID)
    {
        return itemProgress.GetNumberOfItem(itemID);
    }
}