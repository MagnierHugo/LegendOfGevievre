using Unity.VisualScripting;
using UnityEngine;


public class Cyclope : BaseMonster
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private int maxShootingCooldown = 5;
    private float shootingCooldown = 0;

    private void Update()
    {
        shootingCooldown += Time.deltaTime;
        if (shootingCooldown >= maxShootingCooldown)
        {
            shootingCooldown = 0;
            Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform).GetComponent<Projectile>();
            newProjectile.SetDirection(GameManager.PlayerTransform);
        }
    }

    protected override Vector3 Move()
    {
        Vector3 vector = GameManager.PlayerTransform.position - transform.position;
        float magnitude = vector.magnitude;
        Vector3 normalized = vector.normalized;
        if (magnitude <= distanceToPlayer - 1f)
            transform.position -= MoveSpeed * Time.deltaTime * vector.normalized;
        else if (magnitude > distanceToPlayer)
            transform.position += MoveSpeed * Time.deltaTime * vector.normalized;

        return normalized;
    }
}