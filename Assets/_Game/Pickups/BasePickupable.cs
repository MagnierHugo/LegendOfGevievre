using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class BasePickupable : MonoBehaviour
{
    protected abstract void OnPickup(GameObject gameObject_);

    protected void OnTriggerEnter(Collider other) => OnPickup(other.gameObject);
}