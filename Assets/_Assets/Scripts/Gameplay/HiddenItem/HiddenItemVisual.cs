using UnityEngine;

public class HiddenItemVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer visualRenderer;
    private HiddenItemVisualManager HiddenItemVisualManager => SingletonManager.HiddenItemVisualManager;

    public void InitVisual(int hiddenItemID)
    {
        if (visualRenderer == null) return;
        var templateVisual = HiddenItemVisualManager.GetHiddenItemVisual(hiddenItemID);
        SetVisualToRenderer(templateVisual);
    }

    private void SetVisualToRenderer(Sprite itemVisual)
    {
        if (itemVisual == null) return;
        visualRenderer.sprite = itemVisual;
    }
}