using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<BaseWeapon> weapons;
    private int currentWeaponIndex;


    private float attackTimer;
    [SerializeField] private float attackRate;


    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > 1 / attackRate)
        {
            weapons[currentWeaponIndex++ % weapons.Count].Attack(gameObject);
            attackTimer = 0;
        }
    }
}
