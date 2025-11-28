using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class ScytheProjectile : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float lifetime = 0.5f;

    [Header("Combat Settings")]
    [SerializeField] private int damage = 10;

    [Header("Animation Settings")]
    [SerializeField] private List<Sprite> animationSprite = new List<Sprite>();

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float animationTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        animationTimer = lifetime;
        rb.bodyType = RigidbodyType2D.Kinematic;

        Destroy(gameObject, lifetime);
    }

    public void Init(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        HandleAnimation();

        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void HandleAnimation()
    {
        if (animationSprite.Count == 0) return;

        float progress = 1f - (animationTimer / lifetime);
        int index = Mathf.FloorToInt(progress * animationSprite.Count);

        index = Mathf.Clamp(index, 0, animationSprite.Count - 1);

        spriteRenderer.sprite = animationSprite[index];

        animationTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BaseMonster enemy))
        {
            enemy.TakeDamage(damage, false);
        }
    }
}