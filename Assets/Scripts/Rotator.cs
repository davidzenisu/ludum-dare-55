using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour
{
    public int maxRotationSpeed;
    private Rigidbody2D _rigidbody;

    private Stopwatch _stopwatch = new Stopwatch();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _stopwatch.Restart();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStopped() && _stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds > 1000)
        {
            UnityEngine.Debug.Log($"Wheel stopped after: {_stopwatch.ElapsedMilliseconds}ms");
            _stopwatch.Stop();
        }
    }

    bool IsStopped()
    {
        return _rigidbody.angularVelocity <= 0;
    }

    public void Break(int breakForce)
    {
        if (IsStopped())
        {
            return;
        }
        _rigidbody.AddTorque(breakForce * -1);
    }

    public void Spin(int force)
    {
        if (!IsStopped())
        {
            return;
        }
        _rigidbody.AddTorque(Math.Min(force, maxRotationSpeed));
    }
}
