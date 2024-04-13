using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StickPositionDebug : MonoBehaviour
{
    public string actionType;
    public GameObject displayReference;
    private RectTransform _rect;
    private float _displayRate;
    private InputAction _inputAction;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _inputAction = InputSystem.actions.FindAction(actionType);
        if (!displayReference)
        {
            displayReference = transform.parent.gameObject;
        }
        var displayReferenceRect = displayReference.GetComponent<RectTransform>().rect;
        var shortestReferenceSide = Math.Min(displayReferenceRect.width, displayReferenceRect.height);
        var longestOwnSide = Math.Max(_rect.rect.width, _rect.rect.height);
        _displayRate = shortestReferenceSide / 2 - longestOwnSide;
    }

    void Update()
    {
        var currentInput = _inputAction.ReadValue<Vector2>();
        var newPosition = new Vector2(currentInput.x, currentInput.y) * _displayRate;
        _rect.anchoredPosition = newPosition;
    }
}
