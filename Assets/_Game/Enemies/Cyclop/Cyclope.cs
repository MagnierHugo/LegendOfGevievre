using UnityEngine;


public class Cyclope : BaseMonster
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private int maxShootingCooldown = 5;
    [SerializeField] private Vector2 minBounds = new Vector2(-32f, -32f);
    [SerializeField] private Vector2 maxBounds = new Vector2(32f, 32f);
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

        Transform _transform = transform;

        if (magnitude <= distanceToPlayer - 1f)
            _transform.position -= MoveSpeed * Time.deltaTime * normalized;
        else if (magnitude > distanceToPlayer)
            _transform.position += MoveSpeed * Time.deltaTime * normalized;

        _transform.position = new Vector3(
            _transform.position.x <= minBounds.x ? minBounds.x : _transform.position.x >= maxBounds.x ? maxBounds.x : _transform.position.x,
            _transform.position.y <= minBounds.y ? minBounds.y : _transform.position.y >= maxBounds.y ? maxBounds.y : _transform.position.y,
            _transform.position.z
        );

        return normalized;
    }
}