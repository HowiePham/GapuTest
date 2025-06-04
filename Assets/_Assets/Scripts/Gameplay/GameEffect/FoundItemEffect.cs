using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FoundItemEffect : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private float flyingTime;
    private InGameVisualHandler InGameVisualHandler => SingletonManager.InGameVisualHandler;
    private PoolingSystem PoolingSystem => SingletonManager.PoolingSystem;

    private void OnDisable()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<RectTransform>(EventName.HiddenPanelUpdated, HandleRunningEffect);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<RectTransform>(EventName.HiddenPanelUpdated, HandleRunningEffect);
    }

    public void InitEffect(int itemID)
    {
        ListenEvent();
        InitImage(itemID);
    }

    private void InitImage(int itemID)
    {
        var itemSprite = InGameVisualHandler.GetVisualSpriteByID(InGameVisualType.HiddenItem, itemID);
        itemImage.sprite = itemSprite;
    }

    private void HandleRunningEffect(RectTransform hiddenItemUI)
    {
        StartCoroutine(RunFlyingEffect(hiddenItemUI));
    }

    private IEnumerator RunFlyingEffect(RectTransform hiddenItemUI)
    {
        float elapsedTime = 0;
        var oldPos = transform.position;

        while (elapsedTime < flyingTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(oldPos, hiddenItemUI.position, elapsedTime / flyingTime);

            yield return null;
        }

        SelfDestroy();
    }

    private void SelfDestroy()
    {
        PoolingSystem.ReturnObjectToPool(PoolType.FoundItemEffect, gameObject);
    }
}