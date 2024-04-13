using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakControl : MonoBehaviour
{
    public string[] breakActions;
    public int breakStrength;
    public Rotator rotator;
    private InputAction[] breakInputActions;


    void Start()
    {
        if (!rotator)
        {
            rotator = GetComponent<Rotator>();
        }
        breakInputActions = breakActions
            .Select(a => InputSystem.actions.FindAction(a))
            .Where(a => a != null)
            .ToArray();
    }

    void Update()
    {
        if (breakInputActions.Any(a => a.IsPressed()))
        {
            Break();
        }
    }

    void Break()
    {
        var combinedBreakStrength = breakInputActions.Sum(a => a.IsPressed() ? breakStrength : 0);
        rotator.Break(combinedBreakStrength);
    }
}
