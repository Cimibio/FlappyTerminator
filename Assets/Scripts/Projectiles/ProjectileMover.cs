using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private Vector2 _velocity;
    private bool _isMoving;

    private void Update()
    {
        if (_isMoving)
            transform.Translate(_velocity * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        _velocity = direction;
        _isMoving = true;
    }

    public void ResetMovement()
    {
        _isMoving = false;
        _velocity = Vector2.zero;
    }
}