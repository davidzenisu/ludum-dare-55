using UnityEngine;

public class Rotator : MonoBehaviour
{
    public int speedRotateStart;
    public int deceleration;
    private float _speedRotate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddTorque(speedRotateStart);
        // _speedRotate = speedRotateStart;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     transform.Rotate(Vector3.back * _speedRotate * Time.deltaTime);
    //     _speedRotate -= deceleration * Time.deltaTime;
    // }
}
