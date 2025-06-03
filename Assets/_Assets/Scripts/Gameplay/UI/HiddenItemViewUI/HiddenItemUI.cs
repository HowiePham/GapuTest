using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenItemUI : MonoBehaviour
{
    [SerializeField] private Image hiddenItemImage;
    [SerializeField] private Text hiddenItemQuantity;
    [SerializeField] private HiddenItemInfo hiddenItemInfo;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;

    public void InitUI(int itemID)
    {
        hiddenItemInfo.ItemID = itemID;
        SetItemImage();
    }

    private void SetItemImage()
    {
        var itemID = hiddenItemInfo.ItemID;
        var visualSprite = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.HiddenItem, itemID);
        hiddenItemImage.sprite = visualSprite;
    }

    private void SetItemQuantityText()
    {
    }
}