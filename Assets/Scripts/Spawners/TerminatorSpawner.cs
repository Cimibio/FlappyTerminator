using Spawners;
using System;
using UnityEngine;

public class TerminatorSpawner : CommandSpawner<Terminator>
{
    [SerializeField] private ProjectileSpawner _projectileSpawner;

    private Terminator _currentTerminator;

    public event Action TerminatorDied;
    public event Action<Terminator> TerminatorSpawned;

    public override void Reset()
    {
        if (_currentTerminator != null)
        {
            _currentTerminator.GameOver -= InformPlayerDeath;
            ReleaseToPool(_currentTerminator);
            _currentTerminator = null;
        }

        base.Reset();
    }

    public void SpawnNewTerminator()
    {
        if (_currentTerminator != null)
        {
            _currentTerminator.GameOver -= InformPlayerDeath;
            ReleaseToPool(_currentTerminator);
        }

        SpawnAtSpawnPoint();
    }

    protected override void OnObjectSpawned(Terminator terminator)
    {
        terminator.GameOver += InformPlayerDeath;
        _currentTerminator = terminator;
        TerminatorSpawned?.Invoke(terminator);
        terminator.SetProjectileSpawner(_projectileSpawner);
    }

    private void InformPlayerDeath()
    {
        if (_currentTerminator != null)
            _currentTerminator.ResetTerminator();

        TerminatorDied?.Invoke();
    }
}