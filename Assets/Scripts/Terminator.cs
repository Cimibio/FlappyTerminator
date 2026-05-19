using System;
using UnityEngine;

[RequireComponent(typeof(TerminatorMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(TerminatorCollisionDetector))]
public class Terminator : MonoBehaviour
{
    private TerminatorMover _birdMover;
    private ScoreCounter _scoreCounter;
    private TerminatorCollisionDetector _handler;

    public event Action GameOver;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _handler = GetComponent<TerminatorCollisionDetector>();
        _birdMover = GetComponent<TerminatorMover>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
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
        _birdMover.Reset();
    }
}
