
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [field: SerializeField] public int HealthPoints { get; protected set; } = 10;
    [field: SerializeField] public int AttackDamage { get; protected set; } = 5;
    [field: SerializeField] public int Speed { get; protected set; } = 5;

    public void TakeDamage(int atk)
    {
        HealthPoints -= atk;
    }
}
