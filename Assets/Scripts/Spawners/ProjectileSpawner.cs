using Spawners;
using UnityEngine;

public class ProjectileSpawner : CommandSpawner<Projectile>
{
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _lifetime = 5f;

    protected override void OnObjectSpawned(Projectile projectile)
    {
        Vector2 direction = _spawnPoint.up;
        projectile.Init(direction, _projectileSpeed, _lifetime);
    }

    public void ShootFromPoint(Transform firePoint)
    {
        if (_objectPooler == null)
        {
            Debug.LogError($"ObjectPooler not assigned on {gameObject.name}");
            return;
        }

        Projectile projectile = GetFromPool();

        if (projectile == null)
            return;

        projectile.transform.position = firePoint.position;

        Vector2 direction = firePoint.up;
        projectile.Init(direction, _projectileSpeed, _lifetime);
    }
}