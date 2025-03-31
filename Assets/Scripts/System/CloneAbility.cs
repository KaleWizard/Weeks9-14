using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAbility : MonoBehaviour
{
    AbilityCooldown cooldown;
    float cooldownLength = 4f;

    public CloneMovement clonePrefab;

    public PlayerMovement movementBase;

    CloneMovement clone;
    float cloneLifeLength = 4f;

    Coroutine cloneDeathTimer = null;

    // Called on scene load
    public void Initialize(AbilityCooldown c)
    {
        cooldown = c;
    }

    // Called upon ability selected
    public void Activate()
    {
        
    }

    // Called upon ability action used
    public void Use()
    {
        clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
        clone.movementBase = movementBase;
        clone.terrain = movementBase.terrain;
        clone.velocity = movementBase.velocity;
        cloneDeathTimer = StartCoroutine(CloneLifetime());
    }

    // Called on cooldown finished
    public void Refresh()
    {

    }

    // Called when ability deselected
    public void Deactivate()
    {
        if (clone != null)
        {
            Destroy(clone.gameObject);
        }
        if (cloneDeathTimer != null)
        {
            StopCoroutine(cloneDeathTimer);
            cloneDeathTimer = null;
        }
    }

    IEnumerator CloneLifetime()
    {
        yield return new WaitForSeconds(cloneLifeLength);
        Destroy(clone.gameObject);
        cooldown.StartTimer(cooldownLength);
        cloneDeathTimer = null;
    }
}
