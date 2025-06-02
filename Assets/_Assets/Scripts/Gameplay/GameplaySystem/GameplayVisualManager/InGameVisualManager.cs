using UnityEngine;

public abstract class InGameVisualManager : MonoBehaviour
{
    [SerializeField] protected InGameVisualType inGameVisualType;
    [SerializeField] protected BaseTemplate<Sprite> visualTemplate;

    public InGameVisualType GetInGameVisualType()
    {
        return inGameVisualType;
    }

    public abstract Sprite GetVisualTemplateBy(int itemID);
}