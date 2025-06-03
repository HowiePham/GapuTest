using UnityEngine;

public abstract class GameEffectHandler : MonoBehaviour
{
    [SerializeField] protected GameObject effectPrefab;
    [SerializeField] protected GameEffectType gameEffectType;

    public abstract void CreateGameEffectAt(Vector2 position);

    public GameEffectType GetGameEffectType()
    {
        return gameEffectType;
    }
}