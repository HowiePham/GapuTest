using System;
using UnityEngine;

public class SelfDestructEffect : MonoBehaviour
{
    [SerializeField] private float destructTime;
    private PoolingSystem PoolingSystem => SingletonManager.PoolingSystem;

    private void OnEnable()
    {
        Invoke(nameof(SelfDestruct), destructTime);
    }

    private void SelfDestruct()
    {
        PoolingSystem.ReturnObjectToPool(PoolType.RedMark, gameObject);
    }
}