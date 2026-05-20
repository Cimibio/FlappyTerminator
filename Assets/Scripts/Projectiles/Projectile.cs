using System;
using UnityEngine;

[RequireComponent(typeof(ProjectileMover), typeof(ProjectileRotator), typeof(CollisionDetector))]
public class Projectile : MonoBehaviour
{
    private ProjectileMover _mover;
    private ProjectileRotator _rotator;
    private CollisionDetector _collisionDetector;

    public event Action<Projectile> Died;

    private void Awake()
    {
        _mover = GetComponent<ProjectileMover>();
        _rotator = GetComponent<ProjectileRotator>();
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    private void OnEnable()
    {
        _collisionDetector.Collided += Die;
    }

    private void OnDisable()
    {
        _collisionDetector.Collided -= Die;
        ResetProjectile();
    }

    public void Init(Vector2 direction, float speed)
    {
        Vector2 velocity = direction.normalized * speed;

        _mover.SetDirection(velocity);
        _rotator.SetRotation(direction);
    }

    private void Die()
    {
        Died?.Invoke(this);
    }

    private void ResetProjectile()
    {
        _mover.ResetMovement();
        _rotator.ResetRotation();
    }
}