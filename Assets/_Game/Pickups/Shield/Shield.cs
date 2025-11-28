using UnityEngine;

public sealed class Shield : BasePickupable
{
    [field: SerializeField] public int ShieldValue { get; private set; }

    protected sealed override void OnPickup(GameObject gameObject_)
    {
        if (gameObject_.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            playerHealth.Shield(ShieldValue);
            Destroy(gameObject);
        }
    }
}