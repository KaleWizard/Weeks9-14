using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAbility : MonoBehaviour
{
    AbilityCooldown cooldown;
    float cooldownLength = 4f;

    public CloneMovement clonePrefab;
    CloneMovement clone;

    float cloneLifeLength = 4f;

    public PlayerMovement movementBase;

    // Coroutine is active during the lifetime of a clone
    Coroutine cloneLifeTimer = null;

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
        clone.Initialize(movementBase);
        // Begin waiting for clone's lifetime to end
        cloneLifeTimer = StartCoroutine(CloneLifeTimer());
    }

    // Called on cooldown finished
    public void Refresh()
    {

    }

    // Called when ability deselected
    public void Deactivate()
    {
        // If clone is active, destroy it
        if (clone != null)
        {
            Destroy(clone.gameObject);
        }
        // If clone's life timer is active, stop it
        if (cloneLifeTimer != null)
        {
            StopCoroutine(cloneLifeTimer);
            cloneLifeTimer = null;
        }
    }

    IEnumerator CloneLifeTimer()
    {
        // After clone's lifetime has passed, destroy it and start the cooldown timer
        yield return new WaitForSeconds(cloneLifeLength);
        Destroy(clone.gameObject);
        cooldown.StartTimer(cooldownLength);
        cloneLifeTimer = null;
    }
}
