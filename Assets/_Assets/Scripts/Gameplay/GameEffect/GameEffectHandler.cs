using UnityEngine;

public abstract class GameEffectHandler : MonoBehaviour
{
    [SerializeField] protected GameEffectType gameEffectType;
    protected PoolingSystem PoolingSystem => SingletonManager.PoolingSystem;
    public abstract void CreateGameEffectAt(Vector2 position);
    public abstract void CreateGameEffectAt();

    public GameEffectType GetGameEffectType()
    {
        return gameEffectType;
    }
}