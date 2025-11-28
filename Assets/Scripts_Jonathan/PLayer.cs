using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth { get; private set; }
    public int CurrentXP { get; private set; } = 0;

    public static event Action<int, int> OnHealthChanged;
    public static event Action<int> OnXPChanged;
    public static event Action OnPlayerDied;

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= damageAmount;

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        Debug.Log($"Took {damageAmount} damage. HP remaining: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
            CurrentHealth = 0;
        }
    }

    public void Heal(int healAmount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        Debug.Log($"Healed for {healAmount}. Current HP: {CurrentHealth}");
    }

    public void EarnXP(int xpAmount)
    {
        CurrentXP += xpAmount;

        OnXPChanged?.Invoke(CurrentXP);

        Debug.Log($"Earned {xpAmount} XP. Total XP: {CurrentXP}");
    }

    private void Die()
    {
        Debug.Log("U ded cunt!");

        OnPlayerDied?.Invoke();
    }
}