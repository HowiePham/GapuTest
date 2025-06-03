using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenItemUIPanel : MonoBehaviour
{
    [SerializeField] private HiddenItemUI hiddenItemUIPrefab;
    [SerializeField] private int testQuantity;
    [SerializeField] private List<HiddenItemUI> hiddenItemUiList = new List<HiddenItemUI>();

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        hiddenItemUiList.Clear();
        for (int i = 0; i < testQuantity; i++)
        {
            var itemID = i;
            var newHiddenItemUI = NewHiddenItemUI(itemID);
            hiddenItemUiList.Add(newHiddenItemUI);
        }
    }

    private HiddenItemUI NewHiddenItemUI(int itemID)
    {
        var newItemUI = Instantiate(hiddenItemUIPrefab, transform);
        newItemUI.InitUI(itemID);
        return newItemUI;
    }
}