using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CloneMovement : MonoBehaviour
{
    public PlayerMovement movementBase;

    public Vector2 velocity = Vector2.zero;

    PlayerTerrainCollision collision;

    bool isGrounded = false;

    // Start is called before the first frame update
    public void Initialize(PlayerMovement playerMovement)
    {
        movementBase = playerMovement;
        collision = new PlayerTerrainCollision(transform, movementBase.terrain);
        velocity = movementBase.velocity;
        velocity.x *= -1;
        isGrounded = movementBase.isGrounded;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = transform.position;

        if (!isGrounded && collision.FloorCheck(velocity))
        {
            velocity.y = 0f;
            pos.y = Mathf.Floor(pos.y) + collision.playerHeight;
            isGrounded = true;
        }

        if (isGrounded)
        {
            isGrounded = collision.AboveFloor(velocity);
            Jump();
        }
        else
        {
            velocity += movementBase.gravity * Time.deltaTime;
        }

        if (collision.CeilingCheck(velocity))
        {
            velocity.y = 0f;
            pos.y = Mathf.Ceil(pos.y) - collision.playerHeight * 1.05f;
        }

        if (collision.WallCheckLeft(velocity))
        {
            velocity.x = 0f;
            pos.x = Mathf.Floor(pos.x) + collision.playerWidth * 1.05f;
        }
        else if (collision.WallCheckRight(velocity))
        {
            velocity.x = 0f;
            pos.x = Mathf.Ceil(pos.x) - collision.playerWidth * 1.05f;
        }
        else
        {
            Move();
        }

        pos += velocity * Time.deltaTime;
        transform.position = pos;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = movementBase.jumpSpeed;
            isGrounded = false;
        }
    }

    void Move()
    {
        velocity.x += Input.GetKey(KeyCode.A) ? movementBase.speed : 0;
        velocity.x += Input.GetKey(KeyCode.D) ? -movementBase.speed : 0;
        velocity.x = Mathf.Clamp(velocity.x, -4f, 4f);
        velocity.x *= 0.95f;
    }
}
