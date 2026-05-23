using System.Collections;
using UnityEngine;

namespace Spawners
{
    public abstract class PeriodicSpawner<T> : BaseSpawner<T> where T : MonoBehaviour
    {
        [SerializeField] protected float _startDelay = 0.5f;
        [SerializeField] protected float _repeatRate = 1f;

        protected bool _isSpawning = false;
        protected Coroutine _spawnCoroutine;
        protected WaitForSeconds _startDelayWait;
        protected WaitForSeconds _repeatRateWait;

        protected virtual void Start()
        {
            _startDelayWait = new WaitForSeconds(_startDelay);
            _repeatRateWait = new WaitForSeconds(_repeatRate);
        }

        protected abstract void SpawnObject();

        protected virtual IEnumerator SpawnRoutine()
        {
            yield return _startDelayWait;

            while (_isSpawning)
            {
                SpawnObject();
                yield return _repeatRateWait;
            }
        }

        public virtual void StartSpawning()
        {
            if (_spawnCoroutine == null && gameObject.activeInHierarchy)
            {
                _isSpawning = true;
                _spawnCoroutine = StartCoroutine(SpawnRoutine());
            }
        }

        public virtual void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                _isSpawning = false;
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        public virtual void Reset()
        {
            StopSpawning();
            ReturnAllToPool();
        }
    }
}