using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public abstract class BasePickupable : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    private Transform target;
    private float currentSpeed;
    private bool isAttracted = false;

    protected abstract void OnPickup(GameObject gameObject_);

    protected void OnTriggerEnter2D(Collider2D other)
    {
        OnPickup(other.gameObject);
    }

    public void StartAttraction(Transform playerTransform)
    {
        if (isAttracted) return;

        target = playerTransform;
        currentSpeed = baseSpeed;
        isAttracted = true;
    }

    private void Update()
    {
        if (!isAttracted || target == null) return;

        currentSpeed += acceleration * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            transform.position = target.position;
        }
    }
}