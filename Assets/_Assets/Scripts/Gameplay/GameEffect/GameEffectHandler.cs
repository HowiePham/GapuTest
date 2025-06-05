using System;
using UnityEngine;

public abstract class GameEffectHandler : MonoBehaviour
{
    [SerializeField] protected GameEffectType gameEffectType;
    private Camera _mainCamera;
    protected PoolingSystem PoolingSystem => SingletonManager.PoolingSystem;

    protected void Awake()
    {
        InitSystem();
    }

    protected virtual void InitSystem()
    {
        _mainCamera = Camera.main;
    }

    public abstract void CreateGameEffectAt(Vector2 position);
    public abstract void CreateGameEffect();

    protected Vector3 GetCurrentMousePosition()
    {
        var screenPosition = Input.mousePosition;
        screenPosition.z = _mainCamera.nearClipPlane;
        return screenPosition;
    }

    public GameEffectType GetGameEffectType()
    {
        return gameEffectType;
    }
}