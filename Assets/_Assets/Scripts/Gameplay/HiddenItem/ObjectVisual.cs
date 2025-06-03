using UnityEngine;

public class ObjectVisual : MonoBehaviour
{
    [SerializeField] private InGameVisualType inGameVisualType;
    [SerializeField] private SpriteRenderer visualRenderer;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;

    private void Awake()
    {
        InitSystem();
    }

    private void InitSystem()
    {
        visualRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitVisual(int itemID)
    {
        if (visualRenderer == null) return;
        var templateVisual = InGameVisualHandler.GetVisualSpriteByID(inGameVisualType, itemID);
        SetVisualToRenderer(templateVisual);
    }

    private void SetVisualToRenderer(Sprite itemVisual)
    {
        if (itemVisual == null) return;
        visualRenderer.sprite = itemVisual;
    }
}