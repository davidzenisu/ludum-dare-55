using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonPressDebug : MonoBehaviour
{
    public string actionType;
    private Image _image;
    private InputAction _inputAction;

    void Start()
    {
        _image = GetComponent<Image>();
        _inputAction = InputSystem.actions.FindAction(actionType);
    }

    void Update()
    {
        var buttonPressed = _inputAction.IsPressed();
        _image.enabled = buttonPressed;
    }
}
