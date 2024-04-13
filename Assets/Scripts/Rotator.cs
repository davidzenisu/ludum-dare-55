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
        //Spin(maxRotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStopped())
        {
            UnityEngine.Debug.Log($"Wheel stopped after: {_stopwatch.ElapsedMilliseconds}ms");
            _stopwatch.Stop();
        }
        BreakControl();
    }

    void BreakControl()
    {

    }

    bool IsStopped()
    {
        return _rigidbody.angularVelocity <= 1 && _stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds > 1000;
    }

    void Break(int breakForce)
    {
        _rigidbody.AddTorque(breakForce * -1);
    }

    void Spin(int force)
    {
        _rigidbody.AddTorque(force);
    }
}
