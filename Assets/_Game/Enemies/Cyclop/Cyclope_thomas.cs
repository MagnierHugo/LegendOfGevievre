using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class Cyclope : BaseMonster
{
    private Transform player;
    [SerializeField] private GameObject projectilePrefab;
    private float shootingCooldown = 0;
    [SerializeField] private int maxShootingCooldown = 5;

    private void Update()
    {
        shootingCooldown += Time.deltaTime;
        if (shootingCooldown >= maxShootingCooldown)
        {
            shootingCooldown = 0;
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile new_projectile = newProjectile.GetComponent<Projectile>();
            new_projectile.SetDirection(player);

        }
    }
}