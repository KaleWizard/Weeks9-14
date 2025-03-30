using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 velocity;

    public float speed = 5f;
    public float jumpSpeed = 8f;

    public Vector2 gravity = Vector2.down * 15f;

    public Tilemap terrain;

    PlayerTerrainCollision collision;

    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        collision = new PlayerTerrainCollision(transform, terrain);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = transform.position;

        if (!isGrounded && collision.FloorCheck())
        {
            velocity.y = 0f;
            pos.y = Mathf.Floor(pos.y) + collision.playerHeight;
            isGrounded = true;
        }

        if (isGrounded)
        {
            isGrounded = collision.AboveFloor();
            Jump();
        } else
        {
            velocity += gravity * Time.deltaTime;
        }

        if (collision.CeilingCheck())
        {
            velocity.y = 0f;
            pos.y = Mathf.Ceil(pos.y) - collision.playerHeight * 1.05f;
        }

        if (collision.WallCheckLeft())
        {
            velocity.x = 0f;
            pos.x = Mathf.Floor(pos.x) + collision.playerWidth * 1.05f;
        } else if (collision.WallCheckRight())
        {
            velocity.x = 0f;
            pos.x = Mathf.Ceil(pos.x) - collision.playerWidth * 1.05f;
        } else
        {
            Move();
        }

        pos += velocity * Time.deltaTime;
        transform.position = pos;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) ||  Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpSpeed;
            isGrounded = false;
        }
    }

    void Move()
    {
        velocity.x += Input.GetKey(KeyCode.D) ? speed : 0;
        velocity.x += Input.GetKey(KeyCode.A) ? -speed : 0;
        velocity.x = Mathf.Clamp(velocity.x, -4f, 4f);
        velocity.x *= 0.95f;
    }
}
