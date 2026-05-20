using Spawners;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class ProjectileSpawner : Spawner<Projectile>
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed = 10f;

    private Shooter _shooter;

    protected override void Start()
    {
        base.Start();
        _shooter = GetComponent<Shooter>();
    }

    private void OnEnable()
    {
        _shooter.Shooted += OnShoot;
    }

    private void OnDisable()
    {
        _shooter.Shooted -= OnShoot;
    }

    protected override void Spawn(Projectile projectile)
    {
        base.Spawn(projectile);
        projectile.Died += OnProjectileDied;
    }

    protected override void Despawn(Projectile projectile)
    {
        projectile.Died -= OnProjectileDied;

        ProjectileMover mover = projectile.GetComponent<ProjectileMover>();
        mover.ResetMovement();

        base.Despawn(projectile);
    }

    private void OnShoot()
    {
        Projectile projectile = GetFromPool();
        projectile.transform.position = _firePoint.position;

        Vector2 direction = _firePoint.right;
        ProjectileMover mover = projectile.GetComponent<ProjectileMover>();
        mover.SetDirection(direction * _projectileSpeed);
    }

    private void OnProjectileDied(Projectile projectile)
    {
        Despawn(projectile);
    }
}