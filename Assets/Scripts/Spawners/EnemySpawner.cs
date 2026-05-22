using Spawners;
using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private float _enemySpeed = 1f;
    [SerializeField] private float _deltaSpawnAreaOffset = 3.5f;
    [SerializeField] private float _lifetime = 15f;
    [SerializeField] private Vector2 _startVector = Vector2.up;

    private bool _isSpawning = true;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _sleepTime;

    protected override void Start()
    {
        _sleepTime = new WaitForSeconds(_repeatRate);
        base.Start();
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    protected override void Spawn(Enemy enemy)
    {
        enemy.transform.position = GetRandomSpawnPoint();

        base.Spawn(enemy);

        Vector2 direction = _startVector;
        enemy.Init(direction, _enemySpeed, _lifetime);
        enemy.Died += Remove;
    }

    public void Reset()
    {
        StopSpawning();
    }

    private void Remove(Enemy enemy)
    {
        enemy.Died -= Remove;
        ReleaseToPool(enemy);
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

    private Vector3 GetRandomSpawnPoint()
    {
        float randomYOffset = Random.Range(-_deltaSpawnAreaOffset, _deltaSpawnAreaOffset);

        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            transform.position.y + randomYOffset,
            transform.position.z
        );

        return spawnPosition;
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isSpawning)
        {
            GetFromPool();
            yield return _sleepTime;
        }
    }
}