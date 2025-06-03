using System.Collections.Generic;
using UnityEngine;

public class HiddenItemUIPanel : MonoBehaviour
{
    [SerializeField] private HiddenItemUI hiddenItemUIPrefab;
    [SerializeField] private int testQuantity;
    private Dictionary<int, HiddenItemUI> _hiddenItemUIList = new Dictionary<int, HiddenItemUI>();
    private MapManager MapManager => SingletonManager.MapManager;

    private void Awake()
    {
        ListenEvent();
    }

    private void OnDestroy()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<int>(EventName.ItemProgressUpdated, UpdateHiddenItemUI);
        GameEventSystem.Subscribe(EventName.MapProgressUpdated, UpdateUI);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.ItemProgressUpdated, UpdateHiddenItemUI);
        GameEventSystem.Unsubscribe(EventName.MapProgressUpdated, UpdateUI);
    }

    private void UpdateUI()
    {
        var allItemInMap = MapManager.GetItemList();

        foreach (var item in allItemInMap)
        {
            var itemID = item.Key;

            if (_hiddenItemUIList.ContainsKey(itemID))
            {
                var hiddenItemUI = GetHiddenItemUI(itemID);
                hiddenItemUI.UpdateItemQuantityText();
                continue;
            }

            NewHiddenItemUI(itemID);
        }
    }

    private void NewHiddenItemUI(int itemID)
    {
        var newItemUI = Instantiate(hiddenItemUIPrefab, transform);
        newItemUI.InitUI(itemID);
        _hiddenItemUIList.Add(itemID, newItemUI);
    }

    private void UpdateHiddenItemUI(int itemID)
    {
        var hiddenItemUI = GetHiddenItemUI(itemID);
        hiddenItemUI.UpdateItemQuantityText();
    }

    private HiddenItemUI GetHiddenItemUI(int itemID)
    {
        return !_hiddenItemUIList.ContainsKey(itemID) ? null : _hiddenItemUIList[itemID];
    }
}