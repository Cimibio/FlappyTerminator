using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rotator), typeof(CollisionDetector))]
[RequireComponent (typeof(LifeTimer))]
public class Projectile : MonoBehaviour
{
    private Mover _mover;
    private Rotator _rotator;
    private CollisionDetector _collisionDetector;
    private LifeTimer _lifeTimer;

    public event Action<Projectile> Died;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _lifeTimer = GetComponent<LifeTimer>();
    }

    private void OnEnable()
    {
        _collisionDetector.Collided += Die;
        _lifeTimer.Expired += Die;
    }

    private void OnDisable()
    {
        _collisionDetector.Collided -= Die;
        _lifeTimer.Expired -= Die;
    }

    public void Init(Vector2 direction, float speed, float lifetime)
    {
        _lifeTimer.StartTimer(lifetime);
        _mover.SetDirection(speed);
        _rotator.SetDirection(direction);
    }

    private void Die()
    {
        Died?.Invoke(this);
    }

    public void ResetState()
    {
        _mover.ResetMovement();
        _rotator.ResetRotation();
        _lifeTimer.StopTimer();
    }
}