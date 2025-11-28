using UnityEngine;
using System.Collections.Generic;

public class ScytheProjectile : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = -360f;
    [SerializeField] private float lifetime = 0.5f;

    [Header("Combat Settings")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float knockbackForce = 5f;

    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hitEnemies.Contains(other.gameObject))
        {
            hitEnemies.Add(other.gameObject);

            // enemy.takeDamage();
        }
    }
}