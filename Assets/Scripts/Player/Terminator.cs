using System;
using UnityEngine;

[RequireComponent(typeof(TerminatorMover), typeof(ScoreCounter))]
[RequireComponent(typeof(InputReader), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionDetector))]
public class Terminator : MonoBehaviour
{
    private TerminatorMover _mover;
    private ScoreCounter _scoreCounter;
    private CollisionDetector _collisionDetector;
    private InputReader _inputReader;
    private PlayerAnimator _animator;

    public event Action GameOver;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _mover = GetComponent<TerminatorMover>();
        _inputReader = GetComponent<InputReader>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _collisionDetector.Collided += ProcessCollision;
    }

    private void Update()
    {
        if (_inputReader.IsJumpPressed)
            _mover.Jump();
    }

    private void OnDisable()
    {
        _collisionDetector.Collided -= ProcessCollision;
    }

    private void ProcessCollision()
    {
        GameOver?.Invoke();
        _animator.PlayExplosionAnimation();
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _mover.Reset();
    }
}
