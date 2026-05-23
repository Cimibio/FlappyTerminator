using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    [SerializeField] private FirePoint _firePoint;
    [SerializeField] private float _cooldown = 0.5f;

    private ProjectileSpawner _projectileSpawner;
    private float _lastShotTime = -Mathf.Infinity;

    //public event Action Shooted;

    public void SetSpawner(ProjectileSpawner spawner)
    {
        _projectileSpawner = spawner;
    }

    public void Shoot()
    {
        if (Time.time >= _lastShotTime + _cooldown)
        {
            _lastShotTime = Time.time;
            _projectileSpawner.ShootFromPoint(_firePoint.transform);
        }
    }
}