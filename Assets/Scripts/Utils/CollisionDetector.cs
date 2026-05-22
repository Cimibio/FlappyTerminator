using UnityEngine;
using System;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;

    public event Action Collided;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)        
            HandleCollision();        
    }

    private void HandleCollision()
    {
        Debug.Log($"[{gameObject.name}] hit detected");

        Collided?.Invoke();
    }
}
