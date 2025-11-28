using UnityEngine;

public sealed class HealPotion : BasePickupable
{
    [field: SerializeField] public int HealValue { get; private set; }

    protected sealed override void OnPickup(GameObject gameObject_)
    {
        if (gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
            playerHealth.Heal(HealValue);
    }
}