using UnityEngine;

public sealed class Shield : BasePickupable
{
    [SerializeField] public int ShieldValue { get; private set; }

    protected override void OnPickup(GameObject gameObject_)
    {
        return;
        //throw new System.NotImplementedException();
    }
}