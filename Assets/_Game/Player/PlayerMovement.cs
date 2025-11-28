using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[DefaultExecutionOrder(-1)]
public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public Vector2 Velocity;

    private Animator animator;

    public Vector2 minLimits;
    public Vector2 maxLimits;
    private void Start()
    {
        animator = GetComponent<Animator>();
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