using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private List<BaseMonster> activeMonster = new List<BaseMonster>();

    private void Update()
    {
        Vector3 playerPos = player.position;
        float deltaTime = Time.deltaTime;

        for (int i = 0; i < activeMonster.Count; i++)
        {
            BaseMonster enemy = activeMonster[i];
            Vector3 direction = playerPos - enemy.transform.position;

            if (direction.sqrMagnitude > 0.1f)
            {
                enemy.transform.position += deltaTime * enemy.GetMoveSpeed() * direction.normalized;

                enemy.transform.up = direction;
            }
            else
            {
                // player.TakeDamage(enemy.GetDamage());
            }
        }
    }
}