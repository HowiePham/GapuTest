using UnityEngine;

public class ItemDetailEffectHandler : GameEffectHandler
{
    private ItemDetailEffect _currentEffect;

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
        GameEventSystem.Subscribe<int>(EventName.ItemProgressUpdated, InitItemDetailEffect);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.ItemProgressUpdated, InitItemDetailEffect);
    }

    private void InitItemDetailEffect(int itemID)
    {
        var screenPosition = GetCurrentMousePosition();
        CreateGameEffectAt(screenPosition);
        _currentEffect.Init(itemID);
    }

    public override void CreateGameEffectAt(Vector2 position)
    {
        var newEffect = PoolingSystem.GetObjectFromPool(PoolType.ItemDetailEffect, position);
        _currentEffect = newEffect.GetComponent<ItemDetailEffect>();
    }

    public override void CreateGameEffect()
    {
        var newEffect = PoolingSystem.GetObjectFromPool(PoolType.ItemDetailEffect);
        _currentEffect = newEffect.GetComponent<ItemDetailEffect>();
    }
}