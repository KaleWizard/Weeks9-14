using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindAbility : MonoBehaviour
{
    AbilityCooldown cooldown;
    float cooldownLength = 5f;

    List<Vector3> positions = new List<Vector3>();
    int maxPositions = 32;
    float trailTimeLength = 2f;

    Coroutine trailUpdate = null;

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
        if (trailUpdate != null)
        {
            StopCoroutine(trailUpdate);
            trailUpdate = null;
        }
        positions.Clear();
        trail.gameObject.SetActive(false);
    }

    IEnumerator UpdateTrail()
    {
        float timeBetweenPositionUpdates = trailTimeLength / maxPositions;
        float newPositionTimer = 0;

        UpdatePositions();

        while (true)
        {
            newPositionTimer += Time.deltaTime;
            if (newPositionTimer >= timeBetweenPositionUpdates)
            {
                UpdatePositions();
                newPositionTimer -= timeBetweenPositionUpdates;
            }
            trail.positionCount = positions.Count;
            trail.SetPositions(positions.ToArray());

            UpdateEchoPosition(newPositionTimer / timeBetweenPositionUpdates);

            yield return null;
        }
    }

    void UpdatePositions()
    {
        if (positions.Count >= maxPositions)
        {
            positions.RemoveAt(0);
        }
        positions.Add(transform.position);
    }

    void UpdateEchoPosition(float lerpDistance)
    {
        if (positions.Count == maxPositions)
        {
            Vector2 start = positions[0];
            Vector2 end = positions[1];
            playerEcho.position = Vector2.Lerp(start, end, lerpDistance);
        } else
        {
            playerEcho.position = positions[0];
        }
    }
}
