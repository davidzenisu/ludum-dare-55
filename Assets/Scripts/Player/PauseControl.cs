using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{
    public string pauseAction;
    private InputAction _pauseInputAction;

    void Start()
    {
        _pauseInputAction = InputSystem.actions.FindAction(pauseAction);
    }

    void Update()
    {
        if (_pauseInputAction.WasPressedThisFrame())
        {
            WheelController.Instance.TriggerPause();
        }
    }
}
