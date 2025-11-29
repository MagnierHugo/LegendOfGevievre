using UnityEngine;

public class GevievreGroupParent : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    private Vector2 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        velocity = (GameManager.PlayerTransform.position - transform.position).normalized * speed;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }
}
