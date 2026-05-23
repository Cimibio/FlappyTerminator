using Spawners;
using System;
using UnityEngine;

public class TerminatorSpawner : CommandSpawner<Terminator>
{
    [SerializeField] private ProjectileSpawner _projectileSpawner;

    private Terminator _currentTerminator;

    public event Action TerminatorDied;
    public event Action<Terminator> TerminatorSpawned;

    protected override void OnObjectSpawned(Terminator terminator)
    {
        terminator.GameOver += OnTerminatorDeath;
        _currentTerminator = terminator;
        TerminatorSpawned?.Invoke(terminator);
        terminator.SetProjectileSpawner(_projectileSpawner);
    }

    public void SpawnNewTerminator()
    {
        if (_currentTerminator != null)
        {
            _currentTerminator.GameOver -= OnTerminatorDeath;
            ReleaseToPool(_currentTerminator);
        }

        SpawnAtPoint();
    }

    private void OnTerminatorDeath()
    {
        if (_currentTerminator != null)
            _currentTerminator.Reset();

        TerminatorDied?.Invoke();
    }

    public override void Reset()
    {
        if (_currentTerminator != null)
        {
            _currentTerminator.GameOver -= OnTerminatorDeath;
            ReleaseToPool(_currentTerminator);
            _currentTerminator = null;
        }

        base.Reset();
    }
}