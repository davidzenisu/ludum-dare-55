using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakControl : MonoBehaviour
{
    public string[] breakActions;
    public int breakStrength;
    private InputAction[] breakInputActions;


    void Start()
    {
        breakInputActions = breakActions
            .Select(a => InputSystem.actions.FindAction(a))
            .Where(a => a != null)
            .ToArray();
    }

    void Update()
    {
        if (!breakInputActions.Any(a => a.IsPressed()))
        {
            return;
        }
        var wheelRotator = WheelController.Instance.GetWheelRotator();
        if (wheelRotator == null)
        {
            return;
        }
        Break(wheelRotator);
    }

    void Break(Rotator rotator)
    {

        var combinedBreakStrength = breakInputActions.Sum(a => a.IsPressed() ? breakStrength : 0);
        rotator.Break(combinedBreakStrength);
    }
}
