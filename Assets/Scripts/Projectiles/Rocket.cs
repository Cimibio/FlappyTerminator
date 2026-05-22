using System;
using UnityEngine;

[RequireComponent(typeof(ProjectileMover), typeof(CollisionDetector), typeof(LifeTimer))]
public class Rocket : MonoBehaviour
{
    private ProjectileMover _mover;
    private CollisionDetector _collisionDetector;
    private LifeTimer _lifeTimer;

    public event Action<Rocket> Died;

    private void Awake()
    {
        _mover = GetComponent<ProjectileMover>();
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
        ResetProjectile();
    }

    public void Init(Vector2 direction, float speed, float lifetime)
    {
        Vector2 velocity = direction.normalized * speed;

        _lifeTimer.StartTimer(lifetime);
        _mover.SetDirection(velocity);
    }

    private void Die()
    {
        Died?.Invoke(this);
    }

    private void ResetProjectile()
    {
        _lifeTimer.StopTimer();
        _mover.ResetMovement();
    }
}