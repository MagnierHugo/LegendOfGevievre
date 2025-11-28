using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<BaseWeapon> weapons;


    private float attackTimer;
    [SerializeField] private float attackRate;


    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > 1 / attackRate)
        {
            for (int i = 0;  i < weapons.Count; i++)
            {
                weapons[i].Attack(gameObject, CalculateAttackDirection());
            }
            attackTimer = 0;
        }
    }

    private Vector2 CalculateAttackDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - (Vector2)transform.position).normalized;
    }
}
