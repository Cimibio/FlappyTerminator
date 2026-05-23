using UnityEngine;

namespace Spawners
{
    public abstract class CommandSpawner<T> : BaseSpawner<T> where T : MonoBehaviour
    {
        [SerializeField] protected Transform _spawnPoint;

        public virtual void SpawnAtSpawnPoint()
        {
            if (_spawnPoint == null)
            {
                Debug.LogError($"Spawn point not set on {gameObject.name}");
                return;
            }

            SpawnAtPosition(_spawnPoint.position);
        }

        public virtual void SpawnAtPosition(Vector3 position)
        {
            T obj = GetFromPool();

            if (obj == null) 
                return;

            obj.transform.position = position;
            OnObjectSpawned(obj);
        }

        protected abstract void OnObjectSpawned(T obj);

        public virtual void Reset()
        {
            ReturnAllToPool();
        }
    }
}