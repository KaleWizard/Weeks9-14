using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockHandRotation : MonoBehaviour
{
    bool isMinuteHand = false;

    public UnityEvent OnHourFinished;

    public float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool wasJustAtPeak = transform.localEulerAngles.z < 180f;
        transform.localEulerAngles -= new Vector3(0, 0, Time.deltaTime * speed * );
        if (transform.localEulerAngles.z > 180f && wasJustAtPeak && isMinuteHand)
        {
            OnHourFinished.Invoke();
        }
    }
}
