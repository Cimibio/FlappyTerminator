using Spawners;
using UnityEngine;

public class RocketSpawner : CommandSpawner<Rocket>
{
    [SerializeField] private float _rocketSpeed = 5f;
    [SerializeField] private float _lifetime = 10f;

    protected override void OnObjectSpawned(Rocket rocket) { }

    public void ShootFromPoint(Transform firePoint)
    {
        if (_objectPooler == null)
        {
            Debug.LogError($"ObjectPooler not assigned on {gameObject.name}");
            return;
        }

        Rocket rocket = GetFromPool();

        if (rocket == null)
            return;

        rocket.Died += Remove;
        SetupRocket(rocket, firePoint);
    }

    private void Remove(Rocket rocket)
    {
        rocket.ResetState();
        rocket.Died -= Remove;
        ReleaseToPool(rocket);
    }

    private void SetupRocket(Rocket rocket, Transform firePoint)
    {
        rocket.transform.position = firePoint.position;
        Vector2 direction = firePoint.up;
        rocket.Init(direction, _rocketSpeed, _lifetime);
    }
}