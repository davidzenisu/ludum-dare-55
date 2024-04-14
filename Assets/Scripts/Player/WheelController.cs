using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class WheelController : MonoBehaviour
{
    public GameObject pauseMenu;
    public static WheelController Instance { get; private set; }
    public int totalGameLengthSeconds;
    private GameObject _targetWheel;
    private Rotator _targetWheelRotator;
    private Light2D _targetWheelLight;
    private int _totalScore;

    private Timer _timer;
    private Score _scoreBoard;
    private bool _isGameRunning;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

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
        if (_targetWheelRotator)
        {
            _targetWheelLight?.gameObject.SetActive(!_targetWheelRotator.IsSpinning());
        }
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
        _isGameRunning = true;
        AddScore(_totalScore * -1);
        StartTimer();
    }

    private void EndGame()
    {

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
}
