using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinControl : MonoBehaviour
{
    public string[] spinActions;
    public int totalSpinStrength;
    public Rotator rotator;
    private InputAction[] spinInputActions;


    void Start()
    {
        if (!rotator)
        {
            rotator = GetComponent<Rotator>();
        }
        spinInputActions = spinActions
            .Select(a => InputSystem.actions.FindAction(a))
            .Where(a => a != null)
            .ToArray();
    }

    void Update()
    {
        if (spinInputActions.All(a => a.IsPressed()))
        {
            Spin();
        }
    }

    void Spin()
    {
        rotator.Spin(totalSpinStrength);
    }
}
