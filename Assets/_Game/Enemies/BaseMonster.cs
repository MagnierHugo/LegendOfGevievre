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

    private void Awake()
    {
        float timeRatio = Time.time / 10f;
        HealthPoints += HealthPoints * (int)(0.01f * timeRatio);
        AttackDamage += AttackDamage * (int)(0.01f * timeRatio);
        MoveSpeed += MoveSpeed * (int)(0.01f * timeRatio);
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
            if (!xpDrop && Random.value <= 0.001f) // 0.1% chance
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
        float random = Random.value;
        Vector3 offset = new Vector3(random * 10, random * 10, 0);

        if (random <= 0.01f) // 1% chance
        {
            int randomPickupable = Random.Range(1, 4);
            GameObject pickupableToSpawn = randomPickupable switch
            {
                1 => healPotion,
                2 => shield,
                _ => magnet
            };

            Instantiate(pickupableToSpawn, transform.position + offset, Quaternion.identity);
        }
    }

    private void SpawnXpOrb(Vector3 position)
    {
        float random = Random.value;

        GameObject orbToSpawn = random switch
        {
            <= 0.7f => smallXpOrb,
            <= 0.95f => mediumXpOrb,
            _ => largeXpOrb
        };
        
        if (orbToSpawn != null)
        {
            Instantiate(orbToSpawn, position, Quaternion.identity);
        }
    }

    private void SpawnSuperXpOrb(Vector3 position)
    {
        Instantiate(superXpOrb, position, Quaternion.identity);
    }
}
