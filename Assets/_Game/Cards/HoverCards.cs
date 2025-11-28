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

    public void Init(PowerUpData data)
    {
        linkedData = data;
    }

    private void Start()
    {
        baseScale = transform.localScale;
        hoverScale = new(baseScale.x + 0.5f, baseScale.y + 0.5f, 1f);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        FindFirstObjectByType<LevelUpManager>().SelectPowerUp();
    }

    public void ShowDescription(string description)
    {
        panel.SetActive(true);
        text.text = description;
    }

    public void HideDescription()
        => panel.SetActive(false);
}
