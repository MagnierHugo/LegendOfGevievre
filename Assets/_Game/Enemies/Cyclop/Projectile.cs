using UnityEngine;


public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 2;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 3;
    private float spawnTime;

    private void Awake() => spawnTime = Time.time;

    private void Update()
    {
        transform.position += Time.deltaTime * speed * direction;
        if (spawnTime + lifeTime > Time.time)
            return;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDirection(Transform player)
    {
        direction = ((player?.position ?? Vector3.zero) - transform.position).normalized;
    }
}
