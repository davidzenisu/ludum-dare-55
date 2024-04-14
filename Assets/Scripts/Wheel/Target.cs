using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered wheel");
        WheelController.Instance.SetTargetWheel(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited wheel");
        WheelController.Instance.ResetTargetWheel();
    }

}
