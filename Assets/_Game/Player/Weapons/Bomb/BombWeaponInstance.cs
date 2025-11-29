using UnityEngine;

public class BombWeaponInstance : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float gravity = 9.81f;
    public float initialJumpForce = 8f;
    public float projectileSpeed = 2f;
    public float destroySmokeDelay = 1.0f;

    [Header("Explosion Prefab")]
    public GameObject explosionPrefab;

    // Static upgrade stats
    private static int range = 5;
    public static int level = 1;
    private static int id = 0;

    // Flags
    private int index = -1;

    // Movement State (unique per bomb)
    private float launchForce;
    private Vector3 target;
    private Vector2 direction;

    public void Init(Vector2 direction_, int currentLevel)
    {
        level = currentLevel;
        direction = direction_;
        index = id;
        launchForce = initialJumpForce * Random.Range(0.8f, 1.2f);
        target = transform.position + (Vector3)direction * range;
        //Debug.Log("Target is " + target.ToString());
        id++;
    }
    
    public void Upgrade()
    {
        range++;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Move parabolic
        Vector3 velocity = new Vector3(direction.x * projectileSpeed, direction.y * launchForce) * Time.deltaTime;

        transform.position += velocity;

        launchForce -= gravity * Time.deltaTime;

        // Impact check - has to go down
        if (transform.position.y <= target.y && launchForce < 0)
        {
            ExplodeHere();
            Destroy(gameObject);
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x > Screen.width || screenPos.y > Screen.height || screenPos.y < 0 || screenPos.x < 0)
        {
            //Debug.Log("They missed :skull: nahh");
            Destroy(gameObject);
        }
    }

    void ExplodeHere()
    {
        GameObject e = Instantiate(
            explosionPrefab,
            new Vector3(target.x + Random.Range(-5.0f, 5.0f), target.y + Random.Range(-5.0f, 5.0f)),
            Quaternion.identity
        );
        // TODO: Damage the surrounding enemies depending on that position and the range
        Destroy(e, destroySmokeDelay);

        //Debug.Log($"Bomb #{index} exploded at {e.transform.position}");
    }

}