using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    public Image cooldownUI;

    Coroutine currentTimer;

    public UnityEvent OnCooldownFinished;

    public void StartTimer(float seconds)
    {
        if (currentTimer != null)
        {
            Debug.Log("Error: Timer started while another timer is running");
        }
        currentTimer = StartCoroutine(TimerRoutine(seconds));
    }

    public void EndTimer()
    {
        if (currentTimer == null) return;

        StopCoroutine(currentTimer);
    }

    IEnumerator TimerRoutine(float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            cooldownUI.fillAmount = t / seconds;
            yield return null;
        }
        OnCooldownFinished.Invoke();
        currentTimer = null;
    }
}
