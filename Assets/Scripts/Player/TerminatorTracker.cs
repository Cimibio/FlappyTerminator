using UnityEngine;

public class TerminatorTracker : MonoBehaviour
{
    [SerializeField] private Terminator _terminator;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        var position = transform.position;
        position.x = _terminator.transform.position.x + _xOffset;
        transform.position = position;
    }
}
