using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : TemporaryMonoSingleton<PoolingSystem>
{
    [SerializeField] private List<Pool> poolList = new();
    private readonly Dictionary<PoolType, Pool> _poolDictionary = new();
    private readonly Dictionary<PoolType, Queue<GameObject>> _inActivePoolDictionary = new();

    protected override void Init()
    {
        InitPoolSystem();
    }

    private void InitPoolSystem()
    {
        foreach (var pool in poolList)
        {
            var poolType = pool.poolType;
            _poolDictionary.Add(poolType, pool);

            var poolSize = pool.poolSize;
            var poolObjQueue = NewObjectPoolQueue(poolType, poolSize);

            _inActivePoolDictionary.Add(poolType, poolObjQueue);
        }
    }

    private Queue<GameObject> NewObjectPoolQueue(PoolType poolType, int poolSize)
    {
        var objectStack = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            var obj = NewGameObject(poolType);
            objectStack.Enqueue(obj);
        }

        return objectStack;
    }

    public GameObject GetObjectFromPool(PoolType poolType)
    {
        GameObject newObj = null;
        if (CanGetObjectFromPool(poolType))
        {
            var poolObjQueue = _inActivePoolDictionary[poolType];
            newObj = poolObjQueue.Dequeue();
        }
        else
        {
            newObj = NewGameObject(poolType, true);
        }

        newObj.SetActive(true);


        return newObj;
    }

    public GameObject GetObjectFromPool(PoolType poolType, Transform parent = null)
    {
        var newObj = GetObjectFromPool(poolType);

        var newObjTransform = newObj.transform;
        if (parent != null) newObjTransform.SetParent(parent);

        return newObj;
    }

    public GameObject GetObjectFromPool(PoolType poolType, Vector3 position, Transform parent = null)
    {
        var newObj = GetObjectFromPool(poolType);

        var newObjTransform = newObj.transform;
        newObjTransform.position = position;
        if (parent != null) newObjTransform.SetParent(parent);

        return newObj;
    }

    public void ReturnObjectToPool(PoolType poolType, GameObject obj)
    {
        var poolObjQueue = _inActivePoolDictionary[poolType];
        obj.SetActive(false);
        poolObjQueue.Enqueue(obj);
    }

    private GameObject NewGameObject(PoolType poolType, bool active = false)
    {
        var pool = _poolDictionary[poolType];
        var poolPrefab = pool.objectPrefab;
        var poolParent = pool.parent;
        var obj = Instantiate(poolPrefab, poolParent, true);
        obj.SetActive(active);
        return obj;
    }

    private bool CanGetObjectFromPool(PoolType poolType)
    {
        var poolObjQueue = _inActivePoolDictionary[poolType];
        return poolObjQueue.Count > 0;
    }
}