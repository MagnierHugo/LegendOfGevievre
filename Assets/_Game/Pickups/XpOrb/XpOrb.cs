using UnityEngine;

public sealed class XpOrb : BasePickupable
{
    [field: SerializeField] public int XpValue { get; private set; }

    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float acceleration = 10f;

    private Transform target;
    private float currentSpeed;
    private bool isAttracted = false;

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

    protected sealed override void OnPickup(GameObject gameObject_)
    {
        if (gameObject_.TryGetComponent<PlayerXP>(out var playerXP))
        {
            playerXP.EarnXP(XpValue);
            Destroy(gameObject);
        }
    }
}