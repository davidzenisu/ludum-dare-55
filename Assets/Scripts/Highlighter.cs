using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class Highlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject highlight;
    void Awake()
    {
        addPhysicsRaycaster();
    }

    void addPhysicsRaycaster()
    {
        var physicsRaycaster = FindAnyObjectByType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.SetActive(false);
    }

}
