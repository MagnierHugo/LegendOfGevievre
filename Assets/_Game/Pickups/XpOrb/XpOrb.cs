using UnityEngine;

public sealed class XpOrb : BasePickupable
{
    [field: SerializeField] public int XpValue { get; private set; }

    protected sealed override void OnPickup(GameObject gameObject_)
    {
        if (gameObject.TryGetComponent<PlayerXP>(out var playerXP))
            playerXP.EarnXP(XpValue);
    }
}