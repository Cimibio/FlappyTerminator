using System;
using UnityEngine;

[RequireComponent(typeof(ProjectileMover), typeof(CollisionDetector))]
public class Rocket : MonoBehaviour
{
    private ProjectileMover _mover;
    private CollisionDetector _collisionDetector;

    public event Action<Rocket> Died;

    private void Awake()
    {
        _mover = GetComponent<ProjectileMover>();
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
    }

    private void Die()
    {
        Died?.Invoke(this);
    }

    private void ResetProjectile()
    {
        _mover.ResetMovement();
    }
}