using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class Cyclope : Monster
{
    Vector3 player = new Vector3(0, 0, 0);
    [SerializeField] private Transform cyclopeTransform;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int cooldown_tir = 0;
    [SerializeField] private int cooldown_tir_max = 500;


    public Cyclope()
    {
        pv = 50;
        atk = 10;
        vit = 3;
    }
    void Update()
    {
        cooldown_tir += 1;
        if (cooldown_tir == cooldown_tir_max)
        {
            cooldown_tir = 0;
            GameObject newProjectile = Instantiate(projectilePrefab, cyclopeTransform.position, Quaternion.identity);
            Projectile_ennemies new_projectile = newProjectile.GetComponent<Projectile_ennemies>();
            new_projectile.SetDirection(player);

        }
    }


}