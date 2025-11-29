using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider healthSlider;
    [SerializeField] private UnityEngine.UI.Slider shieldSlider;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxShield = 100;
	public int CurrentHealth { get; private set; }
	public int CurrentShield { get; private set; }

	public static event Action<int, int> OnHealthChanged;
	public static event Action<int, int> OnShieldChanged;
	public static event Action OnPlayerDied;

	void Awake()
	{
		CurrentHealth = maxHealth;
		CurrentShield = maxShield;
		shieldSlider.value = 1.0f;
		healthSlider.value = 1.0f;
    }

    public void UpdateHealthSlider(int currentHealth, int maxHealth)
		=> healthSlider.value = (float)currentHealth / maxHealth;

    public void UpdateShieldSlider(int currentShield, int maxShield)
        => shieldSlider.value = (float)currentShield / maxShield;


    public void TakeDamage(int damageAmount)
	{
		if (CurrentHealth <= 0)
			return;

		// Transfer remaining damage to health
		if (CurrentShield > 0)
		{
            CurrentShield -= damageAmount;
			if (CurrentShield < 0)
			{
				damageAmount = -CurrentShield;
				CurrentShield = 0;
			}
			else
			{
				damageAmount = 0;
			}
				UpdateShieldSlider(CurrentShield, maxShield);
			OnShieldChanged?.Invoke(CurrentShield, maxShield);
        }

		CurrentHealth -= damageAmount;

        UpdateHealthSlider(CurrentHealth, maxHealth);

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

		Debug.Log($"Took {damageAmount} damage. HP remaining: {CurrentHealth}");

		if (CurrentHealth <= 0)
		{
			Die();
			CurrentHealth = 0;
		}
	}

	public void Heal(int healAmount)
	{
		if (CurrentHealth >= 100)
			return;

		CurrentHealth += healAmount;
		CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);

        UpdateHealthSlider(CurrentHealth, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

		Debug.Log($"Healed for {healAmount}. Current HP: {CurrentHealth}");
	}

    public void ApplyShield(int shieldAmount)
    {
        if (CurrentShield >= 100)
			return;

        CurrentShield += shieldAmount;
        CurrentShield = Mathf.Min(CurrentShield, maxShield);

        UpdateShieldSlider(CurrentShield, maxShield);
        OnShieldChanged?.Invoke(CurrentShield, maxShield);

        Debug.Log($"Added {shieldAmount} to current shield. Current Shield: {CurrentShield}");
    }

    private void Die()
	{
        SceneManager.LoadScene("GameOverMenu");

        OnPlayerDied?.Invoke();
	}
}