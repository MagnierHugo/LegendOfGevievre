using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class Cyclope : BaseMonster
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int maxShootingCooldown = 5;
    private float shootingCooldown = 0;

    private void Update()
    {
        shootingCooldown += Time.deltaTime;
        if (shootingCooldown >= maxShootingCooldown)
        {
            shootingCooldown = 0;
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile new_projectile = newProjectile.GetComponent<Projectile>();
            new_projectile.SetDirection(GameManager.PlayerTransform);

        }
    }
}