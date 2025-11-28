using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Sprite> playerSprite;
    public Vector2 Velocity;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Velocity = Vector2.zero;

        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        if (left ^ right)
            Velocity.x = right ? speed : -speed;

        bool up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        if (up ^ down)
            Velocity.y = up ? speed : -speed;

        transform.position += Time.deltaTime * Vector3.ClampMagnitude(Velocity, speed);

        if (Velocity.y > 0)
            spriteRenderer.sprite = playerSprite[0];
        else if (Velocity.y < 0)
            spriteRenderer.sprite = playerSprite[2];

        if (Velocity.x > 0)
            spriteRenderer.sprite = playerSprite[1];
        else if (Velocity.x < 0)
            spriteRenderer.sprite = playerSprite[3];

    }
}