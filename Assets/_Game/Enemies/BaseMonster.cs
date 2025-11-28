using System;

using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class BaseMonster : MonoBehaviour
{
    [field: SerializeField] public int HealthPoints { get; protected set; } = 10;
    [field: SerializeField] public int AttackDamage { get; protected set; } = 5;
    [field: SerializeField] public float MoveSpeed { get; protected set; } = 2f;

    [SerializeField] private float immunityDuration = .3f;
    private float lastTimeTookDamage = float.MinValue;

    [Header("Xp Orbs")]
    [SerializeField] private GameObject smallXpOrb = null;
    [SerializeField] private GameObject mediumXpOrb = null;
    [SerializeField] private GameObject largeXpOrb = null;

    public void Awake() => JobSystemManager.RegisterEnemy(transform);
    private void OnDestroy() => JobSystemManager.UnregisterEnemy(transform);

    public void TakeDamage(int damage)
    {
        if (lastTimeTookDamage + immunityDuration > Time.time)
            return;
        lastTimeTookDamage = Time.time;

        print("Took damage");
        HealthPoints -= damage;
        if (HealthPoints <= 0)
            Die();
    }

    private void Die()
    {
        SpawnXpOrb(transform.position);
        OnDeath();
        Destroy(gameObject);
    }

    protected virtual void OnDeath() { }

    public void SpawnXpOrb(Vector3 position)
    {
        float random = UnityEngine.Random.value;

        if (random <= 0.7) // 70% chance of small orb
            Instantiate(smallXpOrb, position, Quaternion.identity);

        else if (random <= 0.95) // 25% chance of medium orb
            Instantiate(mediumXpOrb, position, Quaternion.identity);

        else if (random <= 1) // 5% chance of large orb
            Instantiate(largeXpOrb, position, Quaternion.identity);
    }
}
