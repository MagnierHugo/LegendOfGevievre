using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    [SerializeField] public float MoveSpeed { get; protected set; } = 2f;
    [SerializeField] public float Damage { get; protected set; } = 10f;

    private void OnDeath()
    {

    }
}
