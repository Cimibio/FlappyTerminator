using UnityEngine;

public class DirectionSetter : MonoBehaviour
{
    [SerializeField] private bool _rotateToMovement = true;
    [SerializeField] private float _rotationSpeed = 360f;
    [SerializeField] private float _startDirection = -90f;

    private Vector2 _movementDirection;
    private bool _hasDirection;

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero) 
            return;

        _movementDirection = direction;
        _hasDirection = true;

        if (_rotateToMovement)
            RotateToDirection(direction);
    }

    private void Update()
    {
        if (_rotateToMovement && _hasDirection && _movementDirection != Vector2.zero)        
            RotateToDirection(_movementDirection);        
    }

    private void RotateToDirection(Vector2 direction)
    {
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle + _startDirection);

        if (_rotationSpeed >= 360f)
            transform.rotation = targetRotation;
        else
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public void ResetDirection()
    {
        _hasDirection = false;
        _movementDirection = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }
}
