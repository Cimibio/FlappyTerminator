using Spawners;
using UnityEngine;

//[RequireComponent(typeof(Shooter))]
public class ProjectileSpawner : CommandSpawner<Projectile>
{
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _lifetime = 5f;

    //private Shooter _shooter;

    //private void Awake()
    //{
    //    _shooter = GetComponent<Shooter>();
    //}

    //private void OnEnable()
    //{
    //    if (_shooter != null)
    //        _shooter.Shooted += SpawnAtPoint;
    //}

    //private void OnDisable()
    //{
    //    if (_shooter != null)
    //        _shooter.Shooted -= SpawnAtPoint;
    //}

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