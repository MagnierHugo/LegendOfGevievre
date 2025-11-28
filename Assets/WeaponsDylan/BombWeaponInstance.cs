using UnityEngine;

public class BombWeaponInstance : MonoBehaviour
{
    private Vector3 startingPosition = Vector3.zero;
    private Vector2 explosionPosition = Vector2.zero;

    private const float SHOOTS_ELAPSED_TIME = 2.0f;
    private const float GRAVITY = 9.81f;
    private float jumpIntensity = 8.0f;
    private float projectileSpeed = 4.0f;
    private float timer = SHOOTS_ELAPSED_TIME;

    private static int range = 20;
    private static int damage = 50;

    private bool isAlive = false;

    GameObject explosion;

    public void Start()
    {
        InitInstance();
    }

    public void Update()
    {
        ParabollicMovement();
    }

    public void InitInstance()
    {
        isAlive = true;
        explosion = GameObject.Find("Explosion");
        explosion.SetActive(false);
    }

    public void ParabollicMovement()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (isAlive)
            {
                transform.position += new Vector3(projectileSpeed * Time.deltaTime, jumpIntensity * Time.deltaTime, 0);
                jumpIntensity -= Time.deltaTime * GRAVITY;
                if (transform.position.y <= startingPosition.y)
                {
                    explosion?.SetActive(true);
                    explosionPosition = new Vector2(Random.Range(0, 10), Random.Range(0, 10));
                    Explode();
                    Destroy(gameObject);
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

    private void Explode()
    {
        // TODO: Explosion visual
        Debug.Log("Explosion at " + explosionPosition.ToString() + ", it's a decoy :D 1!!!!1!1!1!1!!");
        Debug.Log("The real me is at : " + transform.position.ToString() + " !!111!1!1!1!!1!1");
    }
}