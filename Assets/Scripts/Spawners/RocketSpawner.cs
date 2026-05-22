using Spawners;
using System.Collections;
using UnityEngine;

public class RocketSpawner : Spawner<Rocket>
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed = 5f;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private float _lifetime = 10f;
    [SerializeField] private float _startSpawnDelay = 0.5f;

    private bool _isSpawning = true;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _sleep;
    private WaitForSeconds _spawnDelay;

    private void OnEnable()
    {
        StartSpawning();        
    }

    protected override void Start()
    {
        base.Start();
        _sleep = new WaitForSeconds(_fireRate);
        _spawnDelay = new WaitForSeconds(_startSpawnDelay);
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    protected override void Spawn(Rocket rocket)
    {
        base.Spawn(rocket);
        rocket.Died += Remove;
    }

    protected override void Despawn(Rocket rocket)
    {
        rocket.Died -= Remove;
        base.Despawn(rocket);
    }

    private void Shoot()
    {
        Rocket rocket = GetFromPool();
        rocket.transform.position = _firePoint.position;

        Vector2 direction = _firePoint.up;
        rocket.Init(direction, _projectileSpeed, _lifetime);
    }

    private void Remove(Rocket rocket)
    {
        Despawn(rocket);
    }

    private void StartSpawning()
    {
        if (_spawnCoroutine == null)
        {
            _isSpawning = true;
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    private void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            _isSpawning = false;
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        yield return _spawnDelay;

        while (_isSpawning)
        {
            Shoot();
            yield return _sleep;
        }
    }
}
