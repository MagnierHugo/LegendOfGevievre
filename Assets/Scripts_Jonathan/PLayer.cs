using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth { get; private set; }
    public int CurrentXP { get; private set; } = 0;

    private Collider2D playerCollider;

    public static event Action<int, int> OnHealthChanged;
    public static event Action<int> OnXPChanged;
    public static event Action OnPlayerDied;

    void Awake()
    {
        playerCollider = GetComponent<Collider2D>();

        if (playerCollider == null)
        {
            Debug.LogError("Player GameObject requires a Collider2D component!");
        }

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

    public void ToggleCollider(bool isEnabled)
    {
        if (playerCollider != null)
        {
            playerCollider.enabled = isEnabled;
            Debug.Log($"Player Collider enabled set to: {isEnabled}");
        }
    }

    private void Die()
    {
        Debug.Log("U ded cunt!");

        OnPlayerDied?.Invoke();

        ToggleCollider(false);
    }
}