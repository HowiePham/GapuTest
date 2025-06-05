using UnityEngine;

public class RedMarkGameEffectHandler : GameEffectHandler
{
    public override void CreateGameEffectAt(Vector2 position)
    {
        PoolingSystem.GetObjectFromPool(PoolType.RedMarkEffect, position);
    }

    public override void CreateGameEffect()
    {
        PoolingSystem.GetObjectFromPool(PoolType.RedMarkEffect);
    }
}