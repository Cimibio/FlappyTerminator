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
            ReleaseToPool(_currentTerminator);
            _currentTerminator = null;
        }

        base.Reset();
    }

    public void SpawnNewTerminator()
    {
        if (_currentTerminator != null)        
            ReleaseToPool(_currentTerminator);        

        SpawnAtSpawnPoint();
    }

    protected override void OnObjectSpawned(Terminator terminator)
    {
        base.OnObjectSpawned(terminator);

        _currentTerminator = terminator;
        TerminatorSpawned?.Invoke(terminator);
        terminator.SetProjectileSpawner(_projectileSpawner);
    }

    protected override void SubscribeToEvents(Terminator terminator)
    {
        terminator.Destroyed += InformPlayerDeath;
    }

    protected override void UnsubscribeFromEvents(Terminator terminator)
    {
        terminator.Destroyed -= InformPlayerDeath;
    }

    private void InformPlayerDeath()
    {
        if (_currentTerminator != null)
            _currentTerminator.ResetTerminator();

        TerminatorDied?.Invoke();
    }
}