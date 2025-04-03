
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class PlayerTerrainCollision
{
    public float playerWidth = 0.2f;
    public float playerHeight = 0.4f;

    Transform player;
    PlayerMovement playerMovement;

    Tilemap terrain;

    public PlayerTerrainCollision(Transform playerTransform, Tilemap terrainGrid)
    {
        player = playerTransform;
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        terrain = terrainGrid;
    }

    public bool FloorCheck()
    {
        if (playerMovement.velocity.y > 0) return false;
        Vector2 pos = player.position;

        if (!AboveFloor())
        {
            return false;
        }

        bool inFloor = TileAtPoint(pos + Vector2.down * playerHeight, Vector3Int.zero) != null;

        return inFloor;
    }

    public bool AboveFloor()
    {
        Vector2 pos = player.position;
        TileBase leftTile = TileAtPoint(pos + Vector2.left * playerWidth, Vector3Int.down);
        TileBase rightTile = TileAtPoint(pos + Vector2.right * playerWidth, Vector3Int.down);

        return leftTile != null || rightTile != null;
    }

    public bool CeilingCheck()
    {
        if (playerMovement.velocity.y < 0) return false;
        Vector2 pos = player.position;
        TileBase leftTile = TileAtPoint(pos + Vector2.left * playerWidth, Vector3Int.up);
        TileBase rightTile = TileAtPoint(pos + Vector2.right * playerWidth, Vector3Int.up);

        bool inCeiling = TileAtPoint(pos + Vector2.up * playerHeight, Vector3Int.zero) != null;

        return inCeiling && (leftTile != null || rightTile != null);
    }

    public bool WallCheckLeft()
    {
        if (playerMovement.velocity.x > 0) return false;
        Vector2 pos = player.position;
        TileBase topTile = TileAtPoint(pos + Vector2.up * playerHeight, Vector3Int.left);
        TileBase bottomTile = TileAtPoint(pos + Vector2.down * playerHeight, Vector3Int.left);

        bool inWall = TileAtPoint(pos + Vector2.left * playerWidth, Vector3Int.zero) != null;

        return inWall && (topTile != null || bottomTile != null);
    }

    public bool WallCheckRight()
    {
        if (playerMovement.velocity.x < 0) return false;
        Vector2 pos = player.position;
        TileBase topTile = TileAtPoint(pos + Vector2.up * playerHeight, Vector3Int.right);
        TileBase bottomTile = TileAtPoint(pos + Vector2.down * playerHeight, Vector3Int.right);

        bool inWall = TileAtPoint(pos + Vector2.right * playerWidth, Vector3Int.zero) != null;

        return inWall && (topTile != null || bottomTile != null);
    }

    TileBase TileAtPoint(Vector2 point, Vector3Int modifier)
    {
        return terrain.GetTile(terrain.WorldToCell(point) + modifier);
    }
}
