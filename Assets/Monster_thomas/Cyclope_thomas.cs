using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class Cyclope : Monster
{
    private Transform player;
    [SerializeField] private GameObject projectilePrefab;
    private float cooldownTir = 0;
    [SerializeField] private int cooldownTirMax = 5;


    public override void Init(int pv = 50, int atk = 10, int vit = 3)
    {
        base.Init(pv, atk, vit);
    }
    private void Update()
    {
        cooldownTir += Time.deltaTime;
        if (cooldownTir >= cooldownTirMax)
        {
            cooldownTir = 0;
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile_ennemies new_projectile = newProjectile.GetComponent<Projectile_ennemies>();
            new_projectile.SetDirection(player);

        }
    }


}