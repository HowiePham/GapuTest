using UnityEngine;
using UnityEngine.UI;

public class HiddenItemUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image hiddenItemImage;
    [SerializeField] private Text hiddenItemQuantity;
    [SerializeField] private HiddenItemInfo hiddenItemInfo;
    public int ItemID => hiddenItemInfo.ItemID;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;
    private MapManager MapManager => SingletonManager.MapManager;

    public void InitUI(int itemID)
    {
        hiddenItemInfo.ItemID = itemID;
        SetItemImage();
        UpdateItemQuantityText();
    }

    private void SetItemImage()
    {
        var itemID = hiddenItemInfo.ItemID;
        var visualSprite = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.HiddenItem, itemID);
        hiddenItemImage.sprite = visualSprite;
    }

    public void UpdateItemQuantityText()
    {
        var totalItemCount = MapManager.GetNumberOfItem(ItemID);
        var totalFoundItemCount = MapManager.GetNumberOfFoundItem(ItemID);

        hiddenItemQuantity.text = totalFoundItemCount + GameString.Slash + totalItemCount;
    }

    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }
}