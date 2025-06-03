using System;
using System.Collections.Generic;

[Serializable]
public class ItemProgress
{
    private Dictionary<int, List<HiddenItem>> _allItemInMap = new Dictionary<int, List<HiddenItem>>();
    private Dictionary<int, int> _numberOfItem = new Dictionary<int, int>();
    private Dictionary<int, int> _numberOfFoundItem = new Dictionary<int, int>();

    public void IncreaseFoundItem(int itemID)
    {
        var itemCount = 0;
        if (_numberOfFoundItem.ContainsKey(itemID)) itemCount = _numberOfFoundItem[itemID];
        itemCount++;
        _numberOfFoundItem[itemID] = itemCount;

        InvokeUpdatingItemProgress(itemID);
    }

    private void InvokeUpdatingItemProgress(int itemID)
    {
        GameEventSystem.Invoke(EventName.ItemProgressUpdated, itemID);
    }

    private void UpdateNumberOfHiddenItemInMap(List<HiddenItem> hiddenItemInMap)
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

    public void UpdateAllHiddenItemInMap(List<MapController> mapList)
    {
        foreach (var map in mapList)
        {
            var hiddenItemInMap = map.GetAllHiddenItemInMap();
            var mapID = map.MapID;

            _allItemInMap[mapID] = hiddenItemInMap;

            UpdateNumberOfHiddenItemInMap(hiddenItemInMap);
        }
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
}