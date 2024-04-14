using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class WheelController : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject endMenu;
    public GameObject wheel;
    public GameObject gameZone;
    public PentagramSpawn[] pentagramPositions;
    private PentagramMonitor[] pentagramMonitors = new PentagramMonitor[0];
    public static WheelController Instance { get; private set; }
    public int totalGameLengthSeconds;
    private GameObject _targetWheel;
    private Rotator _targetWheelRotator;
    private Light2D _targetWheelLight;
    private int _totalScore;

    private Timer _timer;
    private Score _scoreBoard;
    private bool _isGameRunning;

    class PentagramMonitor
    {
        public PentagramSpawn spawnInfo;
        public bool hasSpawned;
        public GameObject pentagram;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        addPhysicsRaycaster();
        _timer = FindAnyObjectByType<Timer>();
        _scoreBoard = FindAnyObjectByType<Score>();
        startMenu?.SetActive(true);
    }

    private void CreatePentagramMonitors()
    {
        foreach (var pentagramMonitor in pentagramMonitors)
        {
            if (pentagramMonitor.pentagram == null)
            {
                continue;
            }
            Destroy(pentagramMonitor.pentagram);
        }
        pentagramMonitors = pentagramPositions.Select(p => new PentagramMonitor()
        {
            spawnInfo = p
        }).ToArray();
    }

    private void addPhysicsRaycaster()
    {
        var physicsRaycaster = FindAnyObjectByType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    private void Update()
    {
        if (!_isGameRunning)
        {
            return;
        }
        if (_targetWheelRotator)
        {
            _targetWheelLight?.gameObject.SetActive(!_targetWheelRotator.IsSpinning());
        }
        ValidateTimer();
    }

    public GameObject GetTargetWheel()
    {
        return _targetWheel;
    }

    public void SetTargetWheel(GameObject targetWheel)
    {
        _targetWheel = targetWheel;
        _targetWheelLight = _targetWheel.GetComponentInChildren<Light2D>(true);
        _targetWheelRotator = _targetWheel.GetComponentInChildren<Rotator>(true);
    }

    public void ResetTargetWheel()
    {
        _targetWheelLight?.gameObject.SetActive(false);
        _targetWheel = null;
        _targetWheelLight = null;
        _targetWheelRotator = null;
    }

    public Rotator GetWheelRotator()
    {
        return GetTargetWheel()?.GetComponentInChildren<Rotator>();
    }

    public void AddScore(int addedScore)
    {
        _totalScore += addedScore;
        _scoreBoard?.SetScore(_totalScore);
    }

    private void StartTimer()
    {
        _timer?.StartTimer(totalGameLengthSeconds);
    }

    public void StartGame()
    {
        CreatePentagramMonitors();
        Time.timeScale = 1;
        _isGameRunning = true;
        AddScore(_totalScore * -1);
        StartTimer();
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        _isGameRunning = false;
        endMenu?.SetActive(true);
    }

    private void ValidateTimer()
    {
        var timeRemaining = _timer?.GetTimeRemaining();
        if (timeRemaining == null)
        {
            return;
        }
        if (timeRemaining <= 0)
        {
            EndGame();
            return;
        }
        var timeElapsed = totalGameLengthSeconds - timeRemaining;
        var pentagramSpawns = pentagramMonitors.Where(p => !p.hasSpawned && p.spawnInfo.timeStamp <= timeElapsed).ToList();
        foreach (var spawn in pentagramSpawns)
        {
            spawn.pentagram = SpawnPentagram(new Vector2(spawn.spawnInfo.x, spawn.spawnInfo.y));
            spawn.hasSpawned = true;
        }
    }

    public void TriggerPause()
    {
        if (_isGameRunning)
        {
            Time.timeScale = 0f;
            // Enable main menu
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            // Disable main menu
            pauseMenu.SetActive(false);
        }
        _isGameRunning = !_isGameRunning;
    }

    public int GetScore()
    {
        return _totalScore;
    }

    public GameObject SpawnPentagram(Vector2 position)
    {
        return Instantiate(wheel, position, new Quaternion(), gameZone.transform);
    }
}

[Serializable]
public struct PentagramSpawn
{
    public float x;
    public float y;
    public float timeStamp;
}