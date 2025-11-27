using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    [SerializeField] public Transform monsterTransform = null;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damage = 10f;

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
}
