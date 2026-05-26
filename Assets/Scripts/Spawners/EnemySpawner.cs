using Spawners;
using System;
using UnityEngine;

public class EnemySpawner : PeriodicSpawner<Enemy>
{
    [SerializeField] private RocketSpawner _rocketSpawner;
    [SerializeField] private float _enemySpeed = 1f;
    [SerializeField] private float _deltaSpawnAreaOffset = 3.5f;
    [SerializeField] private float _lifetime = 15f;
    [SerializeField] private Vector2 _startVector = Vector2.up;
    [SerializeField] private Transform _spawnAreaCenter;

    public event Action Scored;

    protected override void Start()
    {
        if (_spawnAreaCenter == null)
            _spawnAreaCenter = transform;

        base.Start();
    }

    protected override void SpawnObject()
    {
        Enemy enemy = GetFromPool();

        if (enemy == null) 
            return;

        enemy.ResetState();
        enemy.transform.position = GetRandomSpawnPoint();
        enemy.Init(_startVector, _enemySpeed, _lifetime, _rocketSpawner);
    }

    protected override void SubscribeToEvents(Enemy enemy)
    {
        enemy.Died += Remove;
        enemy.Destroyed += InformScore;
    }

    protected override void UnsubscribeFromEvents(Enemy enemy)
    {
        enemy.Died -= Remove;
        enemy.Destroyed -= InformScore;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float randomYOffset = UnityEngine.Random.Range(-_deltaSpawnAreaOffset, _deltaSpawnAreaOffset);

        return new Vector3(
            _spawnAreaCenter.position.x,
            _spawnAreaCenter.position.y + randomYOffset,
            _spawnAreaCenter.position.z
        );
    }

    private void Remove(Enemy enemy)
    {
        ReleaseToPool(enemy);
    }

    private void InformScore()
    {
        Scored?.Invoke();
    }
}