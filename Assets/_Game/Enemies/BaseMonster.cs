using System;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    [field: SerializeField] public int HealthPoints { get; protected set; } = 10;
    [field: SerializeField] public int AttackDamage { get; protected set; } = 5;
    [field: SerializeField] public float MoveSpeed { get; protected set; } = 2f;

    public void Awake() => JobSystemManager.RegisterEnemy(transform);
    private void OnDestroy() => JobSystemManager.UnregisterEnemy(transform);

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

    protected virtual void OnDeath() { }
}
