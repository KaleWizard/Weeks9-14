
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTerrainCollision
{
    public float playerWidth = 0.2f;
    public float playerHeight = 0.4f;

    Transform player;

    Tilemap terrain;

    public PlayerTerrainCollision(Transform playerTransform, Tilemap terrainGrid)
    {
        player = playerTransform;
        terrain = terrainGrid;
    }

    // Checks if the player is on the floor
    public bool FloorCheck(Vector2 velocity)
    {
        // If player is moving up, player is not on the floor
        if (velocity.y > 0) return false;

        Vector2 pos = player.position;

        // Determine if cell at bottom-center of player's hitbox is non-empty
        bool inFloor = TileAtPoint(pos + Vector2.down * playerHeight, Vector3Int.zero) != null;

        return inFloor;
    }

    // Determines whether the cell below the player's cell is terrain
    public bool AboveFloor()
    {
        Vector2 pos = player.position;
        // Get cell(s) below left and right sides of player hitbox
        TileBase leftTile = TileAtPoint(pos + Vector2.left * playerWidth, Vector3Int.down);
        TileBase rightTile = TileAtPoint(pos + Vector2.right * playerWidth, Vector3Int.down);

        // Player is above terrain if at least one cell is non-empty
        return leftTile != null || rightTile != null;
    }

    // Checks if the player is touching the ceiling
    public bool CeilingCheck(Vector2 velocity)
    {
        // If player is moving down, player is not touching the ceiling
        if (velocity.y < 0) return false;

        Vector2 pos = player.position;

        // Determine if cell at top-center of player hitbox is non-empty
        bool inCeiling = TileAtPoint(pos + Vector2.up * playerHeight, Vector3Int.zero) != null;

        return inCeiling;
    }

    // Checks if the player is touching a wall to their left
    public bool WallCheckLeft(Vector2 velocity)
    {
        // If player is moving right, player is not touching a wall
        if (velocity.x > 0) return false;
        Vector2 pos = player.position;

        // Determine if cell at left-center of player hitbox is non-empty
        bool inWall = TileAtPoint(pos + Vector2.left * playerWidth, Vector3Int.zero) != null;

        return inWall;
    }

    // Checks if the player is touching a wall to their right
    public bool WallCheckRight(Vector2 velocity)
    {
        // If player is moving left, player is not touching a wall
        if (velocity.x < 0) return false;
        Vector2 pos = player.position;

        // Determine if cell at right-center of player hitbox is non-empty
        bool inWall = TileAtPoint(pos + Vector2.right * playerWidth, Vector3Int.zero) != null;

        return inWall;
    }

    // Returns the tile at (point + modifier) if it exists, null otherwise
    TileBase TileAtPoint(Vector2 point, Vector3Int modifier)
    {
        return terrain.GetTile(terrain.WorldToCell(point) + modifier);
    }
}
