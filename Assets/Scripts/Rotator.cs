using System.Diagnostics;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public int speedRotateStart;
    public int deceleration;
    private float _speedRotate;

    private Stopwatch _stopwatch = new Stopwatch();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddTorque(speedRotateStart);
        // _speedRotate = speedRotateStart;
        _stopwatch.Restart();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(Vector3.back * _speedRotate * Time.deltaTime);
        // _speedRotate -= deceleration * Time.deltaTime;
        if (GetComponent<Rigidbody2D>().angularVelocity <= 1 && _stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds > 1000)
        {
            UnityEngine.Debug.Log($"Wheel stopped after: {_stopwatch.ElapsedMilliseconds}ms");
            _stopwatch.Stop();
        }
    }
}
