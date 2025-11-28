using UnityEngine;

public sealed class Shield : BasePickupable
{
    [SerializeField] public int ShieldValue { get; private set; }

    protected override void OnPickup(GameObject gameObject_)
    {
        Debug.Log($"{nameof(Shield)}::{nameof(OnPickup)}");
    }
}