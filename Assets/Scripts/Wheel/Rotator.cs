using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour
{
    public int maxRotationSpeed;
    private Rigidbody2D _rigidbody;

    private Stopwatch _stopwatch = new Stopwatch();
    private int _currentScore;
    private bool _currentlySpinning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving() && _stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds > 1000)
        {
            StopSpin();
        }
    }

    bool IsMoving()
    {
        return _rigidbody.angularVelocity <= 2f;
    }

    private void StopSpin()
    {
        UnityEngine.Debug.Log($"Wheel stopped after: {_stopwatch.ElapsedMilliseconds}ms");
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Adding {_currentScore} points");
        _currentlySpinning = false;
        WheelController.Instance.AddScore(_currentScore);
    }

    public void Break(int breakForce)
    {
        if (!_currentlySpinning)
        {
            return;
        }
        _rigidbody.AddTorque(breakForce * -1);
    }

    public void Spin(float force)
    {
        if (_currentlySpinning)
        {
            return;
        }
        _currentlySpinning = true;
        _stopwatch.Restart();
        _rigidbody.AddTorque(Math.Min(force, maxRotationSpeed));
    }

    public void SetScore(int score)
    {
        _currentScore = score;
    }

    public bool IsSpinning()
    {
        return _currentlySpinning;
    }
}
