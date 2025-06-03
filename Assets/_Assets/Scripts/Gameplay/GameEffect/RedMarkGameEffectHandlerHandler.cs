using UnityEngine;

public class RedMarkGameEffectHandlerHandler : GameEffectHandler
{
    public override void CreateGameEffectAt(Vector2 position)
    {
        PoolingSystem.GetObjectFromPool(PoolType.RedMark, position);
    }
}