using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    [SerializeField] public Transform monsterTransform = null;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damage = 10f;

    public void Awake()
    {
        JobSystemManager.RegisterEnemy(monsterTransform);
    }

    private void OnDestroy()
    {
        JobSystemManager.UnregisterEnemy(monsterTransform);     
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnDeath()
    {

    }

    public void TakeDamage(int damage)
    {

    }
}
