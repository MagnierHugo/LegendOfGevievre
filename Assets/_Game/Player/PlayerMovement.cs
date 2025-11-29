using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public sealed class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    [SerializeField] private float speed;
    public Vector2 Velocity;

    private Animator animator;

    private void Awake()
    {
        Instance = this;
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

        Velocity = input * speed;
        transform.position += (Vector3)Velocity * Time.deltaTime;
        
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