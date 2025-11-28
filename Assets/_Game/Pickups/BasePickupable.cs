using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class BasePickupable : MonoBehaviour
{
    protected abstract void OnPickup(GameObject gameObject_);

    protected void OnTriggerEnter2D(Collider2D other)
    {
        print("OnTriggerEnter");
        OnPickup(other.gameObject);
    }
}