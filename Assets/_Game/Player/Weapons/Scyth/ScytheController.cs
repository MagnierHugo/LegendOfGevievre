using UnityEngine;

public class ScytheController : MonoBehaviour
{
    [Header("Weapon Config")]
    [SerializeField] private GameObject scythePrefab;
    [SerializeField] private float cooldown = 1.5f;
    [SerializeField] private Transform playerTransform;

    private float cooldownTimer;

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = cooldown;
        }
    }

    private void Attack()
    {
        if (scythePrefab == null) return;

        GameObject scythe = Instantiate(scythePrefab, transform.position, Quaternion.identity);

        scythe.transform.SetParent(transform);
    }
}