using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerHealth : MonoBehaviour
{
	public int MaxHealth = 100;
	public int CurrentHealth { get; private set; }

	private BoxCollider2D playerCollider;

	public static event Action<int, int> OnHealthChanged;
	public static event Action OnPlayerDied;

	void Awake()
	{
		playerCollider = GetComponent<BoxCollider2D>();
		CurrentHealth = MaxHealth;
	}

	public void TakeDamage(int damageAmount)
	{
		if (CurrentHealth <= 0)
			return;

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

	private void Die()
	{
		Debug.Log("U ded cunt!");

		OnPlayerDied?.Invoke();
	}
}