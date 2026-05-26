using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rotator), typeof(CollisionDetector))]
[RequireComponent(typeof(LifeTimer))]
public abstract class BaseProjectile<T> : MonoBehaviour where T : BaseProjectile<T>
{
    private Mover _mover;
    private Rotator _rotator;
    private CollisionDetector _collisionDetector;
    private LifeTimer _lifeTimer;

    public event Action<T> Died;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _lifeTimer = GetComponent<LifeTimer>();
    }

    private void OnEnable()
    {
        _collisionDetector.Collided += InformDeath;
        _lifeTimer.Expired += InformDeath;
    }

    private void OnDisable()
    {
        _collisionDetector.Collided -= InformDeath;
        _lifeTimer.Expired -= InformDeath;
    }

    public void Init(Vector2 direction, float speed, float lifetime)
    {
        _lifeTimer.StartTimer(lifetime);
        _mover.InitMovement(speed);
        _rotator.SetDirection(direction);
    }

    public void ResetState()
    {
        _mover.ResetMovement();
        _rotator.ResetRotation();
        _lifeTimer.StopTimer();
    }

    private void InformDeath()
    {
        Died?.Invoke((T)this);
    }
}
