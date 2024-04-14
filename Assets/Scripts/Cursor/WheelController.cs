using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class WheelController : MonoBehaviour
{
    public static WheelController Instance { get; private set; }
    public int totalGameLengthSeconds;
    private GameObject _targetWheel;
    private int _totalScore;

    private Timer _timer;
    private Score _scoreBoard;

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
        _timer?.StartTimer(totalGameLengthSeconds);
    }

    private void addPhysicsRaycaster()
    {
        var physicsRaycaster = FindAnyObjectByType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public GameObject GetTargetWheel()
    {
        return _targetWheel;
    }

    public void SetTargetWheel(GameObject targetWheel)
    {
        _targetWheel = targetWheel;
        _targetWheel.GetComponentInChildren<Light2D>(true)?.gameObject.SetActive(true);
    }

    public void ResetTargetWheel()
    {
        _targetWheel.GetComponentInChildren<Light2D>(true)?.gameObject.SetActive(false);
        _targetWheel = null;
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
}
