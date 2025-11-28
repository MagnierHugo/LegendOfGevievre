using System;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    [field: SerializeField] public int HealthPoints { get; protected set; } = 10;
    [field: SerializeField] public int AttackDamage { get; protected set; } = 5;
    [field: SerializeField] public float MoveSpeed { get; protected set; } = 2f;

    private bool inAttackRange = false;
    private PlayerHealth playerHealth;
    private float lastAttackTime;
    [SerializeField] private float attackCooldown;

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

    protected virtual void OnDeath() { }
}
