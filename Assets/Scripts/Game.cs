using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private TerminatorSpawner _terminatorSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private TerminatorTracker _tracker;

    private bool _isGameActive = false;

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _terminatorSpawner.TerminatorDied += OnTerminatorDied;
        _terminatorSpawner.TerminatorSpawned += SetTrackingTarget;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _terminatorSpawner.TerminatorDied -= OnTerminatorDied;
        _terminatorSpawner.TerminatorSpawned -= SetTrackingTarget;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void SetTrackingTarget(Terminator terminator)
    {
        _tracker.SetTarget(terminator);
    }

    private void OnTerminatorDied()
    {
        if (!_isGameActive) 
            return;

        _isGameActive = false;
        Time.timeScale = 0;
        _endGameScreen.Open();

        _enemySpawner.Reset();
    }

    private void OnRestartButtonClick()
    {
        _endGameScreen.Close();
        StartGame();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        _isGameActive = true;
        Time.timeScale = 1;

        _terminatorSpawner.SpawnNewTerminator();

        _enemySpawner.StartSpawning();
    }
}