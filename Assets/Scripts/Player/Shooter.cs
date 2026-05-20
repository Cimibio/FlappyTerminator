using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _cooldown = 0.5f;

    private float _lastShotTime = -Mathf.Infinity;

    public event Action Shooted;

    public void Shoot()
    {
        if (Time.time >= _lastShotTime + _cooldown)
        {
            _lastShotTime = Time.time;
            Shooted?.Invoke();
        }
    }
}