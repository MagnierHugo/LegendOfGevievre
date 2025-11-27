using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

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
    }
}