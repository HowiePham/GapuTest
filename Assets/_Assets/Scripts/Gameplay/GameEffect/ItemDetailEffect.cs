using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDetailEffect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text quantityText;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;
    private MapManager MapManager => SingletonManager.MapManager;

    public void Init(int itemID)
    {
        ListenEvent();
        SetItemImage(itemID);
        UpdateText(itemID);
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe(EventName.FirstTouch, DisableEffect);
        GameEventSystem.Subscribe(EventName.MultiTouch, DisableEffect);
        GameEventSystem.Subscribe(EventName.UnlockNewMap, DisableEffect);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe(EventName.FirstTouch, DisableEffect);
        GameEventSystem.Unsubscribe(EventName.MultiTouch, DisableEffect);
        GameEventSystem.Unsubscribe(EventName.UnlockNewMap, DisableEffect);
    }

    private void OnDisable()
    {
        StopListeningEvent();
    }

    private void DisableEffect()
    {
        gameObject.SetActive(false);
    }

    private void SetItemImage(int itemID)
    {
        var visualSprite = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.HiddenItem, itemID);
        itemImage.sprite = visualSprite;
    }

    private void UpdateText(int itemID)
    {
        var totalItemCount = MapManager.GetNumberOfItem(itemID);
        var totalFoundItemCount = MapManager.GetNumberOfFoundItem(itemID);

        quantityText.text = totalFoundItemCount + GameString.Slash + totalItemCount;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DisableEffect();
    }
}