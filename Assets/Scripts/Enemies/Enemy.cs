using System;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector), typeof(EnemyAnimator), typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private CollisionDetector _collisionDetector;
    private EnemyAnimator _animator;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
        _animator = GetComponent<EnemyAnimator>();
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _collider.enabled = true;
        _collider.isTrigger = false;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnEnable()
    {        
        _animator.EnemyExplosionAnimationCompleted += Remove;
        _collisionDetector.Collided += Die;
    }

    private void OnDisable()
    {
        _animator.EnemyExplosionAnimationCompleted -= Remove;
        _collisionDetector.Collided -= Die;
    }

    private void Die()
    {
        _animator.PlayExplosionAnimation();
        _collider.enabled = false;
    }

    private void Remove()
    {
        Debug.Log($"[{gameObject.name}] Died!");
        Died?.Invoke(this);
    }
}