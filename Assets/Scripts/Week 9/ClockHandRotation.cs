using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockHandRotation : MonoBehaviour
{
    public bool isMinuteHand = false;

    public UnityEvent OnHourFinished;

    public float speed = 30f;

    public float rotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation += Time.deltaTime * speed * (isMinuteHand? 12: 1);
        if (rotation > 360f && isMinuteHand)
        {
            rotation -= 360f;
            OnHourFinished.Invoke();
        }
        transform.localEulerAngles = new Vector3(0, 0, rotation * -1);
    }
}
