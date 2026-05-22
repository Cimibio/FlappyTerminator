using Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private float _enemySpeed = 1f;
    [SerializeField] private float _deltaSpawnAreaOffset = 3.5f;
    [SerializeField] private float _lifetime = 15f;
    [SerializeField] private float _startSpawnDelay = 0.5f;
    [SerializeField] private Vector2 _startVector = Vector2.up;

    private bool _isSpawning = true;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _sleepTime;
    private WaitForSeconds _spawnDelay;

    private List<Enemy> _activeEnemies = new List<Enemy>();

    protected override void Start()
    {
        _spawnDelay = new WaitForSeconds(_startSpawnDelay);
        _sleepTime = new WaitForSeconds(_repeatRate);
        base.Start();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    protected override void Spawn(Enemy enemy)
    {
        enemy.transform.position = GetRandomSpawnPoint();

        base.Spawn(enemy);

        _activeEnemies.Add(enemy);

        enemy.Init(_startVector, _enemySpeed, _lifetime);
        enemy.Died += Remove;
    }

    protected override void Despawn(Enemy enemy)
    {
        enemy.Died -= Remove;
        _activeEnemies.Remove(enemy);
        base.Despawn(enemy);
    }

    public void Reset()
    {
        StopSpawning();

        for (int i = _activeEnemies.Count - 1; i >= 0; i--)        
            if (_activeEnemies[i] != null)            
                ReleaseToPool(_activeEnemies[i]);     

        _activeEnemies.Clear();
    }

    private void Remove(Enemy enemy)
    {
        enemy.Died -= Remove;
        ReleaseToPool(enemy);
    }

    public void StartSpawning()
    {        
        if (_spawnCoroutine == null)
        {
            _isSpawning = true;
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawning()
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
        yield return _spawnDelay;

        while (_isSpawning)
        {
            GetFromPool();
            yield return _sleepTime;
        }
    }
}