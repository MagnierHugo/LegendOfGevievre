using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;

    private void Reset()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string description)
    {
        panel.SetActive(true);
        text.text = description;
    }
}
