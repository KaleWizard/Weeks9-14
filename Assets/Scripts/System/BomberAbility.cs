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
        if (bomb != null)
            Destroy(bomb);
        if (indicator != null)
            Destroy(indicator.gameObject);
        if (indicatorUpdate != null)
            StopCoroutine(indicatorUpdate);
    }

    void ThrowBomb()
    {
        StopCoroutine(indicatorUpdate);
        Destroy(indicator.gameObject);
        bomb.Throw(direction.normalized);
    }

    IEnumerator UpdateIndicator()
    {
        while (true)
        {
            if (movementScript.velocity.SqrMagnitude() > 0.05f)
            {
                direction = movementScript.velocity;
            }
            indicator.up = direction;
            bomb.transform.position = transform.position;
            yield return null;
        }
    }

    void SpawnBomb()
    {
        bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.physics = movementScript;
        indicator = Instantiate(indicatorPrefab, transform);
        indicatorUpdate = StartCoroutine(UpdateIndicator());
    }
}
