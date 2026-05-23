using System;
using UnityEngine;

[RequireComponent(typeof(TerminatorMover), typeof(Shooter))]
[RequireComponent(typeof(InputReader), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionDetector))]
public class Terminator : MonoBehaviour
{
    private TerminatorMover _mover;
    private CollisionDetector _collisionDetector;
    private InputReader _inputReader;
    private PlayerAnimator _animator;
    private Shooter _shooter;

    public event Action GameOver;

    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
        _mover = GetComponent<TerminatorMover>();
        _inputReader = GetComponent<InputReader>();
        _animator = GetComponent<PlayerAnimator>();
        _shooter = GetComponent<Shooter>();
    }

    private void OnEnable()
    {
        _collisionDetector.Collided += ProcessCollision;
    }

    private void Update()
    {
        if (_inputReader.IsJumpPressed)
            _mover.Jump();

        if (_inputReader.IsAttackPressed)
            _shooter.Shoot();
    }

    private void OnDisable()
    {
        _collisionDetector.Collided -= ProcessCollision;
    }

    public void ResetTerminator()
    {
        _animator.ResetAnimation();
        _mover.Reset();
    }

    public void SetProjectileSpawner(ProjectileSpawner spawner)
    {
        _shooter.SetSpawner(spawner);
    }

    private void ProcessCollision()
    {
        GameOver?.Invoke();
        _animator.PlayExplosionAnimation();
    }
}
