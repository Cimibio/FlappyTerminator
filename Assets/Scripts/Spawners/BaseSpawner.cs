using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected ObjectPooler<T> _objectPooler;

        protected List<T> _activeObjects = new List<T>();

        protected virtual void OnValidate()
        {
            if (_objectPooler == null)
                _objectPooler = GetComponent<ObjectPooler<T>>();
        }

        public void ReturnAllToPool()
        {
            if (_objectPooler != null)
            {
                foreach (var item in _activeObjects)
                {
                    UnsubscribeFromEvents(item);
                }

                _objectPooler.ReturnAllToPool();
            }

            _activeObjects.Clear();
        }

        protected T GetFromPool()
        {
            T obj = _objectPooler.Get();

            if (obj != null)
            {
                _activeObjects.Add(obj);
                SubscribeToEvents(obj);
            }

            return obj;
        }

        protected void ReleaseToPool(T obj)
        {
            if (_objectPooler != null && obj != null)
            {
                UnsubscribeFromEvents(obj);
                _objectPooler.Release(obj);
            }
        }

        protected abstract void SpawnObject();

        protected abstract void UnsubscribeFromEvents(T obj);

        protected abstract void SubscribeToEvents(T obj);
    }
}