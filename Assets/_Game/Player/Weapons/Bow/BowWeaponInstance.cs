using UnityEngine;

public class BowWeaponInstance : MonoBehaviour
{
    private GameObject arrow = null;

    private Vector3 startingPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 playerPosition = new Vector3(-5.0f, -5.0f, 0.0f);

    private Vector2 attackDirection = new Vector2(1.0f, 1.0f); // Straight 45° angle
    private Vector2 arrowPosition = Vector2.zero;

    private const float SHOOTS_ELAPSED_TIME = 1.0f;
    private float timer = SHOOTS_ELAPSED_TIME;

    private const int range = 20;

    private bool isAlive = false;

    public void Start()
    {
        InitArrow();
    }

    public void Update()
    {
        if (isAlive)
            Attack();
        else
            InitArrow();
    }

    public void InitArrow()
    {
        startingPosition = playerPosition;
        arrow = GetComponent<GameObject>();
        attackDirection = new Vector2(playerPosition.normalized.x, playerPosition.normalized.y);
        isAlive = true;
    }

    public void Attack()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if ((int)timer % 2 == 0)
            {
                OffsetDeltaTime();
            }
            arrowPosition += attackDirection;
            if (Mathf.Abs(startingPosition.x - arrowPosition.x) > range)
            {
                timer = SHOOTS_ELAPSED_TIME;
                Destroy(arrow);
                isAlive = false;
            }
        }
    }

    public void Upgrade()
    {

    }

    // Special ability - just randomize path, takes ~1s
    public void OffsetDeltaTime()
    {
        attackDirection += new Vector2(Random.Range(-1.0f, 1.0f) * Time.deltaTime, Random.Range(-1.0f, 1.0f) * Time.deltaTime);
    }
}