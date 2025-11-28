using UnityEngine;

public class Sword : MonoBehaviour
{
    private Transform ownerTransform;

    private float initialSearchTimer;
    private const float SEARCH_DELAY = 0.5f; 

    [Header("Orbit Settings")]
    [Tooltip("Radius of the circle the item orbits in")]
    [SerializeField]
    private float orbitRadius = 1.5f;
    [Tooltip("Speed of rotation in degrees per second")]
    [SerializeField]
    private float rotationSpeed = 360f;

    [Header("Damage Settings")]
    [SerializeField]
    private int damageAmount = 5;
    [Tooltip("Time in seconds between damage ticks on the same enemy")]
    [SerializeField]
    private float damageCooldown = 0.5f;

    private float angle; 
    private float lastDamageTime;
    private bool isAttached = false; 

    private void Start()
    {
        angle = Random.Range(0f, 360f);
        lastDamageTime = Time.time;
        initialSearchTimer = 0f;
    }

    private void Update()
    {
        if (!isAttached)
        {
            initialSearchTimer += Time.deltaTime;
            if (initialSearchTimer >= SEARCH_DELAY)
            {
                FindAndAttachToRandomEnemy();
            }
            return;
        }


        if (ownerTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        angle += rotationSpeed * Time.deltaTime;
        if (angle >= 360f) angle -= 360f;

        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * orbitRadius;
        float y = Mathf.Sin(radians) * orbitRadius;

        Vector3 newPosition = new Vector3(x, y, ownerTransform.position.z);
        transform.position = ownerTransform.position + newPosition;
    }

    private void FindAndAttachToRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            return;
        }

        System.Collections.Generic.List<GameObject> availableEnemies = new System.Collections.Generic.List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponentInChildren<Sword>() == null)
            {
                availableEnemies.Add(enemy);
            }
        }

        if (availableEnemies.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, availableEnemies.Count);
        GameObject targetEnemy = availableEnemies[randomIndex];

        ownerTransform = targetEnemy.transform;
        transform.SetParent(ownerTransform);
        isAttached = true;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (!isAttached) return; 

        if (Time.time < lastDamageTime + damageCooldown)
            return;

        if (other.CompareTag("Enemy") && other.transform != ownerTransform)
        {
            BaseMonster targetMonster = other.GetComponent<BaseMonster>();

            if (targetMonster != null)
            {
                targetMonster.TakeDamage(damageAmount);

                lastDamageTime = Time.time;
            }
        }
    }
}