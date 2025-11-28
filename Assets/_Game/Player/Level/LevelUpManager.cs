using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class LevelUpManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject xpBubble;
    [SerializeField] private GameObject xpParent;

    [Header("LevelUp Choices")]
    [SerializeField] private GameObject panelLevelUp;
    [SerializeField] private List<PowerUpData> allPowerUps;
    [SerializeField] private List<Image> cards;

    [Header("Slider")]
    [SerializeField] private Slider slider;
    [SerializeField] private int xpMax = 100;
    [SerializeField] private int xp = 0;

    private readonly float generateRange = 5f;
    public static bool GamePaused {  get; private set; }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 randomPos = new(
                Random.Range(-generateRange, generateRange),
                Random.Range(-generateRange, generateRange)
            );

            Instantiate(xpBubble, randomPos, Quaternion.identity, xpParent.transform);
        }
    }

    private void FixedUpdate()
    {
        if (slider != null)
            if (slider.value == 1)
                LevelUp();
    }

    #region Gain Exp
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);

        GainXP(20);
    }

    public void SetXP(int currentXP, int maxXP)
        => slider.value = (float)currentXP / maxXP;

    private void GainXP(int amount)
    {
        xp += amount;
        xp = Mathf.Clamp(xp, 0, xpMax);
        SetXP(xp, xpMax);
    }
    #endregion

    private void LevelUp()
    {
        TogglePause();

        // Reset values
        slider.value = 0;
        xp = 0;
        xpMax += 50;

        // Powers choice
        panelLevelUp.SetActive(true);
        GetRandomChoices(3);
    }

    private List<PowerUpData> GetAvailablePowerUps()
    {
        return allPowerUps
            .Where(p => p.CurrentLevel <= p.MaxLevel)
            .ToList();
    }

    private List<PowerUpData> GetRandomChoices(int count)
    {
        List<PowerUpData> available = GetAvailablePowerUps();
        List<PowerUpData> result = new();

        for (int i = 0; i < count && available.Count > 0; i++)
        {
            int r = Random.Range(0, available.Count);
            cards[i].sprite = available[r].Icon; // Sprite initialization

            var hover = cards[i].GetComponent<HoverCards>();
            hover.Init(available[r]);

            result.Add(available[r]);
            available.RemoveAt(r);
        }

        return result;
    }

    private void TogglePause()
    {
        GamePaused = !GamePaused;
        Time.timeScale = GamePaused ? 0f : 1f;
    }

    public void SelectPowerUp()
    {
        TogglePause();
        panelLevelUp.SetActive(false);
    }
}
