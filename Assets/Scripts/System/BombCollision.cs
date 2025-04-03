using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombCollision : MonoBehaviour
{
    public Vector2 velocity;

    public PlayerMovement physics;

    float bombRadius = 0.15f;

    float throwingSpeed = 4f;

    public void Throw(Vector2 vel)
    {
        // Initial speed is player speed plus throwing speed
        velocity = vel * (throwingSpeed + physics.velocity.magnitude);
        StartCoroutine(BombMovement());
    }

    IEnumerator BombMovement()
    {
        while (!CollidingWithTerrain())
        {
            // Update velocity change due to gravity
            velocity += physics.gravity * Time.deltaTime;

            // Update position change due to velocity
            Vector2 pos = transform.position;
            pos += velocity * Time.deltaTime;
            transform.position = pos;

            yield return null;
        }
        // TODO spawn explosion

        // Bomb is done so destroy it
        Destroy(gameObject);
    }

    // Returns true if the bomb is colliding with terrain, false otherwise
    bool CollidingWithTerrain()
    {
        // Get cell position of bomb
        Vector3Int cellPos = physics.terrain.WorldToCell(transform.position);

        // Check top edge for collision with bomb
        if (!IsCellEmpty(cellPos, 0, 1) && (transform.position.y + bombRadius > physics.terrain.CellToWorld(cellPos).y + 1f))
            return true;
        // Check bottom edge for collision with bomb
        if (!IsCellEmpty(cellPos, 0, -1) && (transform.position.y - bombRadius < physics.terrain.CellToWorld(cellPos).y))
            return true;
        // Check left for collision with bomb
        if (!IsCellEmpty(cellPos, -1, 0) && (transform.position.x - bombRadius < physics.terrain.CellToWorld(cellPos).x))
            return true;
        // Check right edge for collision with bomb
        if (!IsCellEmpty(cellPos, 1, 0) && (transform.position.x + bombRadius > physics.terrain.CellToWorld(cellPos).x + 1f))
            return true;

        // Get position of bomb's cell in the world
        Vector3 cellWorldPos = physics.terrain.CellToWorld(cellPos);
        cellWorldPos.z = transform.position.z;

        // Check top left corner for collision with bomb
        if (!IsCellEmpty(cellPos, -1, 1) && CollidingWithPoint(cellWorldPos + Vector3.up))
            return true;
        // Check top right corner for collision with bomb
        if (!IsCellEmpty(cellPos, 1, 1) && CollidingWithPoint(cellWorldPos + Vector3.up + Vector3.right))
            return true;
        // Check bottom left corner for collision with bomb
        if (!IsCellEmpty(cellPos, -1, -1) && CollidingWithPoint(cellWorldPos))
            return true;
        // Check bottom right corner for collision with bomb
        if (!IsCellEmpty(cellPos, 1, -1) && CollidingWithPoint(cellWorldPos + Vector3.right))
            return true;

        // No collisions found, so return false
        return false;
    }

    // Returns true if the cell at (cell + (xModifier, yModifier)) in physics.terrain is empty, false otherwise
    bool IsCellEmpty(Vector3Int cell, int xModifier, int yModifier)
    {
        cell.x += xModifier;
        cell.y += yModifier;
        TileBase tile = physics.terrain.GetTile(cell);
        return tile == null;
    }

    // Returns true if point is within the bomb's radius, false otherwise
    bool CollidingWithPoint(Vector3 point)
    {
        return Vector3.Distance(transform.position, point) < bombRadius;
    }
}
