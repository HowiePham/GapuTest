using UnityEngine;

public class HiddenItemVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer visualRenderer;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;

    public void InitVisual(int hiddenItemID)
    {
        if (visualRenderer == null) return;
        var templateVisual = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.HiddenItem, hiddenItemID);
        SetVisualToRenderer(templateVisual);
    }

    private void SetVisualToRenderer(Sprite itemVisual)
    {
        if (itemVisual == null) return;
        visualRenderer.sprite = itemVisual;
    }
}