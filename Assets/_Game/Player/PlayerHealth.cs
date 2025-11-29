using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider healthSlider;
    [SerializeField] private UnityEngine.UI.Slider shieldSlider;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float maxShield = 100;
	[SerializeField] private float shieldDecreaseSpeed = 1;
	public int CurrentHealth { get; private set; }
	public float CurrentShield { get; private set; }	

	public static event Action<int, int> OnHealthChanged;
	public static event Action<int, int> OnShieldChanged;
	public static event Action OnPlayerDied;

	private void Awake()
	{
		CurrentHealth = maxHealth;
		CurrentShield = 0f;
		shieldSlider.value = 0f;
		healthSlider.value = 1f;
    }

    private void Update()
    {
        if ( CurrentShield > 0)
		{
			CurrentShield -= shieldDecreaseSpeed * Time.deltaTime;
            UpdateShieldSlider((int)CurrentShield, (int)maxShield);
            OnShieldChanged?.Invoke((int)CurrentShield, (int)maxShield);
        }
		else
			CurrentShield = 0;
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
				damageAmount = (int)-CurrentShield;
				CurrentShield = 0;
			}
			else
			{
				damageAmount = 0;
			}
				UpdateShieldSlider((int)CurrentShield, (int)maxShield);
			OnShieldChanged?.Invoke((int)CurrentShield, (int)maxShield);
        }

		CurrentHealth -= damageAmount;

        UpdateHealthSlider(CurrentHealth, maxHealth);

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

		//Debug.Log($"Took {damageAmount} damage. HP remaining: {CurrentHealth}");

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
	}

    public void ApplyShield(int shieldAmount)
    {
        CurrentShield += shieldAmount;
        CurrentShield = Mathf.Min(CurrentShield, maxShield);

        UpdateShieldSlider((int)CurrentShield, (int)maxShield);
        OnShieldChanged?.Invoke((int)CurrentShield, (int)maxShield);
    }

    private void Die()
	{
        SceneManager.LoadScene("GameOverMenu");

        OnPlayerDied?.Invoke();
	}
}