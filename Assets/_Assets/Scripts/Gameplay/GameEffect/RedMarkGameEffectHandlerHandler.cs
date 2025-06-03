using UnityEngine;

public class RedMarkGameEffectHandlerHandler : GameEffectHandler
{
    public override void CreateGameEffectAt(Vector2 position)
    {
        Instantiate(effectPrefab, position, Quaternion.identity);
    }
}