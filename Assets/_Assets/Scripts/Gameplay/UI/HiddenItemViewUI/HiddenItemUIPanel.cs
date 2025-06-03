using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HiddenItemUIPanel : MonoBehaviour
{
    [SerializeField] private HiddenItemUI hiddenItemUIPrefab;
    [SerializeField] private int testQuantity;
    [SerializeField] private List<HiddenItemUI> hiddenItemUIList = new List<HiddenItemUI>();
    private MapManager MapManager => SingletonManager.MapManager;

    private void Start()
    {
        ListenEvent();
        InitUI();
    }

    private void OnDestroy()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<int>(EventName.UpdatingItemProgress, UpdateHiddenItemUI);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.UpdatingItemProgress, UpdateHiddenItemUI);
    }

    private void InitUI()
    {
        var allItemInMap = MapManager.GetItemList();
        hiddenItemUIList.Clear();

        foreach (var item in allItemInMap)
        {
            var itemID = item.Key;
            var newHiddenItemUI = NewHiddenItemUI(itemID);
            hiddenItemUIList.Add(newHiddenItemUI);
        }
    }

    private HiddenItemUI NewHiddenItemUI(int itemID)
    {
        var newItemUI = Instantiate(hiddenItemUIPrefab, transform);
        newItemUI.InitUI(itemID);
        return newItemUI;
    }

    private void UpdateHiddenItemUI(int itemID)
    {
        var hiddenItemUI = hiddenItemUIList[itemID];
        hiddenItemUI.UpdateItemQuantityText();
    }
}