using UnityEngine;

public class FoundItemEffectHandler : GameEffectHandler
{
    public override void CreateGameEffectAt(Vector2 position)
    {
        var effect = PoolingSystem.GetObjectFromPool(PoolType.FoundItemEffect, position);
    }
}