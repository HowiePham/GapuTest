using UnityEngine;
using UnityEngine.UI;

public class HiddenItemUI : MonoBehaviour
{
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
        SetItemQuantityText();
    }

    private void SetItemImage()
    {
        var itemID = hiddenItemInfo.ItemID;
        var visualSprite = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.HiddenItem, itemID);
        hiddenItemImage.sprite = visualSprite;
    }

    public void SetItemQuantityText()
    {
        var totalItemCount = MapManager.GetNumberOfItem(ItemID);
        var totalFoundItemCount = MapManager.GetNumberOfFoundItem(ItemID);

        hiddenItemQuantity.text = totalFoundItemCount + "/" + totalItemCount;
    }
}