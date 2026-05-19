using System;
using UnityEngine;

[RequireComponent(typeof(TerminatorMover), typeof(ScoreCounter))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(TerminatorCollisionDetector))]
public class Terminator : MonoBehaviour
{
    private TerminatorMover _mover;
    private ScoreCounter _scoreCounter;
    private TerminatorCollisionDetector _collisionDetector;
    private InputReader _inputReader;

    public event Action GameOver;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _collisionDetector = GetComponent<TerminatorCollisionDetector>();
        _mover = GetComponent<TerminatorMover>();
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _collisionDetector.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _collisionDetector.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Pipe)
        {
            GameOver?.Invoke();
        }

        else if(interactable is ScoreZone) 
        {
            _scoreCounter.Add();
        }
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _mover.Reset();
    }
}
