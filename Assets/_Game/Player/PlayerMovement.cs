using UnityEngine;

[DefaultExecutionOrder(-1)]
public sealed class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    [SerializeField] private float speed;
    public Vector2 Velocity;

    private Animator animator;
    private Transform cameraTransform;

    public Vector2 minLimits;
    public Vector2 maxLimits;
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();

        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("No Main Camera found!");
        }
    }

    private void Update()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            input.x = -1;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            input.x = 1;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            input.y = 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            input.y = -1;

        // Clamp diagonal movement to keep speed consistent
        input = Vector2.ClampMagnitude(input, 1);
        Vector2 desiredPosition = (Vector2)transform.position + input * speed * Time.deltaTime;

        Vector3 moveDir;
        if (cameraTransform != null)
        {
            moveDir = (cameraTransform.right * input.x) + (cameraTransform.up * input.y);
            moveDir.z = 0;
            moveDir.Normalize();
        }
        else
        {
            moveDir = input;
        }

        Velocity = moveDir * speed;
        transform.position += (Vector3)Velocity * Time.deltaTime;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minLimits.x, maxLimits.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minLimits.y, maxLimits.y);

        transform.position = desiredPosition;
        
        // Movement animation direction priority
        if (input.x != 0)
        {
            // Horizontal priority
            animator.SetFloat("MoveX", input.x);
            animator.SetFloat("MoveY", 0);
        }
        else
        {
            // Only vertical when no horizontal
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", input.y);
        }

        animator.SetBool("IsMoving", input.sqrMagnitude > 0.01f);
    }
}