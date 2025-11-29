using System.Collections;
using UnityEngine;
[RequireComponent (typeof(CircleCollider2D))]
public class PlayerMagnet : MonoBehaviour
{
    [Header("Magnet Settings")]
    [SerializeField] private float pickupRange = 1f;
    private CircleCollider2D magnetCollider;

    private void Awake()
    {
        magnetCollider = GetComponent<CircleCollider2D>();
        magnetCollider.isTrigger = true;
        UpdateMagnetRange();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BasePickupable basePickupable))
        {
            basePickupable.StartAttraction(transform.parent);
        }
    }

    public void IncreaseRange(float amount)
    {
        pickupRange += amount;
        UpdateMagnetRange();
        StartCoroutine(ResetRange());
    }

    private IEnumerator ResetRange()
    {
        yield return new WaitForSeconds(3);
        pickupRange = 1f;
        UpdateMagnetRange();
    }

    private void UpdateMagnetRange()
    {
        magnetCollider.radius = pickupRange;
    }
}