using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(Collider2D))]
public class BaseMonster : MonoBehaviour
{
    [field: SerializeField] public int HealthPoints { get; protected set; } = 10;
    [field: SerializeField] public int AttackDamage { get; protected set; } = 5;
    [field: SerializeField] public float MoveSpeed { get; protected set; } = 2f;

    [SerializeField] private float immunityDuration = .3f;
    private float lastTimeTookDamage = float.MinValue;
    [SerializeField] private float damageFlashDuration;

    [Header("Xp Orbs")]
    [SerializeField] private GameObject smallXpOrb = null;
    [SerializeField] private GameObject mediumXpOrb = null;
    [SerializeField] private GameObject largeXpOrb = null;
    [SerializeField] private GameObject superXpOrb = null;

    [Header("Pickable Prefab")]
    [SerializeField] private GameObject healPotion = null;
    [SerializeField] private GameObject shield = null;
    [SerializeField] private GameObject magnet = null;

    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    private bool inAttackRange = false;
    private PlayerHealth playerHealth;
    private float lastAttackTime;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out playerHealth))
            inAttackRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out playerHealth))
            inAttackRange = false;
    }

    private void FixedUpdate()
    {
        Rotate(Move());
    }

    private void Update()
    {
        if (!inAttackRange)
            return;

        if (lastAttackTime + attackCooldown > Time.time)
            return;

        lastAttackTime = Time.time;
        playerHealth?.TakeDamage(AttackDamage);
    }

    public void TakeDamage(int damage, bool xpDrop = true)
    {
        if (lastTimeTookDamage + immunityDuration > Time.time)
            return;
        lastTimeTookDamage = Time.time;
 
        HealthPoints -= damage;
        if (HealthPoints <= 0)
        {
            if (!xpDrop && Random.value <= 0.2f) // 1% chance
                    SpawnSuperXpOrb(transform.position);

            if (xpDrop)
                SpawnXpOrb(transform.position);

            Die();
        }
    }

    protected virtual Vector3 Move()
    {
        Vector3 vector = GameManager.PlayerTransform.position - transform.position;
        if (vector.magnitude < 0.1f)
            return Vector3.zero;

        Vector3 normalized = vector.normalized;
        transform.position += MoveSpeed * Time.deltaTime * normalized;
        return normalized;
    }

    private void Rotate(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) <= .1f) 
            return;

        if (direction.x < 0f)
            spriteRenderer.sprite = lastTimeTookDamage + damageFlashDuration > Time.time ? sprites[1] : sprites[0];
        else
            spriteRenderer.sprite = lastTimeTookDamage + damageFlashDuration > Time.time ? sprites[3] : sprites[2];
    }

    private void Die()
    {
        OnDeath();
        Destroy(gameObject);
    }

    protected virtual void OnDeath()
    {
        spawnPickupable();
    }

    private void spawnPickupable()
    {
        float random = UnityEngine.Random.value;
        Vector3 offset = new Vector3(random * 10, random * 10, 0);

        if (random <= 0.05f)
            SpawnHeal(transform.position + offset); // Offset so it doesn't spawn on the xp orbs
        else if (random <= 0.1f)
            SpawnShield(transform.position + offset);
        else if (random <= 0.2f)
            SpawnMagnet(transform.position + offset);
    }

    private void SpawnXpOrb(Vector3 position)
    {
        float random = UnityEngine.Random.value;

        if (random <= 0.7) // 70% chance of small orb
            Instantiate(smallXpOrb, position, Quaternion.identity);

        else if (random <= 0.95) // 25% chance of medium orb
            Instantiate(mediumXpOrb, position, Quaternion.identity);

        else if (random <= 1) // 5% chance of large orb
            Instantiate(largeXpOrb, position, Quaternion.identity);
    }

    private void SpawnSuperXpOrb(Vector3 position)
    {
        Instantiate(superXpOrb, position, Quaternion.identity);
    }
    private void SpawnHeal(Vector3 position)
    {
        Instantiate(healPotion, position, Quaternion.identity);
    }

    private void SpawnShield(Vector3 position)
    {
        Instantiate(shield, position, Quaternion.identity);
    }
    private void SpawnMagnet(Vector3 position)
    {
        Instantiate(magnet, position, Quaternion.identity);
    }
}
