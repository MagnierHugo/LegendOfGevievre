using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HoverCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 hoverScale;
    private Vector3 baseScale;

    private PowerUpData linkedData;

    [SerializeField] private GameObject panelLevelUp;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;

    private event Action<PowerUpData> onPowerUpSelected;

    public void Init(PowerUpData data, Action<PowerUpData> onPowerUpSelected)
    {
        linkedData = data;
        this.onPowerUpSelected = onPowerUpSelected;
    }

    private void Start()
    {
        baseScale = transform.localScale;
        hoverScale = new(baseScale.x + 0.2f, baseScale.y + 0.2f, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverScale;
        ShowDescription(linkedData.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = baseScale;
        HideDescription();
    }

    private void ShowDescription(string description)
    {
        panel.SetActive(true);
        text.text = description;
    }

    private void HideDescription()
        => panel.SetActive(false);

    public void TriggerOnPowerUpData()
    {
        HideDescription();
        onPowerUpSelected?.Invoke(linkedData);
    }
}
