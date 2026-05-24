using System;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector), typeof(EnemyAnimator), typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D), typeof(LifeTimer), typeof(Rotator))]
[RequireComponent (typeof(Mover), typeof(EnemyShooter))]
public class Enemy : MonoBehaviour
{
    private CollisionDetector _collisionDetector;
    private EnemyAnimator _animator;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private LifeTimer _lifeTimer;
    private Rotator _rotator;
    private Mover _enemyMover;
    private EnemyShooter _enemyShooter;

    public event Action<Enemy> Died;
    public event Action Destroyed;

    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
        _animator = GetComponent<EnemyAnimator>();
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _lifeTimer = GetComponent<LifeTimer>();
        _rotator = GetComponent<Rotator>();
        _enemyMover = GetComponent<Mover>();
        _enemyShooter = GetComponent<EnemyShooter>();
    }

    private void Start()
    {
        _collider.isTrigger = false;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;               
    }

    private void OnEnable()
    {        
        _animator.EnemyExplosionAnimationCompleted += Remove;
        _collisionDetector.Collided += Explode;
        _lifeTimer.Expired += Remove;
    }

    private void OnDisable()
    {
        _animator.EnemyExplosionAnimationCompleted -= Remove;
        _collisionDetector.Collided -= Explode;
        _lifeTimer.Expired -= Remove;

        ResetState();
    }

    public void Init(Vector2 direction, float speed, float lifetime, RocketSpawner rocketSpawner)
    {
        _lifeTimer.StartTimer(lifetime);
        _enemyMover.SetDirection(speed);
        _rotator.SetDirection(direction);
        _enemyShooter.SetRocketSpawner(rocketSpawner);
        _collider.enabled = true;
    }    

    public void ResetState()
    {
        Destroyed = null;
        Died = null;

        _lifeTimer.StopTimer();
        _rotator.ResetRotation();
        _enemyMover.ResetMovement();
        _enemyShooter.StopAllCoroutines();
    }

    private void Explode()
    {
        _lifeTimer.StopTimer();
        Destroyed?.Invoke();
        _animator.PlayExplosionAnimation();
        _collider.enabled = false;
    }

    private void Remove()
    {
        Debug.Log($"[{gameObject.name}] Died!");
        Died?.Invoke(this);
    }
}