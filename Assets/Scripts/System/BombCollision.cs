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

    float initialSpeed = 4f;

    public void Throw(Vector2 vel)
    {
        velocity = vel * initialSpeed;
        StartCoroutine(BombMovement());
    }

    IEnumerator BombMovement()
    {
        while (!CollidingWithTerrain())
        {
            Vector2 pos = transform.position;
            velocity += physics.gravity * Time.deltaTime;
            pos += velocity * Time.deltaTime;
            transform.position = pos;
            yield return null;
        }
        Destroy(gameObject);
    }

    bool CollidingWithTerrain()
    {
        Vector3Int cellPos = physics.terrain.WorldToCell(transform.position);

        // Check top edge
        if (!IsCellEmpty(cellPos, 0, 1) && (transform.position.y % 1) > 1 - bombRadius)
            return true;
        // Check bottom edge
        if (!IsCellEmpty(cellPos, 0, -1) && (transform.position.y % 1) < bombRadius)
            return true;
        // Check left 
        if (!IsCellEmpty(cellPos, -1, 0) && (transform.position.x % 1) < bombRadius)
            return true;
        // Check right edge
        if (!IsCellEmpty(cellPos, 1, 0) && (transform.position.x % 1) > 1 - bombRadius)
            return true;

        // Get position of bomb's cell in the world
        Vector3 cellWorldPos = physics.terrain.CellToWorld(cellPos);
        cellWorldPos.z = transform.position.z;

        // Check top left corner
        if (!IsCellEmpty(cellPos, -1, 1) && CollidingWithPoint(cellWorldPos + Vector3.up))
            return true;
        // Check top right corner
        if (!IsCellEmpty(cellPos, 1, 1) && CollidingWithPoint(cellWorldPos + Vector3.up + Vector3.right))
            return true;
        // Check bottom left corner
        if (!IsCellEmpty(cellPos, -1, -1) && CollidingWithPoint(cellWorldPos))
            return true;
        // Check bottom right corner
        if (!IsCellEmpty(cellPos, 1, -1) && CollidingWithPoint(cellWorldPos + Vector3.right))
            return true;

        // No collisions found, so return false
        return false;
    }

    bool IsCellEmpty(Vector3Int cell, int xModifier, int yModifier)
    {
        cell.x += xModifier;
        cell.y += yModifier;
        TileBase tile = physics.terrain.GetTile(cell);
        return tile == null;
    }

    bool CollidingWithPoint(Vector3 point)
    {
        return Vector3.Distance(transform.position, point) < bombRadius;
    }
}
