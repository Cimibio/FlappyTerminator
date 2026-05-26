using UnityEngine;

namespace Spawners
{
    public abstract class CommandSpawner<T> : BaseSpawner<T> where T : MonoBehaviour
    {
        [SerializeField] protected Transform _spawnPoint;

        public virtual void Reset()
        {
            ReturnAllToPool();
        }

        public virtual void SpawnAtSpawnPoint()
        {
            if (_spawnPoint == null)
            {
                Debug.LogError($"Spawn point not set on {gameObject.name}");
                return;
            }

            SpawnObject();
        }

        protected override void SpawnObject()
        {
            T obj = GetFromPool();

            if (obj == null)
                return;

            OnObjectSpawned(obj);
        }

        protected virtual void OnObjectSpawned(T obj) 
        {
            obj.transform.position = _spawnPoint.position;
        }
    }
}