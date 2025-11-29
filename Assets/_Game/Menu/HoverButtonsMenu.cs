using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButtonsMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image outline;

    private Vector3 hoverScale;
    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
        hoverScale = new(baseScale.x + 0.2f, baseScale.y + 0.2f, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverScale;
        outline.color = Color.purple;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = baseScale;
        outline.color = Color.white;
    }
}
