using Spawners;
using UnityEngine;

public class BaseProjectileSpawner<T> : CommandSpawner<T> where T : BaseProjectile<T>
{
    [SerializeField] protected float _projectileSpeed = 10f;
    [SerializeField] protected float _lifetime = 5f;

    public void ShootFromPoint(Transform firePoint)
    {
        if (_objectPooler == null)
        {
            Debug.LogError($"ObjectPooler not assigned on {gameObject.name}");
            return;
        }

        T projectile = GetFromPool();

        if (projectile == null)
            return;

        projectile.Died += Remove;
        SetupProjectile(projectile, firePoint);
    }

    protected virtual void SetupProjectile(T projectile, Transform firePoint)
    {
        projectile.transform.position = firePoint.position;
        Vector2 direction = firePoint.up;
        projectile.Init(direction, _projectileSpeed, _lifetime);
    }

    protected virtual void Remove(T projectile)
    {
        var typedProjectile = projectile as T;

        if (typedProjectile != null)
        {
            typedProjectile.ResetState();
            typedProjectile.Died -= Remove;
            ReleaseToPool(typedProjectile);
        }
    }
}
