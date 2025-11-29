using UnityEngine;

public sealed class Shield : BasePickupable
{
    [field: SerializeField] public int ShieldValue { get; private set; } = 100;

    protected sealed override void OnPickup(GameObject gameObject_)
    {
        if (gameObject_.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            playerHealth.ApplyShield(ShieldValue);
            Destroy(gameObject);
        }
    }
}