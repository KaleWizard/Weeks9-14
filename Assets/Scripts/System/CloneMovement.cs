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

        // If clone is on the ground after being airborne
        if (!isGrounded && collision.FloorCheck(velocity))
        {
            // Set clone y position and velocity to floor height and zero respectively
            velocity.y = 0f;
            pos.y = Mathf.Floor(pos.y) + collision.playerHeight;
            // Clone is now on ground
            isGrounded = true;
        }

        // If clone is on ground
        if (isGrounded)
        {
            // Check if clone is still on ground
            isGrounded = collision.AboveFloor();
            // Check if clone wants to perform a jump
            Jump();
        }
        // If clone is airborne, induce gravity
        else
        {
            velocity += movementBase.gravity * Time.deltaTime;
        }

        // If clone is in a ceiling, eject them from it
        if (collision.CeilingCheck(velocity))
        {
            velocity.y = 0f;
            pos.y = Mathf.Ceil(pos.y) - collision.playerHeight * 1.05f;
        }

        // If clone is in a wall to their left, eject them from it
        if (collision.WallCheckLeft(velocity))
        {
            velocity.x = 0f;
            pos.x = Mathf.Floor(pos.x) + collision.playerWidth * 1.05f;
        }
        // If clone is in a wall to their right, eject them from it
        else if (collision.WallCheckRight(velocity))
        {
            velocity.x = 0f;
            pos.x = Mathf.Ceil(pos.x) - collision.playerWidth * 1.05f;
        }
        // If clone is not in a wall, move them based on player input
        else
        {
            Move();
        }

        // Update position based on velocity
        pos += velocity * Time.deltaTime;
        transform.position = pos;
    }

    void Jump()
    {
        // If player makes a jump input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            // Increase clone vertical velocity and make them airborne
            velocity.y = movementBase.jumpSpeed;
            isGrounded = false;
        }
    }

    void Move()
    {
        // Increase horizontal velocity based on player input
        velocity.x += Input.GetKey(KeyCode.A) ? movementBase.speed : 0; // right
        velocity.x += Input.GetKey(KeyCode.D) ? -movementBase.speed : 0; // left

        // Clamp horizontal velocity to player speed
        velocity.x = Mathf.Clamp(velocity.x, -movementBase.speed, movementBase.speed);
        // Induce slight friction
        velocity.x *= 0.95f;
    }
}
