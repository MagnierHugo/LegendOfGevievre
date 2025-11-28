using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwordWeaponInstance : MonoBehaviour
{
    private Transform ownerTransform;

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

    [SerializeField] private float lifeTime = 3f;

    private float angle;
    private bool isAttached = false;
    private event Action onSwordDestruction;

    private void Start()
    {
        angle = Random.Range(0f, 360f);
        FindAndAttachToRandomEnemy();
        Destroy(gameObject, lifeTime);
    }

    private void OnDestroy() => onSwordDestruction?.Invoke();

    public void Init(Action onDestruction) => onSwordDestruction += onDestruction;

    private void Update()
    {
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
            return;

        List<GameObject> availableEnemies = new List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponentInChildren<SwordWeaponInstance>() == null)
                availableEnemies.Add(enemy);
        }

        if (availableEnemies.Count == 0)
            return;

        int randomIndex = Random.Range(0, availableEnemies.Count);
        GameObject targetEnemy = availableEnemies[randomIndex];

        ownerTransform = targetEnemy.transform;
        transform.SetParent(ownerTransform);
        isAttached = true;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (!isAttached)
            return; 

        if (other.CompareTag("Enemy") && other.transform != ownerTransform)
        {
            BaseMonster targetMonster = other.GetComponent<BaseMonster>();

            if (targetMonster != null)
                targetMonster.TakeDamage(damageAmount);
        }
    }
}