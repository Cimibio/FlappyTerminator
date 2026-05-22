using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultDirection = Vector3.up;

    private float _speed;
    private bool _isMoving;

    private void Update()
    {
        if (_isMoving)
            transform.Translate(_defaultDirection * _speed * Time.deltaTime);
    }

    public void SetDirection(float speed)
    {
        _speed = speed;
        _isMoving = true;
    }

    public void ResetMovement()
    {
        _isMoving = false;
        _speed = 0;
    }
}