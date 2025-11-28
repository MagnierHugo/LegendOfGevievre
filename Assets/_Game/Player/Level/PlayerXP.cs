using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerXP : MonoBehaviour
{
    [Header("LevelUp Choices")]
    [SerializeField] private GameObject panelLevelUp;
    [SerializeField] private List<PowerUpData> allPowerUps;
    [SerializeField] private List<Image> cards;
    [SerializeField] private List<TextMeshProUGUI> levelCards;
    [SerializeField] private Slider slider;

    [SerializeField] private PlayerAttack playerAttack;

    private int xpMax = 10;
    private bool waitingForPowerSelection = false;
    public int CurrentXP { get; private set; }

    public static event Action<int> OnXPChanged;

    public void EarnXP(int xpAmount)
    {
        CurrentXP += xpAmount;
        CheckLevelUp();
        UpdateXPSlider(CurrentXP, xpMax);

        OnXPChanged?.Invoke(CurrentXP);
    }

    public void UpdateXPSlider(int currentXP, int maxXP)
        => slider.value = (float)currentXP / maxXP;

    private void CheckLevelUp()
    {
        if (waitingForPowerSelection) return;

        while (CurrentXP >= xpMax) // While loop allows multi-level XP bursts
        {
            CurrentXP -= xpMax;  // Keep leftover XP
            xpMax += 10;         // Increase required XP

            waitingForPowerSelection = true;
            LevelUpUI();         // Handles UI + power selection
            return;
        }

        UpdateXPSlider(CurrentXP, xpMax);
    }

    private void LevelUpUI()
    {
        GameManager.Instance.TogglePause();

        // Powers choice
        panelLevelUp.SetActive(true);
        GetRandomChoices(3);
    }

    private List<PowerUpData> GetAvailablePowerUps()
    {
        return allPowerUps
            .Where(p => p.CurrentLevel < p.MaxLevel)
            .ToList();
    }

    private void GetRandomChoices(int cardCount)
    {
        List<PowerUpData> available = GetAvailablePowerUps();
        int compteur = 0;

        for (compteur = 0; compteur < cardCount && available.Count > 0; compteur++)
        {
            int r = Random.Range(0, available.Count);

            cards[compteur].sprite = available[r].Icon; // Sprite initialization
            levelCards[compteur].text = $"{available[r].CurrentLevel + 1}";
            var hover = cards[compteur].GetComponent<HoverCards>();
            hover.Init(available[r], SelectPowerUp);

            available.RemoveAt(r);
        }

        if (compteur == 0)
        {
            GameManager.Instance.TogglePause();
            panelLevelUp.SetActive(false);
        }

        for (int i = compteur; i < cards.Count; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
    }

    public void SelectPowerUp(PowerUpData powerUpData)
    {
        if (playerAttack.weapons.Contains(powerUpData.powerUp))
            powerUpData.powerUp.Upgrade();
        else
        {
            powerUpData.powerUp.upgradeLevel = 0;
            playerAttack.weapons.Add(powerUpData.powerUp);
        }

        powerUpData.CurrentLevel++;

        panelLevelUp.SetActive(false);
        GameManager.Instance.TogglePause();

        waitingForPowerSelection = false;

        CheckLevelUp();

        UpdateXPSlider(CurrentXP, xpMax);
    }
}