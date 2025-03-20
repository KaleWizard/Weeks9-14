using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{
    public Transform minuteHand;
    public Transform hourHand;

    public float timeAnHourTakes = 5;

    public float t;
    public int hour = 0;

    public UnityEvent<int> OnTheHour;

    private void Start()
    {
        StartCoroutine(RunClock());
    }

    public IEnumerator RunClock()
    {
        while (true)
        {
            t += Time.deltaTime;

            minuteHand.localEulerAngles = new Vector3(0, 0, t / timeAnHourTakes * -360f);
            hourHand.localEulerAngles = new Vector3(0, 0, (hour + t / timeAnHourTakes) * -30f);

            if (t > timeAnHourTakes)
            {
                t = 0;
                OnTheHour.Invoke(hour);

                hour++;
                if (hour == 12)
                {
                    hour = 0;
                }
            }

            yield return null;
        }
    }
}
