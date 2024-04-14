using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class VirutalMouseConstraints : MonoBehaviour
{

    private VirtualMouseInput _virtualMouseInput;
    private SpinControl _spinControl;
    private float _cursorSpeed;

    private void Awake()
    {
        _virtualMouseInput = GetComponent<VirtualMouseInput>();
        _cursorSpeed = _virtualMouseInput.cursorSpeed;
        _spinControl = FindAnyObjectByType<SpinControl>();
    }

    private void Update()
    {
        _virtualMouseInput.cursorSpeed = _spinControl.SpinButtonsPressed() ? 0 : _cursorSpeed;
    }

    private void LateUpdate()
    {
        var virtualMousePosition = _virtualMouseInput.virtualMouse.position.value;
        virtualMousePosition.x = Mathf.Clamp(virtualMousePosition.x, 0f, Screen.width);
        virtualMousePosition.y = Mathf.Clamp(virtualMousePosition.y, 0f, Screen.height);
        InputState.Change(_virtualMouseInput.virtualMouse.position, virtualMousePosition);
    }
}
