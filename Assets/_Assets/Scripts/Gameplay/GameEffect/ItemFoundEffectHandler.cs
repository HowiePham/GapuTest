using System;
using UnityEngine;

public class ItemFoundEffectHandler : GameEffectHandler
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private FoundItemEffect currentEffect;

    private void Start()
    {
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
        CreateGameEffectAt();
        currentEffect.InitEffect(itemID);
        HandleEffect();
    }

    private void HandleEffect()
    {
        var currentEffectTransform = currentEffect.transform;
        currentEffectTransform.position = mainCamera.WorldToScreenPoint(new Vector3(0, 0, 0));
    }

    public override void CreateGameEffectAt(Vector2 position)
    {
        var newEffect = PoolingSystem.GetObjectFromPool(PoolType.FoundItemEffect, position);
        currentEffect = newEffect.GetComponent<FoundItemEffect>();
    }

    public override void CreateGameEffectAt()
    {
        var newEffect = PoolingSystem.GetObjectFromPool(PoolType.FoundItemEffect);
        currentEffect = newEffect.GetComponent<FoundItemEffect>();
    }
}