using Spawners;
using System;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class TerminatorSpawner : Spawner<Terminator>
{
    [SerializeField] private Transform _spawnPoint;

    private Terminator _currentTerminator;

    public event Action TerminatorDied;
    public event Action<Terminator> TerminatorSpawned;

    private void OnDisable()
    {
        if (_currentTerminator != null)
            _currentTerminator.GameOver -= OnTerminatorDeath;
    }

    protected override void Spawn(Terminator terminator)
    {
        base.Spawn(terminator);
        terminator.transform.position = _spawnPoint.position;
        _currentTerminator = terminator;
        TerminatorSpawned?.Invoke(terminator);

        terminator.GameOver += OnTerminatorDeath;
    }

    protected override void Despawn(Terminator terminator)
    {
        terminator.GameOver -= OnTerminatorDeath;
        base.Despawn(terminator);

        if (_currentTerminator == terminator)
            _currentTerminator = null;
    }

    public void SpawnNewTerminator()
    {
        if (_currentTerminator != null)        
            ReleaseToPool(_currentTerminator);        

        _currentTerminator = GetFromPool();
    }

    private void OnTerminatorDeath()
    {
        _currentTerminator.Reset();
        TerminatorDied?.Invoke();
    }
}