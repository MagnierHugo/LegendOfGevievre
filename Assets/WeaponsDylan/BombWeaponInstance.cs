using UnityEngine;

public class BombWeaponInstance : MonoBehaviour
{
    [SerializeField] private GameObject projectile = null;

    [SerializeField] private Vector3 playerPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 startingPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector2 explosionPosition = Vector2.zero;

    [SerializeField] private float jumpIntensity = 2.0f;
    private const float SHOOTS_ELAPSED_TIME = 1.0f;
    private const float GRAVITY = 9.81f;
    private float timer = SHOOTS_ELAPSED_TIME;
    private float projectileSpeed = 5.0f;

    private static int range = 20;
    private static int damage = 50;

    private bool isAlive = false;
    
    public BombWeaponInstance() {}

    public void Start()
    {
        Instantiate(projectile, startingPosition, Quaternion.identity);
        startingPosition = playerPosition;
        isAlive = true;
    }

    public void Update()
    {
        Attack();
    }

    public void Attack()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            // Explodes in a random spot
            explosionPosition = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
            if (isAlive)
            {
                projectile.transform.position += new Vector3(projectileSpeed * Time.deltaTime, jumpIntensity * Time.deltaTime, 0);
                jumpIntensity -= Time.deltaTime * GRAVITY;
                if (projectile.transform.position.y <= startingPosition.y)
                {
                    Explode(explosionPosition);
                    Destroy(projectile);
                    isAlive = false;
                    timer = SHOOTS_ELAPSED_TIME;
                }
            }
        }
    }

    public void Upgrade()
    {
        damage += 10;
        range += 5;
    }

    private void Explode(Vector2 position)
    {
        Debug.Log("Explosion at " + position.ToString() + ", it's a decoy :D 1!!!!1!1!1!1!!");
    }
}