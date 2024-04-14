using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class SpinControl : MonoBehaviour
{
    public const float TargetAngle = 180;
    public const float MinAngle = 90;
    public const float TargetTime = 0.5f;
    public const float MaxTime = 2f;
    public const float DeadZone = 0.4f;
    public string[] spinActionButtons;
    private InputAction[] _spinInputButtons;
    public string spinActionStick;
    private InputAction _spinInputStick;
    public int totalSpinStrength;
    private SpinCalculation _spinCalculation;

    // spin calculations
    private struct SpinCalculation
    {
        public SpinCalculation(Vector2 spinStartDirection)
        {
            StartDirection = spinStartDirection;
            _spinActionCurrentDirection = StartDirection;
            _spinActionLastDirection = StartDirection;
            _spinActionDuration = 0;
        }

        private Vector2 _spinActionCurrentDirection;
        private Vector2 _spinActionLastDirection;
        private float _spinActionDuration;
        public Vector2 StartDirection { get; init; }
        public Vector2 CurrentDirection
        {
            get => _spinActionCurrentDirection;
            set
            {
                if (_spinActionCurrentDirection.magnitude > 0)
                {
                    _spinActionLastDirection = _spinActionCurrentDirection;
                }
                _spinActionCurrentDirection = value;
            }
        }
        public Vector2 LastDirection
        {
            get => _spinActionLastDirection;
        }
        public float CurrentSpinAngle()
        {
            var spinAngle = Vector2.SignedAngle(LastDirection, CurrentDirection);
            //Debug.Log($"Spin angle is {spinAngle}");
            return spinAngle;
        }
        public float TotalSpinAngle()
        {
            var spinAngle = Vector2.SignedAngle(StartDirection, LastDirection);
            //Debug.Log($"Total spin angle is {spinAngle}");
            // if signed angle is smaller 0, add 180 and difference!
            if (spinAngle < 0)
            {
                return 360f + spinAngle;
            }
            return spinAngle;
        }
        public bool IsSpinning()
        {
            if (LastDirection == null || CurrentDirection == null || CurrentDirection.magnitude == 0)
            {
                return false;
            }
            return CurrentSpinAngle() >= 0;
        }
        public void AddDuration(float duration)
        {
            _spinActionDuration += duration;
        }
        public float SpinDuration
        {
            get => _spinActionDuration;
        }
    }


    void Start()
    {
        _spinInputButtons = spinActionButtons
            .Select(a => InputSystem.actions.FindAction(a))
            .Where(a => a != null)
            .ToArray();
        _spinInputStick = InputSystem.actions.FindAction(spinActionStick);
    }

    void Update()
    {
        _spinCalculation.AddDuration(Time.deltaTime);
        if (SpinCompleted())
        {

            var spinDuration = _spinCalculation.SpinDuration;
            var spinAngle = _spinCalculation.TotalSpinAngle();
            if (spinAngle < MinAngle || spinDuration > MaxTime)
            {
                if (spinAngle != 0 && spinDuration > 0)
                {
                    Debug.Log("Failing spin");
                }
            }
            else
            {
                Debug.Log("Completing spin");
                var spinStrength = EvaluateSpin(spinDuration, spinAngle);
                var wheelRotator = WheelController.Instance.GetWheelRotator();
                if (wheelRotator != null)
                {
                    Spin(wheelRotator, spinStrength);
                }
            }
            _spinCalculation = new SpinCalculation();
        }
    }

    bool SpinCompleted()
    {
        var currentlySpinning = _spinCalculation.IsSpinning();
        // To spin, all spin buttons should be pressed
        if (!SpinButtonsPressed())
        {
            if (currentlySpinning)
            {
                Debug.Log("Cancelling spin");
                _spinCalculation = new SpinCalculation();
            }
            return false;
        }
        // then, check if the stick is currently turning
        var currentStickInput = _spinInputStick.ReadValue<Vector2>();
        if (currentlySpinning)
        {
            _spinCalculation.CurrentDirection = currentStickInput;
        }
        // if currently already spinning, then not completed
        if (currentlySpinning || currentStickInput.magnitude > DeadZone)
        {
            // if not spinning but started: start new spin
            if (!currentlySpinning)
            {
                Debug.Log("Starting spin");
                _spinCalculation = new SpinCalculation(currentStickInput);
            }
            return false;
        }
        return currentStickInput.magnitude <= DeadZone;
    }
    float EvaluateSpin(float spinDuration, float spinAngle)
    {
        var spinDurationFactor = Math.Min(TargetTime / spinDuration, 1);
        var spinAngleFactor = Math.Min(spinAngle / TargetAngle, 1);
        return totalSpinStrength * spinDurationFactor * spinAngleFactor;
    }

    void Spin(Rotator rotator, float spinStrength)
    {
        rotator.Spin(spinStrength);
    }

    public bool SpinButtonsPressed()
    {
        return !_spinInputButtons.Any(a => !a.IsPressed());
    }
}
