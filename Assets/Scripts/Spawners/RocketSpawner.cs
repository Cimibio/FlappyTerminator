using Spawners;
using System.Collections;
using UnityEngine;

public class RocketSpawner : Spawner<Rocket>
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed = 5f;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private float _lifetime = 10f;

    private bool _isSpawning = true;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _sleep;

    protected override void Start()
    {
        base.Start();
        _sleep = new WaitForSeconds(_fireRate);
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    protected override void Spawn(Rocket rocket)
    {
        base.Spawn(rocket);
        rocket.Died += OnProjectileDied;
    }

    protected override void Despawn(Rocket rocket)
    {
        rocket.Died -= OnProjectileDied;
        base.Despawn(rocket);
    }

    private void Shoot()
    {
        Rocket rocket = GetFromPool();
        rocket.transform.position = _firePoint.position;

        Vector2 direction = -_firePoint.right;
        rocket.Init(direction, _projectileSpeed, _lifetime);
    }

    private void OnProjectileDied(Rocket rocket)
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
        while (_isSpawning)
        {
            Shoot();
            yield return _sleep;
        }
    }
}
