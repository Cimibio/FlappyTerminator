using Spawners;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class ProjectileSpawner : Spawner<Projectile>
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _lifetime = 5f;

    private Shooter _shooter;

    protected override void Awake()
    {
        base.Awake();
        _shooter = GetComponent<Shooter>();
    }

    private void OnEnable()
    {
        _shooter.Shooted += Shoot;
    }

    private void OnDisable()
    {
        _shooter.Shooted -= Shoot;
    }

    protected override void Spawn(Projectile projectile)
    {
        base.Spawn(projectile);
        projectile.Died += Remove;
    }

    protected override void Despawn(Projectile projectile)
    {
        projectile.Died -= Remove;
        base.Despawn(projectile);
    }

    private void Shoot()
    {
        Projectile projectile = GetFromPool();
        projectile.transform.position = _firePoint.position;

        Vector2 direction = _firePoint.up;
        projectile.Init(direction, _projectileSpeed, _lifetime);
    }

    private void Remove(Projectile projectile)
    {
        Despawn(projectile);
    }
}