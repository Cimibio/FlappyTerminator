using System;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
public class Enemy : MonoBehaviour
{
    private CollisionDetector _collisionDetector;

    public event Action<Enemy> Died;


    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    private void OnEnable()
    {
        //_animator.EnemyDeathAnimationCompleted += NotifyDeathAnimationCompleted;
        _collisionDetector.Collided += Die;
    }

    private void OnDisable()
    {
        //_animator.EnemyDeathAnimationCompleted -= NotifyDeathAnimationCompleted;
        _collisionDetector.Collided -= Die;
    }

    public void Init()
    {
        
    }

    private void Die()
    {
        //_animator.PlayExplosionAnimation();
    }

    private void NotifyDeathAnimationCompleted()
    {
        Debug.Log($"[{gameObject.name}] Died!");
        Died?.Invoke(this);
    }
}