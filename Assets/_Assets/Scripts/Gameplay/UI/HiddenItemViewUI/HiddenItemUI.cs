using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HiddenItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image hiddenItemImage;
    [SerializeField] private GameObject tickImage;
    [SerializeField] private Text hiddenItemQuantity;
    [SerializeField] private HiddenItemInfo hiddenItemInfo;
    public int ItemID => hiddenItemInfo.ItemID;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;
    private MapManager MapManager => SingletonManager.MapManager;

    public void InitUI(int itemID)
    {
        hiddenItemInfo.ItemID = itemID;
        SetItemImage();
        UpdateItemQuantityUI();
    }

    private void SetItemImage()
    {
        var itemID = hiddenItemInfo.ItemID;
        var visualSprite = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.HiddenItem, itemID);
        hiddenItemImage.sprite = visualSprite;
    }

    public void UpdateItemQuantityUI()
    {
        if (!IsFinishedItem())
        {
            EnableTickImage(false);
            UpdateText();
            return;
        }

        EnableTickImage(true);
        GameEventSystem.Invoke(EventName.FinishHiddenItemUI, transform);
    }

    private void EnableTickImage(bool state)
    {
        tickImage.SetActive(state);
        EnableText(!state);
    }

    private void EnableText(bool state)
    {
        var textObj = hiddenItemQuantity.gameObject;
        textObj.SetActive(state);
    }

    private void UpdateText()
    {
        var totalItemCount = MapManager.GetNumberOfItem(ItemID);
        var totalFoundItemCount = MapManager.GetNumberOfFoundItem(ItemID);

        hiddenItemQuantity.text = totalFoundItemCount + GameString.Slash + totalItemCount;
    }

    private bool IsFinishedItem()
    {
        var totalItemCount = MapManager.GetNumberOfItem(ItemID);
        var totalFoundItemCount = MapManager.GetNumberOfFoundItem(ItemID);

        return totalItemCount == totalFoundItemCount;
    }

    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameEventSystem.Invoke(EventName.HiddenItemUIClicked, this);
    }
}