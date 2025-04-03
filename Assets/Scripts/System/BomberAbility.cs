using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAbility : MonoBehaviour
{
    AbilityCooldown cooldown;
    float cooldownLength = 4f;

    bool isHoldingBomb;

    public BombCollision bombPrefab;
    BombCollision bomb;

    Vector2 direction = Vector2.zero;
    public Transform indicatorPrefab;
    Transform indicator;

    Coroutine indicatorUpdate = null;

    PlayerMovement movementScript;

    // Called on scene load
    public void Initialize(AbilityCooldown c)
    {
        cooldown = c;
        movementScript = GetComponent<PlayerMovement>();
    }

    // Called upon ability selected
    public void Activate()
    {
        isHoldingBomb = false;
    }

    // Called upon ability action used
    public void Use()
    {
        if (isHoldingBomb)
        {
            ThrowBomb();
            isHoldingBomb = false;
            cooldown.StartTimer(cooldownLength);
        } else
        {
            SpawnBomb();
            isHoldingBomb = true;
        }
    }

    // Called on cooldown finished
    public void Refresh()
    {

    }

    // Called when ability deselected
    public void Deactivate()
    {
        // For each potentially active game object, destroy it if it's in the scene
        if (bomb != null)
        {
            Destroy(bomb);
            bomb = null;
        }
        if (indicator != null)
        {
            Destroy(indicator.gameObject);
            indicator = null;
        }
        // If the indicator is updating its direction, stop the coroutine
        if (indicatorUpdate != null)
        {
            StopCoroutine(indicatorUpdate);
        }
    }

    void ThrowBomb()
    {
        // End indicator appearance and updates
        StopCoroutine(indicatorUpdate);
        Destroy(indicator.gameObject);
        // Tell bomb to throw itself in the player's movement direction
        bomb.Throw(direction.normalized);
    }

    IEnumerator UpdateIndicator()
    {
        while (true)
        {
            // If player's velocity is somewhat significant, point indicator in the velocity's direction
            if (movementScript.velocity.SqrMagnitude() > 0.05f)
            {
                direction = movementScript.velocity;
                indicator.up = direction;
            }
            // Update bomb's position to player's position
            bomb.transform.position = transform.position;

            yield return null;
        }
    }

    void SpawnBomb()
    {
        // Spawn a bomb
        bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.physics = movementScript;
        // Spawn a throw direction indicator
        indicator = Instantiate(indicatorPrefab, transform);
        // Start indicator's update coroutine
        indicatorUpdate = StartCoroutine(UpdateIndicator());
    }
}
