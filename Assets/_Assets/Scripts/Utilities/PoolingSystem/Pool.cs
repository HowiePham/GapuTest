using System;
using UnityEngine;

[Serializable]
public class Pool
{
    public PoolType poolType;
    public int poolSize;
    public GameObject objectPrefab;
    public Transform parent;
}