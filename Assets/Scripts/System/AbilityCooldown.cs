using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    // Cooldown UI Circle timer
    public Image cooldownUI;

    // Active timer coroutine if there is one, nlul otherwise
    Coroutine currentTimer;

    public UnityEvent OnCooldownFinished;

    // Starts a new cooldown for time of given seconds
    public void StartTimer(float seconds)
    {
        if (currentTimer != null)
        {
            Debug.Log("Error: Timer started while another timer is running");
        }
        currentTimer = StartCoroutine(TimerRoutine(seconds));
    }

    // Ends current cooldown timer if one is active
    public void EndTimer()
    {
        // If no timer is active, do nothing
        if (currentTimer == null) return;
        // Stop the timer
        StopCoroutine(currentTimer);
        currentTimer = null;
        // Reset UI element to full
        cooldownUI.fillAmount = 1f;
    }

    IEnumerator TimerRoutine(float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            // Set UI element's fill to ratio of current time passed to total time
            cooldownUI.fillAmount = t / seconds;
            yield return null;
        }
        // Cooldown is finished so invoke OnCooldownFinished event
        OnCooldownFinished.Invoke();
        // Also end accessability to this coroutine
        currentTimer = null;
    }

    public bool IsActive()
    {
        return currentTimer != null;
    }
}
