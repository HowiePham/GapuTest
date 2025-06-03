using UnityEngine;

public abstract class GameEffectHandler : MonoBehaviour
{
    [SerializeField] protected GameEffectType gameEffectType;
    protected PoolingSystem PoolingSystem => SingletonManager.PoolingSystem;
    public abstract void CreateGameEffectAt(Vector2 position);

    public GameEffectType GetGameEffectType()
    {
        return gameEffectType;
    }
}