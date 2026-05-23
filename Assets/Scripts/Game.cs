using UnityEngine;

[RequireComponent (typeof(ScoreCounter))]
public class Game : MonoBehaviour
{
    [SerializeField] private TerminatorSpawner _terminatorSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private RocketSpawner _rocketSpawner;
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private TerminatorTracker _terminatorTracker;

    private ScoreCounter _scoreCounter;

    private bool _isGameActive = false;

    private void OnEnable()
    {
        _scoreCounter = GetComponent<ScoreCounter>();

        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _terminatorSpawner.TerminatorDied += OnTerminatorDied;
        _terminatorSpawner.TerminatorSpawned += SetTrackingTarget;
        _enemySpawner.Scored += AddScore;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _terminatorSpawner.TerminatorDied -= OnTerminatorDied;
        _terminatorSpawner.TerminatorSpawned -= SetTrackingTarget;
        _enemySpawner.Scored -= AddScore;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnTerminatorDied()
    {
        if (!_isGameActive) 
            return;

        _isGameActive = false;
        Time.timeScale = 0;
        _endGameScreen.Open();
        _enemySpawner.StopSpawning();
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

        ResetGame();

        _terminatorSpawner.SpawnNewTerminator();
        _enemySpawner.StartSpawning();
    }

    private void SetTrackingTarget(Terminator terminator)
    {
        _terminatorTracker.SetTarget(terminator);
    }

    private void ResetGame()
    {
        _terminatorSpawner.Reset();
        _enemySpawner.Reset();
        _rocketSpawner.Reset();
        _projectileSpawner.Reset();
        _scoreCounter.Reset();
    }

    private void AddScore()
    {
        _scoreCounter.Add();
    }
}