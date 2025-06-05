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

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<int>(EventName.HiddenItemFound, IncreaseTotalFoundItem);
        GameEventSystem.Subscribe<MapController>(EventName.UnlockNewMap, UpdateNewMapProgress);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.HiddenItemFound, IncreaseTotalFoundItem);
        GameEventSystem.Unsubscribe<MapController>(EventName.UnlockNewMap, UpdateNewMapProgress);
    }

    public void Destruct()
    {
        StopListeningEvent();
    }

    public void Init(List<MapController> mapList)
    {
        ListenEvent();
        itemProgress.UpdateAllHiddenItemInMap(mapList);

        for (var i = 0; i <= currentMapProgress; i++)
        {
            var map = mapList[i];
            UpdateNewMapProgress(map);
        }
    }

    public void UpdateNewMapProgress(MapController map)
    {
        UpdateNewTotalItemValue(map);
        GameEventSystem.Invoke(EventName.MapProgressUpdated);
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
        GameEventSystem.Invoke(EventName.MapProgressChanged, currentMapProgress);
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