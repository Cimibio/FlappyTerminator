using System;
using UnityEngine;

[RequireComponent(typeof(ProjectileMover), typeof(CollisionDetector))]
public class Projectile : MonoBehaviour
{
    private CollisionDetector _collisionDetector;

    public event Action<Projectile> Died;

    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    private void OnEnable()
    {
        Debug.Log($"[{gameObject.name}] Projectile spawned");
        _collisionDetector.Collided += Die;
    }

    private void OnDisable()
    {
        Debug.Log($"[{gameObject.name}] Projectile despawned");
        _collisionDetector.Collided -= Die;
    }

    private void Die()
    {
        Died?.Invoke(this);
    }
}