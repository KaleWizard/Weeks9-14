using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindAbility : MonoBehaviour
{
    AbilityCooldown cooldown;
    float cooldownLength = 5f;

    // List of recent player positions to draw trail over
    List<Vector3> positions = new List<Vector3>();
    int maxPositions = 32;
    float trailTimeLength = 2f;

    Coroutine trailUpdate = null;

    // Translucent player sprite at the end of the trail
    // Note that these must be from the same GameObject
    public Transform playerEcho;
    public LineRenderer trail;

    // Called on scene load
    public void Initialize(AbilityCooldown c)
    {
        cooldown = c;
        Deactivate();
    }

    // Called upon ability selected
    public void Activate()
    {
        // Set trail and echo sprite to visible and updating
        trail.gameObject.SetActive(true);
        trailUpdate = StartCoroutine(UpdateTrail());
    }

    // Called upon ability action used
    public void Use()
    {
        transform.position = playerEcho.position;
        cooldown.StartTimer(cooldownLength);
        Deactivate();
    }

    // Called on cooldown finished
    public void Refresh()
    {
        Activate();
    }

    // Called when ability deselected
    public void Deactivate()
    {
        // Stop trail update coroutine if it's running
        if (trailUpdate != null)
        {
            StopCoroutine(trailUpdate);
            trailUpdate = null;
        }
        // Clear list of positions for next
        positions.Clear();
        // Disable trail. Note that the echo sprite is on the same GameObject
        trail.gameObject.SetActive(false);
    }

    IEnumerator UpdateTrail()
    {
        // Get the time between updates of the position list
        float timeBetweenPositionUpdates = trailTimeLength / maxPositions;
        float newPositionTimer = timeBetweenPositionUpdates;

        while (true)
        {
            newPositionTimer += Time.deltaTime;
            // If enough time has passed, add update the position list
            if (newPositionTimer >= timeBetweenPositionUpdates)
            {
                UpdatePositions();
                newPositionTimer -= timeBetweenPositionUpdates;
            }
            // Set the trail's line to follow the position list
            trail.positionCount = positions.Count;
            trail.SetPositions(positions.ToArray());

            // Update the echo sprite's position
            UpdateEchoPosition(newPositionTimer / timeBetweenPositionUpdates);

            yield return null;
        }
    }

    void UpdatePositions()
    {
        // If the limit of positions in the list has been reached, remove the one farthest from the player
        if (positions.Count >= maxPositions)
        {
            positions.RemoveAt(0);
        }
        // Add the player's current position to the list
        positions.Add(transform.position);
    }

    void UpdateEchoPosition(float lerpDistance)
    {
        // If the list is full, move towards the second farthest point in the list
        if (positions.Count >= maxPositions)
        {
            Vector2 start = positions[0];
            Vector2 end = positions[1];
            playerEcho.position = Vector2.Lerp(start, end, lerpDistance);
        } 
        // Otherwise stay at the initial position
        else
        {
            playerEcho.position = positions[0];
        }
    }
}
