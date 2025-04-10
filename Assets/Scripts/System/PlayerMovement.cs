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

    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        collision = new PlayerTerrainCollision(transform, terrain);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = transform.position;

        // If player is on the ground after being airborne
        if (!isGrounded && collision.FloorCheck(velocity))
        {
            // Set player y position and velocity to floor height and zero respectively
            velocity.y = 0f;
            pos.y = Mathf.Floor(pos.y) + collision.playerHeight;
            // Player is now on ground
            isGrounded = true;
        }

        // If player is on ground
        if (isGrounded)
        {
            // Check if player is still on ground
            isGrounded = collision.AboveFloor();
            // Check if player wants to perform a jump
            Jump();
        } 
        // If player is airborne, induce gravity
        else
        {
            velocity += gravity * Time.deltaTime;
        }

        // If player is in a ceiling, eject them from it
        if (collision.CeilingCheck(velocity))
        {
            velocity.y = 0f;
            pos.y = Mathf.Ceil(pos.y) - collision.playerHeight * 1.05f;
        }

        // If player is in a wall to their left, eject them from it
        if (collision.WallCheckLeft(velocity))
        {
            velocity.x = 0f;
            pos.x = Mathf.Floor(pos.x) + collision.playerWidth * 1.05f;
        }
        // If player is in a wall to their right, eject them from it
        else if (collision.WallCheckRight(velocity))
        {
            velocity.x = 0f;
            pos.x = Mathf.Ceil(pos.x) - collision.playerWidth * 1.05f;
        } 
        // If player is not in a wall, move them based on player input
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
        if (Input.GetKeyDown(KeyCode.W) ||  Input.GetKeyDown(KeyCode.Space))
        {
            // Increase player vertical velocity and make them airborne
            velocity.y = jumpSpeed;
            isGrounded = false;
        }
    }

    void Move()
    {
        // Increase horizontal velocity based on player input
        velocity.x += Input.GetKey(KeyCode.D) ? speed : 0; // right
        velocity.x += Input.GetKey(KeyCode.A) ? -speed : 0; // left

        // Clamp horizontal velocity to player speed
        velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
        // Induce slight friction
        velocity.x *= 0.95f;
    }
}
