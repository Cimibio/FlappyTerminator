using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private bool _rotateToDirection = true;
    [SerializeField] private float _rotationSpeed = 360f;
    [SerializeField] private float _angleOffset = -90f;

    private Vector2 _targetDirection;
    private bool _hasDirection;

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero) 
            return;

        _targetDirection = direction.normalized;
        _hasDirection = true;

        if (_rotateToDirection && _rotationSpeed >= 360f)
            ApplyRotation();
    }

    public void ResetRotation()
    {
        _hasDirection = false;
        _targetDirection = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (_rotateToDirection && _hasDirection && _targetDirection != Vector2.zero)
        {
            if (_rotationSpeed >= 360f)
                ApplyRotation();
            else
                SmoothRotate();
        }
    }

    private void ApplyRotation()
    {
        float targetAngle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle + _angleOffset);
    }

    private void SmoothRotate()
    {
        float targetAngle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle + _angleOffset);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}