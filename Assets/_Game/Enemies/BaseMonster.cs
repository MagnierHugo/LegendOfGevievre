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

    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    private bool inAttackRange = false;
    private PlayerHealth playerHealth;
    private float lastAttackTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out playerHealth))
            inAttackRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out playerHealth))
            inAttackRange = false;
    }

    private void FixedUpdate() => Move();
    private void Update()
    {
        if (!inAttackRange)
            return;

        if (lastAttackTime + attackCooldown > Time.time)
            return;

        lastAttackTime = Time.time;
        playerHealth.TakeDamage(AttackDamage);
    }

    public void TakeDamage(int damage)
    {
        if (lastTimeTookDamage + immunityDuration > Time.time)
            return;
        lastTimeTookDamage = Time.time;

        HealthPoints -= damage;
        if (HealthPoints <= 0)
            Die();
    }

    private void Die()
    {
        OnDeath();
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        Vector3 vector = GameManager.PlayerTransform.position - transform.position;
        if (vector.magnitude < 0.1f)
            return;

        transform.position += MoveSpeed * Time.deltaTime * vector.normalized;
    }

    protected virtual void OnDeath()
    {
        SpawnXpOrb(transform.position);
    }

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
