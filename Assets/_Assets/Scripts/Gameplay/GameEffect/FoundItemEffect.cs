using UnityEngine;
using UnityEngine.UI;

public class FoundItemEffect : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;

    public void InitEffect(int itemID)
    {
        InitImage(itemID);
    }

    private void InitImage(int itemID)
    {
        var itemSprite = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.HiddenItem, itemID);
        itemImage.sprite = itemSprite;
    }
}