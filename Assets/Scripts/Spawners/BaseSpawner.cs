using UnityEngine;

namespace Spawners
{
    public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected ObjectPooler<T> _objectPooler;

        protected virtual void OnValidate()
        {
            if (_objectPooler == null)
                _objectPooler = GetComponent<ObjectPooler<T>>();
        }

        protected T GetFromPool()
        {
            if (_objectPooler == null)
            {
                Debug.LogError($"ObjectPooler not assigned on {gameObject.name}");
                return null;
            }

            return _objectPooler.Get();
        }

        protected void ReleaseToPool(T obj)
        {
            if (_objectPooler != null && obj != null)
                _objectPooler.Release(obj);
        }

        public void ReturnAllToPool()
        {
            if (_objectPooler != null)
                _objectPooler.ReturnAllToPool();
        }

        public void SetObjectPooler(ObjectPooler<T> pooler)
        {
            _objectPooler = pooler;
        }
    }
}