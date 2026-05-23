using UnityEngine;

public class TerminatorTracker : MonoBehaviour
{
    [SerializeField] private float _xOffset;
    [SerializeField] private Transform _terminator;

    private void Update()
    {
        var position = transform.position;
        position.x = _terminator.transform.position.x + _xOffset;
        transform.position = position;
    }

    public void SetTarget(Transform terminator)
    {
        _terminator = terminator;
    }
}
