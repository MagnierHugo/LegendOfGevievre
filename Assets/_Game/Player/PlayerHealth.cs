using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider healthSlider;

    [SerializeField] private int maxHealth = 100;
	public int CurrentHealth { get; private set; }

	public static event Action<int, int> OnHealthChanged;
	public static event Action OnPlayerDied;

	void Awake()
	{
		CurrentHealth = maxHealth;
	}

    public void UpdateHealthSlider(int currentHealth, int maxHealth)
		=> healthSlider.value = (float)currentHealth / maxHealth;


    public void TakeDamage(int damageAmount)
	{
		if (CurrentHealth <= 0)
			return;

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
		if (CurrentHealth <= 0) return;

		CurrentHealth += healAmount;
		CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);

        UpdateHealthSlider(CurrentHealth, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

		Debug.Log($"Healed for {healAmount}. Current HP: {CurrentHealth}");
	}

	private void Die()
	{
		Debug.Log("U ded cunt!");

		OnPlayerDied?.Invoke();
	}
}