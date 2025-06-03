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
        itemProgress.InitAllHiddenItem(mapList);
        GetNewTotalItem();
    }

    private void GetNewTotalItem()
    {
        totalItem = 0;
        var allItemInMap = GetAllItemInMap();
        for (var i = 0; i <= currentMapProgress; i++)
        {
            var hiddenItemList = allItemInMap[i];
            totalItem += hiddenItemList.Count;
        }
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
        GameEventSystem.Invoke(EventName.MapProgressChanged);
    }

    public void IncreaseTotalFoundItem(int itemID)
    {
        totalFoundItem++;

        itemProgress.IncreaseFoundItem(itemID);
        CheckMapProgress();
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