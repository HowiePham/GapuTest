using UnityEngine;

public class ItemFoundEffectHandler : GameEffectHandler
{
    private FoundItemEffect _currentEffect;
    
    protected override void InitSystem()
    {
        base.InitSystem();
        ListenEvent();
    }

    private void OnDestroy()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<int>(EventName.HiddenItemFound, InitFoundEffect);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.HiddenItemFound, InitFoundEffect);
    }

    private void InitFoundEffect(int itemID)
    {
        var screenPosition = GetCurrentMousePosition();
        CreateGameEffectAt(screenPosition);
        _currentEffect.InitEffect(itemID);
    }

    public override void CreateGameEffectAt(Vector2 position)
    {
        var newEffect = PoolingSystem.GetObjectFromPool(PoolType.FoundItemEffect, position);
        _currentEffect = newEffect.GetComponent<FoundItemEffect>();
    }

    public override void CreateGameEffect()
    {
        var newEffect = PoolingSystem.GetObjectFromPool(PoolType.FoundItemEffect);
        _currentEffect = newEffect.GetComponent<FoundItemEffect>();
    }
}