using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private bool _collectionCheck = true;

    protected ObjectPool<T> Pool;
    protected List<T> _activeObjects = new List<T>();

    protected virtual void Awake()
    {
        InitializePool();
    }

    protected virtual void InitializePool()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => CreateObject(),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: _collectionCheck,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    protected virtual T CreateObject()
    {
        return Instantiate(_prefab);
    }

    protected virtual void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
        _activeObjects.Add(obj);
    }

    protected virtual void OnReleaseObject(T obj)
    {
        _activeObjects.Remove(obj);
        obj.gameObject.SetActive(false);
    }

    public T Get()
    {
        return Pool.Get();
    }

    public void Release(T obj)
    {
        Pool.Release(obj);
    }

    public void ReturnAllToPool()
    {
        for (int i = _activeObjects.Count - 1; i >= 0; i--)
        {
            if (_activeObjects[i] != null)
                Release(_activeObjects[i]);
        }

        _activeObjects.Clear();
    }

    public void ClearPool()
    {
        ReturnAllToPool();

        // Опционально: очистить внутренний пул Unity
        // Pool.Clear();
    }
}