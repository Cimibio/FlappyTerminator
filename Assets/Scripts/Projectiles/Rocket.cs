using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(CollisionDetector), typeof(LifeTimer))]
[RequireComponent (typeof(Rotator))]
public class Rocket : MonoBehaviour
{
    private Mover _mover;
    private CollisionDetector _collisionDetector;
    private LifeTimer _lifeTimer;
    private Rotator _rotator;

    public event Action<Rocket> Died;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _lifeTimer = GetComponent<LifeTimer>();
        _rotator = GetComponent<Rotator>();
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
        ResetProjectile();
    }

    public void Init(Vector2 direction, float speed, float lifetime)
    {
        //Vector2 velocity = direction.normalized * speed;

        _lifeTimer.StartTimer(lifetime);
        _mover.SetDirection(speed);
        _rotator.SetDirection(direction);
    }

    private void Die()
    {
        Died?.Invoke(this);
    }

    private void ResetProjectile()
    {
        _lifeTimer.StopTimer();
        _mover.ResetMovement();
        _rotator.ResetRotation();
    }
}