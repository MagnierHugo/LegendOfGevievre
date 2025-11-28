using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerXP : MonoBehaviour
{
    [Header("LevelUp Choices")]
    [SerializeField] private GameObject panelLevelUp;
    [SerializeField] private List<PowerUpData> allPowerUps;
    [SerializeField] private List<Image> cards;
    [SerializeField] private Slider slider;

    [SerializeField] private PlayerAttack playerAttack;

    private int xpMax = 10;
    public int CurrentXP { get; private set; }

    public static event Action<int> OnXPChanged;

    private void FixedUpdate()
    {
        if (slider != null)
            if (slider.value == 1)
                LevelUp();
    }

    public void EarnXP(int xpAmount)
    {
        CurrentXP += xpAmount;
        CurrentXP = Mathf.Clamp(CurrentXP, 0, xpMax);
        SetXP(CurrentXP, xpMax);

        OnXPChanged?.Invoke(CurrentXP);

        Debug.Log($"Earned {xpAmount} XP. Total XP: {CurrentXP}");
    }

    public void SetXP(int currentXP, int maxXP)
        => slider.value = (float)currentXP / maxXP;

    private void LevelUp()
    {
        GameManager.Instance.TogglePause();

        // Reset values
        slider.value = 0;
        CurrentXP = 0;
        xpMax += 10;

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
        GameManager.Instance.TogglePause();
        panelLevelUp.SetActive(false);

        if (playerAttack.weapons.Contains(powerUpData.powerUp))
        {
            powerUpData.CurrentLevel++;
            powerUpData.powerUp.Upgrade();
        }
        else
            playerAttack.weapons.Add(powerUpData.powerUp);
    }
}