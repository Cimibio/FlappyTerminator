using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _angleOffset = -90f;

    private Vector2 _targetDirection;
    private bool _hasDirection;

    private void Update()
    {
        if (_hasDirection && _targetDirection != Vector2.zero)
            ApplyRotation();
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        _targetDirection = direction.normalized;
        _hasDirection = true;

        ApplyRotation();
    }

    public void ResetRotation()
    {
        _hasDirection = false;
        _targetDirection = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }

    private void ApplyRotation()
    {
        float targetAngle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle + _angleOffset);
    }
}