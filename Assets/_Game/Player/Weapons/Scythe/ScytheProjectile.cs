using UnityEngine;
using System.Collections.Generic;

public class ScytheProjectile : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float lifetime = 0.5f;

    [Header("Combat Settings")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float knockbackForce = 5f;

    [Header("Animation Settings")]
    [SerializeField] private List<Sprite> animationSprite = new List<Sprite>();

    private SpriteRenderer spriteRenderer;
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    private float animationTimer = 0;

    private int animationIndex = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationTimer = lifetime;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (animationTimer >= lifetime * (2f / 3f))
        {
            spriteRenderer.sprite = animationSprite[0];
        }
        else if (animationTimer >= lifetime / 3f)
        {
            spriteRenderer.sprite = animationSprite[1];
        }
        else
        {
            spriteRenderer.sprite = animationSprite[2];
        }

        animationTimer -= Time.deltaTime;

        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BaseMonster enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}