using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TerminatorMover : MonoBehaviour
{
    [SerializeField] private float _tapForce = 4;
    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private float _maxRotationZ = 35;
    [SerializeField] private float _minRotationZ = -60;
    [SerializeField] private float _rotationOffset = -90;

    private Rigidbody2D _rigidbody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ + _rotationOffset);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ + _rotationOffset);

        Reset();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Reset()
    {
        transform.rotation = Quaternion.identity;
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_speed, _tapForce);
        transform.rotation = _maxRotation;
    }
}
