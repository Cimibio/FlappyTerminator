using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private Vector2 _moveDirection = Vector2.left;

    private Rotator _rotator;

    private void Awake()
    {
        //_rotator = GetComponent<Rotator>();
    }

    //private void Start()
    //{
    //    _rotator?.SetDirection(_moveDirection);
    //}

    private void Update()
    {
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }
}
