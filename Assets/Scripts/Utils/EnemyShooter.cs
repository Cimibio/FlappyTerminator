using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private FirePoint _firePoint;
    [SerializeField] private float _fireInterval = 2f;
    [SerializeField] private float _fireStartDelay = 0.5f;

    private Coroutine _fireCoroutine;
    private RocketSpawner _rocketSpawner;
    private WaitForSeconds _startDelayWait;
    private WaitForSeconds _repeatRateWait;

    private void Awake()
    {
        _startDelayWait = new WaitForSeconds(_fireStartDelay);
        _repeatRateWait = new WaitForSeconds(_fireInterval);
    }

    private void OnDisable()
    {
        StopShooting();
    }

    public void SetRocketSpawner(RocketSpawner spawner)
    {
        StopShooting();
        _rocketSpawner = spawner;
        StartShooting();
    }

    private void StartShooting()
    {
        if (_rocketSpawner != null && _fireCoroutine == null && gameObject.activeInHierarchy)        
            _fireCoroutine = StartCoroutine(ShootRoutine());        
    }

    private void StopShooting()
    {
        if (_fireCoroutine != null)
        {
            StopCoroutine(_fireCoroutine);
            _fireCoroutine = null;
        }
    }

    private IEnumerator ShootRoutine()
    {
        yield return _startDelayWait;

        while (true)
        {
            if (_rocketSpawner != null && _firePoint != null)            
                _rocketSpawner.ShootFromPoint(_firePoint.transform);
            
            yield return _repeatRateWait;
        }
    }
}