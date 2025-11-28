using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Sprite> playerSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 velocity = Vector2.zero;

        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        if (left ^ right)
            velocity.x = right ? speed : -speed;

        bool up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        if (up ^ down)
            velocity.y = up ? speed : -speed;

        transform.position += Time.deltaTime * Vector3.ClampMagnitude(velocity, speed);

        if (velocity.y > 0)
            spriteRenderer.sprite = playerSprite[0];
        if (velocity.x > 0)
            spriteRenderer.sprite = playerSprite[1];
        if (velocity.y < 0)
            spriteRenderer.sprite = playerSprite[2];
        if (velocity.x < 0)
            spriteRenderer.sprite = playerSprite[3];

    }
}